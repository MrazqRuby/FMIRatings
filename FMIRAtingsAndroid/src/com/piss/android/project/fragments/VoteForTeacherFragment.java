package com.piss.android.project.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RatingBar;
import android.widget.TextView;
import com.piss.android.project.tasks.*;

import com.piss.android.project.fmiratings.R;

public class VoteForTeacherFragment extends Fragment {

	private final static String TEACHER_ID = "id";
	private RatingBar ratingBarClarity;
	private RatingBar ratingBarEnthusiasum;
	private RatingBar ratingBarEvaluation;
	private RatingBar ratingBarSpeed;
	private RatingBar ratingBarScope;
	private TextView comment;
	private ImageView sendComment;

	public static VoteForTeacherFragment instance(Integer id) {
		VoteForTeacherFragment fragment = new VoteForTeacherFragment();
		Bundle args = new Bundle();
		args.putInt(TEACHER_ID, id);
		fragment.setArguments(args);
		return fragment;

	}

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.add_comment_layout, null);

		ratingBarClarity = (RatingBar) rootView
				.findViewById(R.id.clarity_rating);
		ratingBarEnthusiasum = (RatingBar) rootView
				.findViewById(R.id.enthusiasum_rating);
		ratingBarEvaluation = (RatingBar) rootView
				.findViewById(R.id.evaluation_rating);
		ratingBarSpeed = (RatingBar) rootView.findViewById(R.id.speed_rating);
		ratingBarScope = (RatingBar) rootView.findViewById(R.id.scope_rating);
		comment = (TextView) rootView.findViewById(R.id.editComment);
		sendComment = (ImageView) rootView.findViewById(R.id.commentArrow);
		sendComment.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				int teacherID = getArguments().getInt(TEACHER_ID);
				int userID = 0;
				int ratingClarity = (int) ratingBarClarity.getRating();
				int ratingEnthusiasum = (int) ratingBarEnthusiasum.getRating();
				int ratingEvaluation = (int) ratingBarEvaluation.getRating();
				int ratingSpeed = (int) ratingBarSpeed.getRating();
				int ratingScope = (int) ratingBarScope.getRating();
				String text = comment.getText().toString();

				PostVoteForTeacher postVote = new PostVoteForTeacher(teacherID,
						userID, ratingClarity, ratingEnthusiasum,
						ratingEvaluation, ratingSpeed, ratingScope, text);

				postVote.execute();

			}
		});

		return rootView;
	}
}
