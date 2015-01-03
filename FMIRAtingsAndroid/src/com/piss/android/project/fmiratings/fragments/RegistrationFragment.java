package com.piss.android.project.fmiratings.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.piss.android.project.fmiratings.R;

public class RegistrationFragment extends Fragment{
	private EditText userName;
	private EditText email;
	private EditText password;
	private EditText confirmPassword;
	private EditText course;
	private EditText specialty;
	private EditText graduationDate;
	
	private Button register;

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.register_layout, null);
		
		email = (EditText) rootView.findViewById(R.id.email);
		password = (EditText) rootView.findViewById(R.id.password);
		register = (Button) rootView.findViewById(R.id.login_btn);
		
		
		register.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO: execute login task
				//TODO: check if user name, pass, email, course and specialty is empty
			}
		});
		
		return rootView;
	}
}
