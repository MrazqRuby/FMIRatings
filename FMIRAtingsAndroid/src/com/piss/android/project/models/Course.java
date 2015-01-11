package com.piss.android.project.models;

import java.io.Serializable;
import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;

public class Course implements Serializable {
	private static final long serialVersionUID = 1L;
	private long id;
	private String name;
	private String description;
	private String category;
	private ArrayList<String> teachers;
	private ArrayList<Comment> comments;

	public static ArrayList<Course> parseFromJSON(JSONArray json) {
		ArrayList<Course> courses = new ArrayList<Course>();
		Course course = null;
		JSONObject item = null;
		JSONArray teachers = null;
		JSONArray coursesComents = null;
		ArrayList<String> teachersArray = null;

		for (int i = 0; i < json.length(); i++) {
			course = new Course();
			try {
				item = json.getJSONObject(i);
				course.setId(item.getLong(APIConnectionConstants.ID));
				course.setDescription(item
						.getString(APIConnectionConstants.DESCRIPTION));
				course.setDescription(item
						.getString(APIConnectionConstants.CATEGORY));
				course.setName(item.getString(APIConnectionConstants.NAME));
				teachers = item.getJSONArray(APIConnectionConstants.TEACHERS);
				coursesComents = item.getJSONArray(APIConnectionConstants.COMMENTS);
				
				if (teachers != null && teachers.length() > 0) {
					teachersArray = new ArrayList<String>();
					for (int j = 0; j < teachers.length(); j++) {
						teachersArray.add(teachers.getString(j));
					}
					course.setTeachers(teachersArray);
				}
				course.setComments(Comment.parseJSON(coursesComents));
				courses.add(course);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return null;
			}
		}
		return courses;
	}

	public ArrayList<Comment> getComments() {
		return comments;
	}

	public void setComments(ArrayList<Comment> comments) {
		this.comments = comments;
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

	public String getCategory() {
		return category;
	}

	public void setCategory(String category) {
		this.category = category;
	}

}
