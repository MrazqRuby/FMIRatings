package com.piss.android.project.fragments;


import java.util.ArrayList;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.tasks.GetCoursesTask;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.piss.android.project.adapters.CoursesAdapter;

public class CoursesListFragment extends Fragment{

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_layout, null);
		
		final RecyclerView recyclerView = (RecyclerView) rootView.findViewById(R.id.recycler_view);
		recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
		
		
		//TODO:  Set authentication token for current user
		
		GetCoursesTask getCoursesTask = new GetCoursesTask(null){

			@Override
			protected void onPostExecute(ArrayList<Course> result) {
				CoursesAdapter adapter = new CoursesAdapter(result);
				
				recyclerView.setAdapter(adapter);
			}
			
		};
		getCoursesTask.execute();
		
		
		return rootView;
	}

	
}
