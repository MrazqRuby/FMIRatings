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
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.piss.android.project.adapters.CoursesAdapter;
import com.piss.android.project.adapters.TeachersAdapter;

public class CoursesListFragment extends Fragment {

	RecyclerView recyclerView;

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);

		recyclerView = (RecyclerView) rootView.findViewById(R.id.recycler_view);
		recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
		recyclerView.setAdapter(null);

		// TODO: Set authentication token for current user

		GetCoursesTask getCoursesTask = new GetCoursesTask(null) {

			@Override
			protected void onPostExecute(ArrayList<Course> result) {
				if (result != null) {
					Log.i("DEBUG", "onPostExecute courses");
					CoursesAdapter adapter = new CoursesAdapter(result);

					recyclerView.setAdapter(adapter);
				} else {
					Toast.makeText(getActivity(), "Server Error",
							Toast.LENGTH_SHORT).show();
				}

			}

		};
		getCoursesTask.execute();

		return rootView;
	}

	private void setAdapter(ArrayList<Course> result) {

	}

}
