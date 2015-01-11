package com.piss.android.project.fragments;

import com.piss.android.project.activities.LoginActivity;
import com.piss.android.project.fmiratings.R;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

public class InitialFragment extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.initial_layout, null);

		Button login = (Button) rootView.findViewById(R.id.login_button);
		Button register = (Button) rootView
				.findViewById(R.id.registration_button);

		login.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				LoginFragment fragment = new LoginFragment();
				((LoginActivity) getActivity()).addFragment(fragment);
			}
		});

		register.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				RegistrationFragment fragment = new RegistrationFragment();
				((LoginActivity) getActivity()).addFragment(fragment);
			}
		});

		return rootView;
	}

}
