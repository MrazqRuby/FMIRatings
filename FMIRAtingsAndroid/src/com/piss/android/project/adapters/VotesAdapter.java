package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.RatingBar;
import android.widget.TextView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Votes;

public class VotesAdapter extends BaseAdapter {

	private ArrayList<Votes> mDataSet;
	private LayoutInflater inflater;
	
	public VotesAdapter(ArrayList<Votes> mDataSet, Context mContext){
		this.mDataSet = mDataSet;
		inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	@Override
	public int getCount() {
		
		return mDataSet.size();
	}

	@Override
	public Votes getItem(int position) {
		// TODO Auto-generated method stub
		return mDataSet.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return mDataSet.get(position).getCriterionId();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View view = convertView;
		ViewHolder holder;

		Votes vote = mDataSet.get(position);

		if (view == null) {

			view = inflater.inflate(R.layout.vote_item_layout, null);
			holder = new ViewHolder();

			// name
			holder.name = (TextView) view.findViewById(R.id.item_name);
			holder.ratingBar = (RatingBar) view.findViewById(R.id.rating);

			view.setTag(holder);

		} else {
			holder = (ViewHolder) view.getTag();
		}

		// populate category data
		holder.name.setText(vote.getCriterionName());
		holder.ratingBar.setRating(vote.getAverage());
		
		return view;
	}

	private static class ViewHolder{
		TextView name;
		RatingBar ratingBar;
	}
}
