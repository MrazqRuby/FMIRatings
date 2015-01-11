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
import com.piss.android.project.models.Course;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.models.Votes;
import com.piss.android.project.tasks.GetVoteForCourseTask;
import com.piss.android.project.tasks.GetVoteForTeacherTask;
import com.piss.android.project.utils.HeaderConstants;

public class CourseFragment  extends Fragment implements OnClickListener{

	private final static String COURSE = "course";
	private RatingBar ratingBarClarity;
	private RatingBar ratingBarWorkload;
	private RatingBar ratingBarInterest;
	private RatingBar ratingBarSimplicity;
	private RatingBar ratingBarUsefulness;
	
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
		Button materials = (Button) rootView.findViewById(R.id.materials_button);
		TextView course_name = (TextView) rootView.findViewById(R.id.course_name);
		TextView category = (TextView) rootView.findViewById(R.id.category);
		ratingBarClarity = (RatingBar) rootView
				.findViewById(R.id.clarity_rating);
		ratingBarInterest = (RatingBar) rootView
				.findViewById(R.id.interest_rating);
		ratingBarSimplicity = (RatingBar) rootView
				.findViewById(R.id.simplicity_rating);
		ratingBarUsefulness = (RatingBar) rootView.findViewById(R.id.usefulness_rating);
		ratingBarWorkload = (RatingBar) rootView.findViewById(R.id.workload_rating);
		
		Course course = ((Course) getArguments().getSerializable(COURSE));
		course_name.setText(course.getName());
		category.setText(course.getCategory());		
		
		vote.setOnClickListener(this);
		teachers.setOnClickListener(this);
		comments.setOnClickListener(this);
		materials.setOnClickListener(this);
		
		((MainActivity) getActivity()).setUpNavigationToolbar();
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.COURSE);
		
		ratingBarClarity.setEnabled(false);
		ratingBarInterest.setEnabled(false);
		ratingBarSimplicity.setEnabled(false);
		ratingBarWorkload.setEnabled(false);
		ratingBarUsefulness.setEnabled(false);
		getVotes();
		return rootView;
	}
	
	private void getVotes() {
		Course course = ((Course) getArguments().getSerializable(COURSE));
		GetVoteForCourseTask getVotesTask = new GetVoteForCourseTask(String.valueOf(course.getId())) {

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
						}else if(criterion.equals("Колко е интересен курса")){
							ratingBarInterest.setRating(vote.getAverage());
						}else if(criterion.equals("Сложност на курса")){
							ratingBarSimplicity.setRating(vote.getAverage());
						}else if(criterion.equals("Полезност на материала")){
							ratingBarUsefulness.setRating(vote.getAverage());
						}else if(criterion.equals("Натоваренос")){
							ratingBarWorkload.setRating(vote.getAverage());
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
		case R.id.materials_button:
			long courseIdForMaterials = ((Course) getArguments().getSerializable(COURSE)).getId();
			MaterialsListFragment materials = MaterialsListFragment.getInstance(courseIdForMaterials);
			((MainActivity) getActivity()).addFragment(materials);
		default:
			break;
		}
	}

}
