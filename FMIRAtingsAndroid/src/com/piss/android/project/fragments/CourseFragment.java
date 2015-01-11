package com.piss.android.project.fragments;

import java.io.Serializable;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;

public class CourseFragment  extends Fragment implements OnClickListener{

	private final static String COURSE = "course";
	
	public static CourseFragment getInstance(Course course) {
		CourseFragment fragment = new CourseFragment();
		Bundle args = new Bundle();
		args.putSerializable(COURSE, (Serializable) course);
		fragment.setArguments(args);
		return fragment;
	}

	
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.course_fragment_layout, null);
		
		Button vote = (Button) rootView.findViewById(R.id.vote_button);
		Button teachers = (Button) rootView.findViewById(R.id.teachers_button);
		Button comments = (Button) rootView.findViewById(R.id.comments_button);
		TextView course_name = (TextView) rootView.findViewById(R.id.course_name);
		TextView category = (TextView) rootView.findViewById(R.id.category);
		
		Course course = ((Course) getArguments().getSerializable(COURSE));
		course_name.setText(course.getName());
		category.setText(course.getCategory());		
		
		vote.setOnClickListener(this);
		teachers.setOnClickListener(this);
		comments.setOnClickListener(this);
		
		((MainActivity) getActivity()).setUpNavigationToolbar();
		((MainActivity) getActivity()).getSupportActionBar().setTitle("Курс");
		
		return rootView;
	}
	
	@Override
	public void onClick(View v) {
		int id = v.getId();
		switch (id) {
		case R.id.teachers_button: 
			Course courseTeachers = ((Course) getArguments().getSerializable(COURSE));
			TeachersFragment teachers = TeachersFragment.getInstance(courseTeachers.getTeachers());
			((MainActivity) getActivity()).addFragment(teachers);
			break;
		case R.id.comments_button:
			Course course = ((Course) getArguments().getSerializable(COURSE));
			ComentsFragment comments = ComentsFragment.getInstance(course.getComments());
			((MainActivity) getActivity()).addFragment(comments);
			break;
		case R.id.vote_button:
			long courseId = ((Course) getArguments().getSerializable(COURSE))
					.getId();
			VoteForCoursesFragment fragment = VoteForCoursesFragment.instance(courseId);
			((MainActivity) getActivity()).addFragment(fragment);
			break;
		default:
			break;
		}
	}

}
