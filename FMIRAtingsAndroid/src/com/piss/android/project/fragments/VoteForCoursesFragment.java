package com.piss.android.project.fragments;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.widget.SearchView;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.RatingBar.OnRatingBarChangeListener;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.tasks.PostVoteForCourseTask;
import com.piss.android.project.utils.APIConnectionConstants;
import com.piss.android.project.utils.HeaderConstants;

public class VoteForCoursesFragment extends Fragment {
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
		ratingBarSimplicity = (RatingBar) rootView
				.findViewById(R.id.simplicity_rating);
		ratingBarUsefulness = (RatingBar) rootView
				.findViewById(R.id.usefulness_rating);
		
		comment = (TextView) rootView.findViewById(R.id.editComment);
		sendComment = (ImageView) rootView.findViewById(R.id.commentArrow);
		sendComment.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				SharedPreferences settings = getActivity()
						.getSharedPreferences(
								APIConnectionConstants.PREFERENCES,
								Activity.MODE_PRIVATE);
				String auth = settings.getString(
						APIConnectionConstants.AUTHENTICATION, "");

				int courseId = (int) getArguments().getLong(COURSE_ID);
				int userId = 0;
				int ratingClarity = (int) ratingBarClarity.getRating();
				int ratingWorkload = (int) ratingBarWorkload.getRating();
				int ratingInterest = (int) ratingBarInterest.getRating();
				int ratingSimplicity = (int) ratingBarSimplicity.getRating();
				int ratingUsefulness = (int) ratingBarUsefulness.getRating();
				String text = comment.getText().toString();

				PostVoteForCourseTask postVote = new PostVoteForCourseTask(
						auth, courseId, userId, ratingClarity, ratingWorkload,
						ratingInterest, ratingSimplicity, ratingUsefulness,
						text) {

					@Override
					protected void onPostExecute(Boolean result) {
						if (!result) {
							Toast.makeText(getActivity(),
									"Вие вече сте гласували", Toast.LENGTH_LONG)
									.show();
						}else{
							Toast.makeText(getActivity(),
									"Вие гласувахте успешно", Toast.LENGTH_LONG)
									.show();
							
						}
						
						((MainActivity) getActivity()).removeFragmentsInclusive(VoteForCoursesFragment.class.getSimpleName());
					}
				};

				postVote.execute();

			}
		});
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.VOTE_COURSE);
		setHasOptionsMenu(true);
		return rootView;
	}
	
	@Override
	public void onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		if (getActivity().isFinishing()) {
			return;
		}
		// getActivity().invalidateOptionsMenu();
		final MenuItem searchItem = menu.findItem(R.id.action_search);
		SearchView mSearchView = (SearchView) MenuItemCompat
				.getActionView(searchItem);
		searchItem.setVisible(false);
	}
}
