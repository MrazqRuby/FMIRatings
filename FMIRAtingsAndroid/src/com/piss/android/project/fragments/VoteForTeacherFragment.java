package com.piss.android.project.fragments;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RatingBar;
import android.widget.RatingBar.OnRatingBarChangeListener;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.tasks.PostVoteForTeacherTask;
import com.piss.android.project.utils.APIConnectionConstants;
import com.piss.android.project.utils.HeaderConstants;

public class VoteForTeacherFragment extends Fragment {

	private final static String TEACHER_ID = "id";
	private RatingBar ratingBarClarity;
	private RatingBar ratingBarEnthusiasum;
	private RatingBar ratingBarEvaluation;
	private RatingBar ratingBarSpeed;
	private RatingBar ratingBarScope;
	private TextView comment;
	private ImageView sendComment;

	public static VoteForTeacherFragment instance(Long id) {
		VoteForTeacherFragment fragment = new VoteForTeacherFragment();
		Bundle args = new Bundle();
		args.putLong(TEACHER_ID, id);
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
		
		
		 ratingBarClarity.setOnRatingBarChangeListener(new OnRatingBarChangeListener() {
		        public void onRatingChanged(RatingBar ratingBar, float rating,
		         boolean fromUser) {

		         Toast.makeText(getActivity(),"Your Selected Ratings  : " + String.valueOf(rating),Toast.LENGTH_LONG).show();

		        }
		       });
		comment = (TextView) rootView.findViewById(R.id.editComment);
		sendComment = (ImageView) rootView.findViewById(R.id.commentArrow);
		sendComment.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				SharedPreferences settings = getActivity().getSharedPreferences(APIConnectionConstants.PREFERENCES, Activity.MODE_PRIVATE);
				String auth = settings.getString(APIConnectionConstants.AUTHENTICATION, "");
				long teacherID = getArguments().getLong(TEACHER_ID);
				int userID = 0;
				int ratingClarity = (int) ratingBarClarity.getRating();
				int ratingEnthusiasum = (int) ratingBarEnthusiasum.getRating();
				int ratingEvaluation = (int) ratingBarEvaluation.getRating();
				int ratingSpeed = (int) ratingBarSpeed.getRating();
				int ratingScope = (int) ratingBarScope.getRating();
				String text = comment.getText().toString();

				PostVoteForTeacherTask postVote = new PostVoteForTeacherTask(auth,
						teacherID, userID, ratingClarity, ratingEnthusiasum,
						ratingEvaluation, ratingSpeed, ratingScope, text) {

					@Override
					protected void onPostExecute(Boolean result) {
						if (!result) {
							Toast.makeText(getActivity(),
									"Вие вече сте гласували", Toast.LENGTH_LONG)
									.show();
						}
					}

				};

				postVote.execute();

			}
		});
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.VOTE_TEACHER);
		return rootView;
	}
}
