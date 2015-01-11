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
import org.json.JSONObject;

import android.os.AsyncTask;
import android.util.Log;

import com.piss.android.project.models.Teacher;
import com.piss.android.project.models.Votes;
import com.piss.android.project.utils.APIConnectionConstants;

public class GetVoteForTeacherTask extends
		AsyncTask<Void, Void, ArrayList<Votes>> {

	// private String auth;
	private Long teacherId;

	public GetVoteForTeacherTask(Long teacherId) {
		// this.auth = auth;
		this.teacherId = teacherId;
	}

	@Override
	protected ArrayList<Votes> doInBackground(Void... params) {
		String request = null;

		request = APIConnectionConstants.API
				+ APIConnectionConstants.API_VOTE_FOR_TEACHER + "/" + teacherId;

		HttpClient client = new DefaultHttpClient();
		Log.e("url", request);
		HttpGet get = new HttpGet(request);

		/* Set Authentication token in header */
		// get.addHeader(APIConnectionConstants.AUTHENTICATION,
		// APIConnectionConstants.BASIC + " " + auth);

		HttpResponse response;
		ArrayList<Votes> mList;
		try {
			response = client.execute(get);

			// Check status code from header
			int code = response.getStatusLine().getStatusCode();
			if (code != 200 && code != 201) {
				return null;
			}
			// Get response string
			String jsonResponse = EntityUtils.toString(response.getEntity());
			JSONObject json = new JSONObject(jsonResponse);

			// Parse json response
			mList = Votes.parseJSON(json);

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
