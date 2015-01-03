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
		
		userName = (EditText) rootView.findViewById(R.id.user_name);
		email = (EditText) rootView.findViewById(R.id.email);
		password = (EditText) rootView.findViewById(R.id.password);
		confirmPassword = (EditText) rootView.findViewById(R.id.confirm_password);
		course = (EditText) rootView.findViewById(R.id.confirm_password);
		specialty = (EditText) rootView.findViewById(R.id.specialty);
		graduationDate = (EditText) rootView.findViewById(R.id.graduation_date);
		
		register = (Button) rootView.findViewById(R.id.login_btn);
		
		
		register.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				
				//Check if user name, pass, email, course and specialty is empty
				if(userName.getText().equals("")){
					userName.setError("Въведете потребителско име!");
					return;
				}
				if(password.getText().equals("")){
					password.setError("Въведете парола!");
					return;
				}
				if(confirmPassword.getText().equals("")){
					confirmPassword.setError("Потвърдете парола!");
					return;
				}
			
				if(email.getText().equals("")){
					email.setError("Въведете имейл!");
					return;
				}
				if(course.getText().equals("")){
					course.setError("Въведете курс!");
					return;
				}
				if(specialty.getText().equals("")){
					specialty.setError("Въведете специалност!");
					return;
				}
				
				//Check if confirm password equals password
				if(!password.getText().equals(confirmPassword.getText())){
					confirmPassword.setError("Въведената парола не отговаря!");
					confirmPassword.setText("");
					return;
				}
				
				
				// TODO: execute login task
			}
		});
		
		return rootView;
	}
}
