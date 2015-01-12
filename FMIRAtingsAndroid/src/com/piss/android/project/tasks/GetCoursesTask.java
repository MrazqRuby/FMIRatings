package com.piss.android.project.tasks;

import java.io.IOException;
import java.util.ArrayList;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;

import com.piss.android.project.models.Course;
import com.piss.android.project.utils.APIConnectionConstants;

import android.os.AsyncTask;
import android.util.Log;

public class GetCoursesTask extends AsyncTask<Void, Void, ArrayList<Course>> {

	private String auth;
	private String id;

	public GetCoursesTask(String auth, String id) {
		this.auth = auth;
		this.id = id;
	}
	
	

	@Override
	protected ArrayList<Course> doInBackground(Void... params) {
		
		String  request = null;
		if (id == null) {
		request = APIConnectionConstants.API
				+ APIConnectionConstants.API_COURSES;
		}
		else{
			request = APIConnectionConstants.API
					+ APIConnectionConstants.API_COURSES +"/" + id;
		}
		HttpClient client = new DefaultHttpClient();
		Log.e("url", request);
		HttpGet get = new HttpGet(request);
		
		/* Set Authentication token in header */
//		get.addHeader(APIConnectionConstants.AUTHENTICATION,
//				APIConnectionConstants.BASIC + " " + auth);

		HttpResponse response;
		ArrayList<Course> mList;
		try {
			response = client.execute(get);

			// Check status code from header
			int code = response.getStatusLine().getStatusCode();
			if (code != 200 && code != 201) {
				return null;
			}
			// Get response string
			String jsonResponse = EntityUtils.toString(response.getEntity());
			JSONArray json = new JSONArray(jsonResponse);
			Log.e("json", jsonResponse);

			// Parse json response
			mList = Course.parseFromJSON(json);

		} catch (ClientProtocolException e) {

			e.printStackTrace();
			return null;
		} catch (IOException e) {

			e.printStackTrace();
			return null;
		} catch (JSONException e) {

			e.printStackTrace();
			return null;
		}
		return mList;
	}

}
