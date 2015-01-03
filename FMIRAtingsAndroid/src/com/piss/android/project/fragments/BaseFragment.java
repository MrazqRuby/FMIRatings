package com.piss.android.project.fragments;


import com.piss.android.project.fmiratings.R;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.piss.android.project.adapters.ListAdapter;

public class BaseFragment extends Fragment{

	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_layout, null);
		
		RecyclerView recyclerView = (RecyclerView) rootView.findViewById(R.id.recycler_view);
		recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
		
		
		ListAdapter adapter = new ListAdapter(null);
		
		recyclerView.setAdapter(adapter);
		
		return rootView;
	}

	
}
