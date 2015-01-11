package com.piss.android.project.models;

import java.io.Serializable;
import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;


public class Teacher implements Serializable {


	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private long id;
	private String name;
	private String department;
	public String getDepartment() {
		return department;
	}

	public void setDepartment(String department) {
		this.department = department;
	}

	private ArrayList<Course> courses;
	private ArrayList<Comment> comments;

	public static ArrayList<Teacher> parseFromJSON(JSONArray json) {
		ArrayList<Teacher> teachers = new ArrayList<Teacher>();
		Teacher teacher = null;
		JSONObject item = null;
		JSONArray coursesJSON = null;
		JSONArray commentsJSON =null;
		try {
			for (int i = 0; i < json.length(); i++) {

				item = json.getJSONObject(i);
				teacher = new Teacher();
				
				teacher.setId(item.getLong(APIConnectionConstants.ID));
				teacher.setName(item.getString(APIConnectionConstants.NAME));
				teacher.setDepartment(item.getString(APIConnectionConstants.DEPARTMENT));
				coursesJSON = item.getJSONArray(APIConnectionConstants.COURSES);
				commentsJSON = item.getJSONArray(APIConnectionConstants.COMMENTS);
				teacher.setCourses(Course.parseFromJSON(coursesJSON));
				teacher.setComments(Comment.parseJSON(commentsJSON));
				
				teachers.add(teacher);

			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return teachers;
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

	public ArrayList<Course> getCourses() {
		return courses;
	}

	public void setCourses(ArrayList<Course> courses) {
		this.courses = courses;
	}

	public ArrayList<Comment> getComments() {
		return comments;
	}

	public void setComments(ArrayList<Comment> comments) {
		this.comments = comments;
	}

}
