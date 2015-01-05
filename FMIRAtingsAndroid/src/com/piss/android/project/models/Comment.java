package com.piss.android.project.models;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;

public class Comment {

	private long id;
	private String author;
	private String text;
	private String date;
	private long teacherID;

	public static ArrayList<Comment> parseJSON(JSONArray json) {
		ArrayList<Comment> comments = new ArrayList<Comment>();
		JSONObject item = null;
		Comment comment = null;
		try {
			for (int i = 0; i < json.length(); i++) {
				item = json.getJSONObject(i);
				comment = new Comment();
				comment.setId(item.getLong(APIConnectionConstants.ID));
				comment.setAuthor(item.getString(APIConnectionConstants.AUTHOR));
				comment.setText(item.getString(APIConnectionConstants.TEXT));
				comment.setDate(item.getString(APIConnectionConstants.DATE_CREATED));
				comment.setTeacherID(item.getLong(APIConnectionConstants.TEACHER_ID));
				
				comments.add(comment);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return comments;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getAuthor() {
		return author;
	}

	public void setAuthor(String author) {
		this.author = author;
	}

	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}

	public String getDate() {
		return date;
	}

	public void setDate(String date) {
		this.date = date;
	}

	public long getTeacherID() {
		return teacherID;
	}

	public void setTeacherID(long teacherID) {
		this.teacherID = teacherID;
	}
	
	
}
