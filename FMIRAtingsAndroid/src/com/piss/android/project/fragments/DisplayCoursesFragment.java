package com.piss.android.project.fragments;

import java.io.Serializable;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.models.Teacher;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class DisplayCoursesFragment extends Fragment {

	private final static String COURSE = "course";
	public static DisplayCoursesFragment getInstance(Course course){
		CourseFragment fragment  = new CourseFragment();
		Bundle args = new Bundle();
		args.putSerializable(COURSE, (Serializable) course);
		fragment.setArguments(args);
		return fragment;
	}
	
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);
		//ListView myListView;
		return rootView;
	}
}
