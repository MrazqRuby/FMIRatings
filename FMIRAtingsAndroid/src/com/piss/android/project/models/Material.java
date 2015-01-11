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

		String file = null;
		String text = null;
		for (int i = 0; i < json.length(); i++) {
			material = new Material();
			try {
				item = json.getJSONObject(i);
				material.setId(item.getInt(APIConnectionConstants.ID));
				text = item.getString(APIConnectionConstants.FILENAME);
				file = item.getString(APIConnectionConstants.FILENAME)
						.substring(1, text.length() - 1);
				material.setFilename(file);
				material.setCourseId(item
						.getInt(APIConnectionConstants.COURSEID));

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
