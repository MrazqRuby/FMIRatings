package com.piss.android.project.fragments;

import java.util.ArrayList;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.piss.android.project.adapters.TeachersAdapter;
import com.piss.android.project.adapters.TeachersNameAdapter;
import com.piss.android.project.fmiratings.R;
import com.piss.android.project.models.Teacher;

public class TeachersFragment  extends Fragment{

	private final static String TEACHERS = "teachers";
	public static TeachersFragment getInstance(ArrayList<String> teachers){
		TeachersFragment fragment  = new TeachersFragment();
		Bundle args = new Bundle();
		args.putStringArrayList(TEACHERS, teachers);
		fragment.setArguments(args);
		return fragment;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater,
			@Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
		ArrayList<String> teachers = (ArrayList<String>) getArguments().getStringArrayList(TEACHERS);
		View rootView = inflater.inflate(R.layout.teachers_list_fragment, null);
		
		ListView myListView = (ListView) rootView.findViewById(R.id.list_view);
		TeachersNameAdapter myAdapter = new TeachersNameAdapter(teachers, getActivity());
		
		myListView.setAdapter(myAdapter);
		return rootView;
	}
}
