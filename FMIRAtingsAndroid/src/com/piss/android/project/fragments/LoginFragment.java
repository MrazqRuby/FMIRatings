package com.piss.android.project.fragments;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.piss.android.project.activities.LoginActivity;
import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.tasks.LoginTask;
import com.piss.android.project.utils.APIConnectionConstants;

public class LoginFragment extends Fragment {

	private EditText name;
	private EditText password;
	private Button login;

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.login_layout, null);

		name = (EditText) rootView.findViewById(R.id.username);
		password = (EditText) rootView.findViewById(R.id.password);
		login = (Button) rootView.findViewById(R.id.login_btn);

		login.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				// Check if pass and name is empty
				if (name.getText().equals("")) {
					name.setError("Въведете име!");
					return;
				}
				if (password.getText().equals("")) {
					password.setError("Въведете парола!");
					return;
				}

				//Execute login task
				LoginTask login = new LoginTask(name.getText().toString(),
						password.getText().toString()) {

							@Override
							protected void onPostExecute(String result) {
								if(result!=null){
									//Save user name , password and auth token in app prefs
									SharedPreferences settings = getActivity().getSharedPreferences(APIConnectionConstants.PREFERENCES, Activity.MODE_PRIVATE);
									Editor editor = settings.edit();
									editor.putString(APIConnectionConstants.USER_NAME, name.getText().toString());
									editor.putString(APIConnectionConstants.PASSWORD, password.getText().toString());
									editor.putString(APIConnectionConstants.AUTHENTICATION, result);
									editor.commit();									
									
									
									//Start MainActivity 
									Intent i = new Intent(getActivity() , MainActivity.class);
									getActivity().startActivity(i);
									((LoginActivity) getActivity()).removeFragmentsInclusive(LoginFragment.class.getSimpleName());
									getActivity().finish();
									
								}else{
									Toast.makeText(getActivity(), "Server error", Toast.LENGTH_SHORT).show();
								}
							}

					
				};
				login.execute();

			}
		});

		return rootView;
	}
}
