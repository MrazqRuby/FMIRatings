package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Material;

public class MaterialsAdapter extends BaseAdapter {
	private ArrayList<Material> mDataSet;
	private LayoutInflater inflater;

	public MaterialsAdapter(ArrayList<Material> mDataSet, Context mContext) {
		this.mDataSet = mDataSet;
		inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}

	@Override
	public int getCount() {

		return mDataSet.size();
	}

	@Override
	public Material getItem(int position) {
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

		Material material = mDataSet.get(position);

		if (view == null) {

			view = inflater.inflate(R.layout.recyclerview_item, null);
			holder = new ViewHolder();

			holder.name = (TextView) view.findViewById(R.id.item_name);

			view.setTag(holder);

		} else {
			holder = (ViewHolder) view.getTag();
		}

		holder.name.setText(material.getFilename());

		return view;
	}

	private static class ViewHolder {
		TextView name;
	}
}

