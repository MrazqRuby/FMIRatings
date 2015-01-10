package com.piss.android.project.fragments;

import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.piss.android.project.adapters.TeachersAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.tasks.GetTeachersTask;

public class TeachersListFragment extends Fragment{

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_layout, null);
		
		final RecyclerView recyclerView = (RecyclerView) rootView.findViewById(R.id.recycler_view);
		recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
		
		
		//TODO:  Set authentication token for current user
		
		GetTeachersTask getCoursesTask = new GetTeachersTask(null, null){

			@Override
			protected void onPostExecute(ArrayList<Teacher> result) {
				TeachersAdapter adapter = new TeachersAdapter(result);
				
				recyclerView.setAdapter(adapter);
			}
			
		};
		getCoursesTask.execute();
		
		
		return rootView;
	}

	
}
