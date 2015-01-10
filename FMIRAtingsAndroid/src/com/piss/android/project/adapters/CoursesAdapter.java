package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;

public class CoursesAdapter extends BaseAdapter {

	private ArrayList<Course> mDataSet;
	private LayoutInflater inflater;
	
	public CoursesAdapter(ArrayList<Course> mDataSet, Context mContext){
		this.mDataSet = mDataSet;
		inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	@Override
	public int getCount() {
		
		return mDataSet.size();
	}

	@Override
	public Course getItem(int position) {
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

		Course course = mDataSet.get(position);

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
		holder.name.setText(course.getName());
		return view;
	}

	private static class ViewHolder{
		TextView name;
	}
}
