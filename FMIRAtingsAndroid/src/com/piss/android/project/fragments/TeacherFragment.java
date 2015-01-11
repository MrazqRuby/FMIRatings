package com.piss.android.project.fragments;

import java.io.Serializable;
import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.RatingBar;
import android.widget.TextView;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.models.Votes;
import com.piss.android.project.tasks.GetVoteForTeacherTask;

public class TeacherFragment extends Fragment implements OnClickListener {

	private final static String TEACHER = "teacher";
	private RatingBar ratingBarClarity;
	private RatingBar ratingBarEnthusiasum;
	private RatingBar ratingBarEvaluation;
	private RatingBar ratingBarSpeed;
	private RatingBar ratingBarScope;

	public static TeacherFragment getInstance(Teacher teacher) {
		TeacherFragment fragment = new TeacherFragment();
		Bundle args = new Bundle();
		args.putSerializable(TEACHER, (Serializable) teacher);
		fragment.setArguments(args);
		return fragment;
	}

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater
				.inflate(R.layout.teacher_fragment_layout, null);

		TextView teacher_name = (TextView) rootView
				.findViewById(R.id.teacher_name);
		TextView teacher_department = (TextView) rootView
				.findViewById(R.id.teacher_department);
		ratingBarClarity = (RatingBar) rootView
				.findViewById(R.id.clarity_rating);
		ratingBarEnthusiasum = (RatingBar) rootView
				.findViewById(R.id.enthusiasum_rating);
		ratingBarEvaluation = (RatingBar) rootView
				.findViewById(R.id.evaluation_rating);
		ratingBarSpeed = (RatingBar) rootView.findViewById(R.id.speed_rating);
		ratingBarScope = (RatingBar) rootView.findViewById(R.id.scope_rating);
		Button vote = (Button) rootView.findViewById(R.id.vote_button);
		Button comments = (Button) rootView.findViewById(R.id.comments_button);
		Button courses = (Button) rootView.findViewById(R.id.courses_button);

		vote.setOnClickListener(this);
		comments.setOnClickListener(this);
		courses.setOnClickListener(this);

		Teacher teacher = ((Teacher) getArguments().getSerializable(TEACHER));
		teacher_name.setText(teacher.getName());
		teacher_department.setText(teacher.getDepartment());
		((MainActivity) getActivity()).setUpNavigationToolbar();
		ratingBarClarity.setEnabled(false);
		ratingBarEnthusiasum.setEnabled(false);
		ratingBarEvaluation.setEnabled(false);
		ratingBarScope.setEnabled(false);
		ratingBarSpeed.setEnabled(false);
		getVotes();
		return rootView;
	}

	private void getVotes() {
		Teacher teacher = ((Teacher) getArguments().getSerializable(TEACHER));
		GetVoteForTeacherTask getVotesTask = new GetVoteForTeacherTask(
				teacher.getId()) {

			@Override
			protected void onPostExecute(ArrayList<Votes> result) {
				if (result != null) {
					Votes vote = null;
					String criterion = null;
					for(int i=0;i<result.size();i++){
						vote = result.get(i);
						criterion = vote.getCriterionName();
						if(criterion.equals("Яснота")){
							ratingBarClarity.setRating(vote.getAverage());
						}else if(criterion.equals("Ентусиазъм")){
							ratingBarEnthusiasum.setRating(vote.getAverage());
						}else if(criterion.equals("Критерии на оценяване")){
							ratingBarEvaluation.setRating(vote.getAverage());
						}else if(criterion.equals("Скорост на преподаване")){
							ratingBarSpeed.setRating(vote.getAverage());
						}else if(criterion.equals("Обхват на преподавания материал")){
							ratingBarScope.setRating(vote.getAverage());
						}
						
					}
					
				}
			}

		};

		getVotesTask.execute();
	}

	@Override
	public void onClick(View v) {
		int id = v.getId();
		Teacher teacher = ((Teacher) getArguments().getSerializable(TEACHER));
		switch (id) {
		case R.id.courses_button:
			DisplayCoursesFragment courses = DisplayCoursesFragment
					.getInstance(teacher.getCourses());
			((MainActivity) getActivity()).addFragment(courses);
			break;
		case R.id.comments_button:

			ComentsFragment comments = ComentsFragment.getInstance(teacher
					.getComments());
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
