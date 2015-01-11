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
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.Toast;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.adapters.TeachersAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.tasks.GetSearchTeacherTask;
import com.piss.android.project.tasks.GetTeachersTask;
import com.piss.android.project.utils.HeaderConstants;

public class TeachersListFragment extends Fragment {
	
	private ListView mListView;
	private ArrayList<Teacher> teachersList;

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);

		mListView = (ListView) rootView.findViewById(R.id.list_view);
	
		mListView.setAdapter(null);
		
		mListView.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				Teacher teacher = teachersList.get(position);
				TeacherFragment fragment = TeacherFragment.getInstance(teacher);
				((MainActivity)getActivity()).addFragment(fragment);
				
			}
		});

		// TODO: Set authentication token for current user

		GetTeachersTask getCoursesTask = new GetTeachersTask(null, null) {

			@Override
			protected void onPostExecute(ArrayList<Teacher> result) {
				if (result != null) {
					teachersList = result;
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
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.TEACHERS);
		return rootView;
	}
	
	@Override
	public void onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		if(getActivity().isFinishing()){
			return;
		}
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
