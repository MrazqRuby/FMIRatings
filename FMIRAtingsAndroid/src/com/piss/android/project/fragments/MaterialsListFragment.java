package com.piss.android.project.fragments;

import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.widget.SearchView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.Toast;

import com.piss.android.project.activities.MainActivity;
import com.piss.android.project.adapters.MaterialsAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Course;
import com.piss.android.project.models.Material;
import com.piss.android.project.tasks.DownloadFileTask;
import com.piss.android.project.tasks.GetMaterialsTask;
import com.piss.android.project.utils.HeaderConstants;

public class MaterialsListFragment extends Fragment {

	ListView mListView;
	ArrayList<Material> materialList;
	public static final String MATERIAL = "Material";
	
	public static MaterialsListFragment getInstance(long courseId){
		MaterialsListFragment fragment  = new MaterialsListFragment();
		Bundle args = new Bundle();
		args.putLong(MATERIAL, courseId);
		fragment.setArguments(args);
		return fragment;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);

		mListView = (ListView) rootView.findViewById(R.id.list_view);
	
		mListView.setAdapter(null);
		
		
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.MATERIALS);

		mListView.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				int fileId = materialList.get(position).getId();
				String filename = materialList.get(position).getFilename();
				DownloadFileTask download = new DownloadFileTask( fileId, filename, getActivity());
				download.execute();
				
			}
		});
		int id = (int)getArguments().getLong(MATERIAL);
		
		GetMaterialsTask getMaterialsTask = new GetMaterialsTask(id) {

			@Override
			protected void onPostExecute(ArrayList<Material> result) {
				if (result != null) {
					materialList = result;
					Log.i("DEBUG", "onPostExecute courses");
					MaterialsAdapter adapter = new MaterialsAdapter(result, getActivity());

					mListView.setAdapter(adapter);
				} else {
					Toast.makeText(getActivity(), "Server Error",
							Toast.LENGTH_SHORT).show();
				}

			}

		};
		getMaterialsTask.execute();

		setHasOptionsMenu(true);
		((MainActivity) getActivity()).getSupportActionBar().setTitle(HeaderConstants.COURSES);
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
