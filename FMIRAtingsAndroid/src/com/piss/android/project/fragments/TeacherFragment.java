package com.piss.android.project.fragments;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class TeacherFragment extends Fragment {

	private final static String TEACHER = "teacher";
	public static TeacherFragment getInstance(Teacher teacher){
		TeacherFragment fragment  = new TeacherFragment();
		Bundle args = new Bundle();
		args.putSerializable(TEACHER, teacher);
		fragment.setArguments(args);
		return fragment;
	}
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teacher_fragment_layout, null);
		
		return rootView;
	}

}
