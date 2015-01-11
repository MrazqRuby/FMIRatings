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
import android.widget.ListView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.adapters.CoursesAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.tasks.GetCoursesTask;
import com.piss.android.project.tasks.GetSearchCourseTask;
import com.piss.android.project.utils.HeaderConstants;

public class CoursesListFragment extends Fragment {

	ListView mListView;
	ArrayList<Course> coursesList;

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
				Course course = coursesList.get(position);
				CourseFragment fragment = CourseFragment.getInstance(course);
				((MainActivity)getActivity()).addFragment(fragment);
				
			}
		});

		// TODO: Set authentication token for current user

		GetCoursesTask getCoursesTask = new GetCoursesTask(null, null) {

			@Override
			protected void onPostExecute(ArrayList<Course> result) {
				if (result != null) {
					coursesList = result;
					Log.i("DEBUG", "onPostExecute courses");
					CoursesAdapter adapter = new CoursesAdapter(result, getActivity());

					mListView.setAdapter(adapter);
				} else {
					Toast.makeText(getActivity(), "Server Error",
							Toast.LENGTH_SHORT).show();
				}

			}

		};
		getCoursesTask.execute();

		setHasOptionsMenu(true);
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.COURSES);
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
				GetSearchCourseTask search = new GetSearchCourseTask(text){
					// if it is empty => return global variable
					@Override
					protected void onPostExecute(ArrayList<Course> result) {
						if (result != null) {
							Log.i("DEBUG", "onPostExecute courses");
							CoursesAdapter adapter = new CoursesAdapter(result, getActivity());

							mListView.setAdapter(adapter);
						} else {
							CoursesAdapter adapter = new CoursesAdapter(coursesList, getActivity());

							mListView.setAdapter(adapter);
						}
					}
					
				};
				
				search.execute();
				return true;
			}
		});
	}
}
