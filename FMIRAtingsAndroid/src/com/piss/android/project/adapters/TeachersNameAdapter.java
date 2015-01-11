package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;

public class TeachersNameAdapter extends BaseAdapter {

	private ArrayList<String> mDataSet;
	private LayoutInflater inflater;
	
	public TeachersNameAdapter(ArrayList<String> DataSet, Context mContext){
		this.mDataSet = DataSet;
		inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	

	@Override
	public String getItem(int position) {
		// TODO Auto-generated method stub
		return mDataSet.get(position);
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View view = convertView;
		ViewHolder holder;

		String teacherName = mDataSet.get(position);

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
		holder.name.setText(teacherName);
		return view;
	}

	private static class ViewHolder{
		TextView name;
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return 0;
	}


	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return mDataSet.size();
	}
}


