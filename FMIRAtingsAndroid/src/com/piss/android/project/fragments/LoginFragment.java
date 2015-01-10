package com.piss.android.project.fragments;

import android.content.Intent;
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
					name.setError("Въведете имейл!");
					return;
				}
				if (password.getText().equals("")) {
					password.setError("Въведете парола!");
					return;
				}

				// TODO: execute login task
				LoginTask login = new LoginTask(name.getText().toString(),
						password.getText().toString()) {

							@Override
							protected void onPostExecute(Boolean result) {
								if(result){
									Intent i = new Intent( getActivity(),MainActivity.class);
									getActivity().startActivity(i);
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
