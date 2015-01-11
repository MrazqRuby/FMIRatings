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

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.tasks.PostVoteForCourseTask;
import com.piss.android.project.utils.HeaderConstants;

public class VoteForCoursesFragment extends Fragment{
	private final static String COURSE_ID = "id";
	private RatingBar ratingBarClarity;
	private RatingBar ratingBarWorkload;
	private RatingBar ratingBarInterest;
	private RatingBar ratingBarSimplicity;
	private RatingBar ratingBarUsefulness;
	private TextView comment;
	private ImageView sendComment;

	public static VoteForCoursesFragment instance(Long id) {
		VoteForCoursesFragment fragment = new VoteForCoursesFragment();
		Bundle args = new Bundle();
		args.putLong(COURSE_ID, id);
		fragment.setArguments(args);
		return fragment;

	}

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.vote_for_course_layout, null);

		ratingBarClarity = (RatingBar) rootView
				.findViewById(R.id.clarity_rating);
		ratingBarWorkload = (RatingBar) rootView
				.findViewById(R.id.workload_rating);
		ratingBarInterest = (RatingBar) rootView
				.findViewById(R.id.interest_rating);
		ratingBarSimplicity = (RatingBar) rootView.findViewById(R.id.simplicity_rating);
		ratingBarUsefulness = (RatingBar) rootView.findViewById(R.id.usefulness_rating);
		comment = (TextView) rootView.findViewById(R.id.editComment);
		sendComment = (ImageView) rootView.findViewById(R.id.commentArrow);
		sendComment.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				String what = "VoteForCourse";
				int courseId = getArguments().getInt(COURSE_ID);
				int userId = 0;
				int ratingClarity = (int) ratingBarClarity.getRating();
				int ratingWorkload = (int) ratingBarWorkload.getRating();
				int ratingInterest = (int) ratingBarInterest.getRating();
				int ratingSimplicity = (int) ratingBarSimplicity.getRating();
				int ratingUsefulness = (int) ratingBarUsefulness.getRating();
				String text = comment.getText().toString();

				PostVoteForCourseTask postVote = new PostVoteForCourseTask(what,
						courseId, userId, ratingClarity, ratingWorkload,
						ratingInterest, ratingSimplicity, ratingUsefulness, text);

				postVote.execute();

			}
		});
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.VOTE_COURSE);
		return rootView;
	}
}
