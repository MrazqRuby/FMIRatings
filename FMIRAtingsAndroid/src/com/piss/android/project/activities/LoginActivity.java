package com.piss.android.project.activities;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.fragments.RegistrationFragment;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

public class LoginActivity extends FragmentActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		
		Fragment fragment = new RegistrationFragment();
		addFragment(fragment);
		
	}
	
	public void addFragment(Fragment fragment) {
		FragmentManager fragmentManager = getSupportFragmentManager();
		//Remove all fragments before adding new 
		Log.i("DEBUG", "fragments: " + fragmentManager.getBackStackEntryCount());
		if (fragmentManager.getBackStackEntryCount() > 0) {
			fragmentManager.popBackStack();
		}
		Log.d("ADD FRagment", "");
		
		FragmentTransaction transaction = fragmentManager.beginTransaction();
		transaction.replace(R.id.content_frame, fragment, fragment.getClass()
				.getSimpleName());
		transaction.addToBackStack(fragment.getClass().getSimpleName());
		transaction.commit();
	}
	
	
	@Override
	public void onDestroy(){
		FragmentManager fragmentManager = getSupportFragmentManager();
		//Remove all fragments before adding new 
		Log.i("DEBUG", "fragments: " + fragmentManager.getBackStackEntryCount());
		if (fragmentManager.getBackStackEntryCount() > 0) {
			fragmentManager.popBackStack();
		}
		super.onDestroy();
	}
}
