package com.piss.android.project.models;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.piss.android.project.utils.APIConnectionConstants;

public class Votes {
	int criterionId;
	String criterionName;
	long average;
	
	public static ArrayList<Votes> parseJSON(JSONObject json) {
		ArrayList<Votes> votes = new ArrayList<Votes>();
		JSONObject item = null;
		Votes vote = null;
		
		try {
			JSONArray jsonArray = json.getJSONArray(APIConnectionConstants.VOTES);
			for (int i = 0; i < jsonArray.length(); i++) {
				item = jsonArray.getJSONObject(i);
				vote = new Votes();
				vote.setCriterionId(item.getInt(APIConnectionConstants.CRITERION_ID));
				vote.setCriterionName(item.getString(APIConnectionConstants.CRITERION_NAME));
				vote.setAverage(item.getLong(APIConnectionConstants.AVARAGE));
				
				votes.add(vote);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return votes;
	}

	public int getCriterionId() {
		return criterionId;
	}

	public void setCriterionId(int criterionId) {
		this.criterionId = criterionId;
	}

	public String getCriterionName() {
		return criterionName;
	}

	public void setCriterionName(String criterionName) {
		this.criterionName = criterionName;
	}

	public long getAverage() {
		return average;
	}

	public void setAverage(long average) {
		this.average = average;
	}
}
