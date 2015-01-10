package com.piss.android.project.fragments;

import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.widget.SearchView;
import android.support.v7.widget.SearchView.OnQueryTextListener;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.Toast;

import com.piss.android.project.adapters.*;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.tasks.GetSearchTeacherTask;
import com.piss.android.project.tasks.GetTeachersTask;

public class TeachersListFragment extends Fragment {
	
	private ListView mListView;

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);

		mListView = (ListView) rootView.findViewById(R.id.list_view);
	
		mListView.setAdapter(null);

		// TODO: Set authentication token for current user

		GetTeachersTask getCoursesTask = new GetTeachersTask(null, null) {

			@Override
			protected void onPostExecute(ArrayList<Teacher> result) {
				if (result != null) {
					TeachersAdapter adapter = new TeachersAdapter(result, getActivity());

					mListView.setAdapter(adapter);
				} else {
					Toast.makeText(getActivity(), "Server Error",
							Toast.LENGTH_SHORT).show();
				}

			}

		};
		getCoursesTask.execute();
		setHasOptionsMenu(true);

		return rootView;
	}
	
	@Override
	public void onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		getActivity().invalidateOptionsMenu();
		final MenuItem searchItem = menu.findItem(R.id.action_search);
		SearchView mSearchView = (SearchView) MenuItemCompat
				.getActionView(searchItem);
		mSearchView.setOnQueryTextListener(new OnQueryTextListener() {
			
			@Override
			public boolean onQueryTextSubmit(String arg0) {
				// TODO Auto-generated method stub
				return false;
			}
			
			@Override
			public boolean onQueryTextChange(String text) {
				 GetSearchTeacherTask search = new GetSearchTeacherTask(text){
					// if it is empty => return global variable
					@Override
					protected void onPostExecute(ArrayList<Teacher> result) {
						if (result != null) {
							Log.i("DEBUG", "onPostExecute teachers");
							TeachersAdapter adapter = new TeachersAdapter(result, getActivity());

							mListView.setAdapter(adapter);
						} else {
							Toast.makeText(getActivity(), "Server Error",
									Toast.LENGTH_SHORT).show();
						}
					}
					
				};
				
				search.execute();
				
				return true;
			}
		});
	}

}
