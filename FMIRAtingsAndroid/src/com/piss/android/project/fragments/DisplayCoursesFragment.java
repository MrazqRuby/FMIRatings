package com.piss.android.project.fragments;

import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.widget.SearchView;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.adapters.CoursesAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.utils.HeaderConstants;

public class DisplayCoursesFragment extends Fragment {

	private final static String COURSE = "course";
	public static DisplayCoursesFragment getInstance(ArrayList<Course> courses){
		DisplayCoursesFragment fragment  = new DisplayCoursesFragment();
		Bundle args = new Bundle();
		args.putSerializable(COURSE, courses);
		fragment.setArguments(args);
		return fragment;
	}
	
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);
		ArrayList<Course> comments = (ArrayList<Course>) getArguments().getSerializable(COURSE);
		ListView myListView = (ListView) rootView.findViewById(R.id.list_view);
		CoursesAdapter myAdapter = new CoursesAdapter(comments, getActivity());
		myListView.setAdapter(myAdapter);
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.COURSES);
		setHasOptionsMenu(true);
		return rootView;
	}
	
	
	@Override
	public void onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		if(getActivity().isFinishing()){
			return;
		}
		//getActivity().invalidateOptionsMenu();
		final MenuItem searchItem = menu.findItem(R.id.action_search);
		SearchView mSearchView = (SearchView) MenuItemCompat
				.getActionView(searchItem);
		searchItem.setVisible(false);
	}
}
