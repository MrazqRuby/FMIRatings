package com.piss.android.project.adapters;

import java.util.ArrayList;

import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.piss.android.project.adapters.CoursesAdapter.ViewItemHolder;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.models.Teacher;

public class TeachersAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
	private static final int TYPE_ITEM = 1;
	public ArrayList<Teacher> mDataset;

	// Provide a suitable constructor (depends on the kind of dataset)
	public TeachersAdapter(ArrayList<Teacher> result) {

		this.mDataset = result;

	}

	public TeachersAdapter() {
		super();
	}

	// Create new views (invoked by the layout manager)
	@Override
	public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent,
			int viewType) {

		RecyclerView.ViewHolder vh = null;
		try {
			View v = null;
			if (viewType == TYPE_ITEM) {
				// create a new view
				v = (View) LayoutInflater.from(parent.getContext()).inflate(
						R.layout.recyclerview_item, parent, false);
				vh = new ViewItemHolder(v);

			}

		} catch (RuntimeException e) {
			Log.e("RuntimeException", "There is no type that matches the type "
					+ viewType + "  make sure your using types correctly");
			e.printStackTrace();
		}
		return vh;
	}

	// Replace the contents of a view (invoked by the layout manager)
	@Override
	public void onBindViewHolder(final RecyclerView.ViewHolder holder,
			final int position) {

		if (holder instanceof ViewItemHolder) {
			// cast holder to ViewItemHolder and set data
			// mDataset items begin from index 0 and so we decrement position
			// because we have header on position 0
			final Teacher item = mDataset.get(position );
			if (item != null) {

				/* Populate item information */
				((ViewItemHolder) holder).folderName.setText(item.getName());

				((ViewItemHolder) holder).itemView.setTag(holder);

			}

		}
	}

	@Override
	public void setHasStableIds(boolean hasStableIds) {
		super.setHasStableIds(true);
	}

	@Override
	public long getItemId(int position) {

		if (position > 0) {
			long h = (mDataset.get(position - 1)).getName().hashCode();
			return h;
		} else {
			return -1;
		}
	}

	// Return the size of your dataset (invoked by the layout manager)
	@Override
	public int getItemCount() {
		return mDataset.size();
	}

	@Override
	public int getItemViewType(int position) {
		return TYPE_ITEM;
	}

	// ------------------------------- Item
	// Holder---------------------------------------//
	// Provide a reference to the views for each data item
	// Complex data items may need more than one view per item, and
	// you provide access to all the views for a data item in a view holder
	public static class ViewItemHolder extends RecyclerView.ViewHolder {

		public View recycleViewItem;
		public TextView folderName;

		public ViewItemHolder(View v) {
			super(v);
			recycleViewItem = v;

		}

	}

	public static class ViewHeaderHolder extends RecyclerView.ViewHolder {
		// each data item is just a string in this case
		public View recycleViewHeader;

		public ViewHeaderHolder(View v) {
			super(v);
			recycleViewHeader = v;

		}

	}
}
