package com.piss.android.project.tasks;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import android.net.ParseException;
import android.os.AsyncTask;

import com.piss.android.project.utils.APIConnectionConstants;

public class PostVoteForCourseTask extends AsyncTask<Void, Void, Boolean>{

	private String what;
	private int courseId;
	private int userId;
	private int clarity;
	private int workload;
	private int interests;
	private int simplicity;
	private int usefulness;
	private String comment;
	
	public PostVoteForCourseTask(String what, int courseId,
								int userId, int clarity, 
								int workload, int interests,
								int simplicity, int usefulness,
								String comment 
								){
		this.what = what;
		this.courseId = courseId;
		this.userId = userId;
		this.clarity = clarity;
		this.workload = workload;
		this.interests = interests;
		this.simplicity = simplicity;
		this.usefulness = usefulness;
		this.comment = comment;
	}
	@Override
	protected Boolean doInBackground(Void... params) {

		HttpClient client = new DefaultHttpClient();
		String url = APIConnectionConstants.API + what;
		
		HttpResponse response = null;

		HttpPost httpPost;
		List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
		try {
			httpPost = new HttpPost(url);
			nameValuePairs.clear();

			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.COURSE_ID, String.valueOf(courseId)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.USER_ID, String.valueOf(userId)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.CLARITY, String.valueOf(clarity)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.WORKLOAD, String.valueOf(workload)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.INTEREST, String.valueOf(interests)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.SIMPLICITY, String.valueOf(simplicity)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.USEFULNESS, String.valueOf(usefulness)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.COMMENT, comment));

			try {
				httpPost.setEntity(new UrlEncodedFormEntity(nameValuePairs));

			} catch (UnsupportedEncodingException e) {
				e.printStackTrace();
				return false;
			}

			try {
				response = client.execute(httpPost);

			} catch (ClientProtocolException e) {
				e.printStackTrace();
				return false;
			} catch (IOException e) {
				e.printStackTrace();
				return false;
			}

			String jsonResponse;
			try {
				jsonResponse = EntityUtils.toString(response.getEntity());
				JSONObject json = new JSONObject(jsonResponse);

				int code = (int) json.getJSONObject(
						APIConnectionConstants.STATUS).getLong(
						APIConnectionConstants.CODE);

				if (code != 200 && code != 201) {
					return false;
				}

			} catch (ClientProtocolException e) {
				e.printStackTrace();
				return false;
			} catch (IOException e) {
				e.printStackTrace();
				return false;
			} catch (JSONException e) {
				e.printStackTrace();
				return false;
			}

		} catch (ParseException e) {
			e.printStackTrace();
			return false;
		}
		return true;

	}
}
