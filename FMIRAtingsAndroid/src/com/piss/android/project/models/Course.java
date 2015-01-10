package com.piss.android.project.models;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;

public class Course {
	private long id;
	private String name;
	private String description;
	private ArrayList<String> teachers;

	public static ArrayList<Course> parseFromJSON(JSONArray json) {
		ArrayList<Course> courses = new ArrayList<Course>();
		Course course = null;
		JSONObject item = null;
		JSONArray teachers = null;
		ArrayList<String> teachersArray = null;

		for (int i = 0; i < json.length(); i++) {
			course = new Course();
			try {
				item = json.getJSONObject(i);
				course.setId(item.getLong(APIConnectionConstants.ID));
				course.setDescription(item
						.getString(APIConnectionConstants.DESCRIPTION));
				course.setName(item.getString(APIConnectionConstants.NAME));
				teachers = item.getJSONArray(APIConnectionConstants.TEACHERS);
				
				if (teachers != null && teachers.length() > 0) {
					teachersArray = new ArrayList<String>();
					for (int j = 0; j < teachers.length(); j++) {
						teachersArray.add(teachers.getString(j));
					}
					course.setTeachers(teachersArray);
				}
				
				courses.add(course);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return null;
			}
		}
		return courses;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public ArrayList<String> getTeachers() {
		return teachers;
	}

	public void setTeachers(ArrayList<String> teachers) {
		this.teachers = teachers;
	}

}