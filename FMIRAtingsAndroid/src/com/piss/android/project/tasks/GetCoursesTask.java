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

import android.os.AsyncTask;
import android.util.Log;

public class GetCoursesTask extends AsyncTask<Void, Void, ArrayList<Course>> {

	@Override
	protected ArrayList<Course> doInBackground(Void... params) {
		String request = "";
		HttpClient client = new DefaultHttpClient();
		Log.e("url", request);
		HttpGet get = new HttpGet(request);

		HttpResponse response;
		ArrayList<Course> mList;
		try {
			response = client.execute(get);

			String jsonResponse = EntityUtils.toString(response.getEntity());
			JSONArray json = new JSONArray(jsonResponse);

			//TODO:get status code from header

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
