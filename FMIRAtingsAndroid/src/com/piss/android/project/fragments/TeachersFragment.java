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
import com.piss.android.project.adapters.TeachersAdapter;
import com.piss.android.project.adapters.TeachersNameAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.utils.HeaderConstants;

public class TeachersFragment  extends Fragment{

	private final static String TEACHERS = "teachers";
	public static TeachersFragment getInstance(ArrayList<Teacher> teachers){
		TeachersFragment fragment  = new TeachersFragment();
		Bundle args = new Bundle();
		args.putSerializable(TEACHERS, teachers);
		fragment.setArguments(args);
		return fragment;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		ArrayList<Teacher> teachers = (ArrayList<Teacher>) getArguments().getSerializable(TEACHERS);
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);
		
		ListView myListView = (ListView) rootView.findViewById(R.id.list_view);
		TeachersAdapter myAdapter = new TeachersAdapter(teachers, getActivity());
		
		myListView.setAdapter(myAdapter);
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.TEACHERS);
		setHasOptionsMenu(true);
		return rootView;
	}
	
	@Override
	public void onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		if (getActivity().isFinishing()) {
			return;
		}
		// getActivity().invalidateOptionsMenu();
		final MenuItem searchItem = menu.findItem(R.id.action_search);
		SearchView mSearchView = (SearchView) MenuItemCompat
				.getActionView(searchItem);
		searchItem.setVisible(false);
	}
}
