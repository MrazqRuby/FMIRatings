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

import android.os.AsyncTask;
import android.util.Log;

import com.piss.android.project.models.Comment;
import com.piss.android.project.models.Teacher;
import com.piss.android.project.utils.APIConnectionConstants;

public class GetCourseCommentsTask extends AsyncTask<Void, Void, ArrayList<Comment>> {
	private String auth;
	private String courseId;

	public GetCourseCommentsTask(String auth, String courseId) {
		this.auth = auth;
		this.courseId = courseId;
	}

	@Override
	protected ArrayList<Comment> doInBackground(Void... params) {
		String request = null;
		if(courseId != null ){
		request = APIConnectionConstants.API
				+ APIConnectionConstants.API_COURSE_COMMENTS;
		} else {
			request = APIConnectionConstants.API
					+ APIConnectionConstants.API_COURSE_COMMENTS + "/" + courseId;
		}

		HttpClient client = new DefaultHttpClient();
		Log.e("url", request);
		HttpGet get = new HttpGet(request);

		/* Set Authentication token in header */
		get.addHeader(APIConnectionConstants.AUTHENTICATION,
				APIConnectionConstants.BASIC + " " + auth);

		HttpResponse response;
		ArrayList<Comment> mList;
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

			// Parse json response
			mList = Comment.parseJSON(json);

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
