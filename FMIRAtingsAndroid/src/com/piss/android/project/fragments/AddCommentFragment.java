package com.piss.android.project.fragments;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.tasks.PostCommentTask;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RatingBar;
import android.widget.TextView;

public class AddCommentFragment extends Fragment {

	private final static String FROM = "from";
	private RatingBar ratingBar;
	private TextView comment;
	private ImageView sendComment;

	public static AddCommentFragment instance(String from) {
		AddCommentFragment fragment = new AddCommentFragment();
		Bundle args = new Bundle();
		args.putString(FROM, from);
		fragment.setArguments(args);
		return fragment;

	}

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.add_comment_layout, null);
		
		ratingBar = (RatingBar) rootView.findViewById(R.id.addRating);
		comment = (TextView) rootView.findViewById(R.id.editComment);
		sendComment = (ImageView) rootView.findViewById(R.id.commentArrow);
		sendComment.setOnClickListener ( new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO add comment task 
				String from = getArguments().getString(FROM);
				String text = comment.getText().toString();
				int rating = (int) ratingBar.getRating();
				PostCommentTask postCommentTask = new PostCommentTask( from, text, rating);
				postCommentTask.execute();
				
			}
		});
		
		
		return rootView;
	}
}
