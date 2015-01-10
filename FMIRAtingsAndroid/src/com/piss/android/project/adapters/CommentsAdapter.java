package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Comment;

public class CommentsAdapter extends BaseAdapter{
	private ArrayList<Comment> mDataSet;
	private LayoutInflater inflater;
	
	public CommentsAdapter(ArrayList<Comment> mDataSet, Context mContext){
		this.mDataSet = mDataSet;
		inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	@Override
	public int getCount() {
		
		return mDataSet.size();
	}

	@Override
	public Comment getItem(int position) {
		// TODO Auto-generated method stub
		return mDataSet.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return mDataSet.get(position).getId();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View view = convertView;
		ViewHolder holder;

		Comment comment = mDataSet.get(position);

		if (view == null) {

			view = inflater.inflate(R.layout.recyclerview_item, null);
			holder = new ViewHolder();

			// name
			holder.name = (TextView) view.findViewById(R.id.item_name);

			view.setTag(holder);

		} else {
			holder = (ViewHolder) view.getTag();
		}

		// populate category data
		holder.name.setText(comment.getAuthor());
		return view;
	}

	private static class ViewHolder{
		TextView name;
	}
}
