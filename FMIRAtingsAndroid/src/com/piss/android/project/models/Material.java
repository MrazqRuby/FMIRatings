package com.piss.android.project.models;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;

public class Material {
	private int id;
	private String filename;
	private int courseId;
	
	public static ArrayList<Material> parseFromJSON(JSONArray json) {
		ArrayList<Material> materials = new ArrayList<Material>();
		JSONObject item = null;	
		Material material = null;

		for (int i = 0; i < json.length(); i++) {
			material = new Material();
			try {
				item = json.getJSONObject(i);
				material.setId(item.getInt(APIConnectionConstants.ID));
				material.setFilename(item
						.getString(APIConnectionConstants.FILENAME));
				material.setCourseId(item.getInt(APIConnectionConstants.COURSEID));
				
				materials.add(material);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return null;
			}
		}
		return materials;
	}
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getFilename() {
		return filename;
	}
	public void setFilename(String filename) {
		this.filename = filename;
	}
	public int getCourseId() {
		return courseId;
	}
	public void setCourseId(int courseId) {
		this.courseId = courseId;
	}
	
	
}
