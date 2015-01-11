package com.piss.android.project.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;

public class TeacherFragment extends Fragment implements OnClickListener {

	private final static String TEACHER = "teacher";

	public static TeacherFragment getInstance(Teacher teacher) {
		TeacherFragment fragment = new TeacherFragment();
		Bundle args = new Bundle();
		args.putSerializable(TEACHER, teacher);
		fragment.setArguments(args);
		return fragment;
	}

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater
				.inflate(R.layout.teacher_fragment_layout, null);
		Button vote = (Button) rootView.findViewById(R.id.vote_button);
		Button comments = (Button) rootView.findViewById(R.id.comments_button);
		Button courses = (Button) rootView.findViewById(R.id.courses_button);

		vote.setOnClickListener(this);
		comments.setOnClickListener(this);
		courses.setOnClickListener(this);

		((MainActivity) getActivity()).setUpNavigationToolbar();
		return rootView;
	}

	@Override
	public void onClick(View v) {
		int id = v.getId();
		switch (id) {
		case R.id.courses_button:

			break;
		case R.id.comments_button:
			Teacher teacher = ((Teacher) getArguments().getSerializable(TEACHER));
			ComentsFragment comments = ComentsFragment.getInstance(teacher.getComments());
			((MainActivity) getActivity()).addFragment(comments);
			break;
		case R.id.vote_button:
			long teacherID = ((Teacher) getArguments().getSerializable(TEACHER))
					.getId();
			VoteForTeacherFragment fragment = VoteForTeacherFragment
					.instance(teacherID);
			((MainActivity) getActivity()).addFragment(fragment);
			break;
		default:
			break;
		}
	}

}
