package com.piss.android.project.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.piss.android.project.fmiratings.R;

public class LoginFragment extends Fragment {

	private EditText email;
	private EditText password;
	private Button login;
	
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.login_layout, null);
		
		email = (EditText) rootView.findViewById(R.id.email);
		password = (EditText) rootView.findViewById(R.id.password);
		login = (Button) rootView.findViewById(R.id.login_btn);
		
		
		login.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				//Check if pass and name is empty
				if(email.getText().equals("")){
					email.setError("Въведете имейл!");
					return;
				}
				if(password.getText().equals("")){
					password.setError("Въведете парола!");
					return;
				}
				
				// TODO: execute login task
				
			}
		});
		
		return rootView;
	}
}
