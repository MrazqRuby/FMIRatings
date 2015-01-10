package com.piss.android.project.tasks;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

import android.net.ParseException;
import android.os.AsyncTask;
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

import com.piss.android.project.utils.APIConnectionConstants;


public class PostVoteForTeacherTask extends AsyncTask<Void, Void, Boolean> {
	
	private int teacherId;
	private int userId;
	private int clarity;
	private int enthusiasm;
	private int evaluation;
	private int speed;
	private int scope;
	private String comment;
	
	public PostVoteForTeacherTask(int teacherId, int userId, int clarity, int enthusiasm, int evaluation, int speed, 
			int scope, String comment){
		this.teacherId = teacherId;
		this.userId = userId;
		this.clarity = clarity;
		this.comment = comment;
		this.enthusiasm = enthusiasm;
		this.evaluation = evaluation;
		this.scope = scope;
		this.comment = comment;
	}
	
	@Override
	protected Boolean doInBackground(Void... params) {
		
		HttpClient client = new DefaultHttpClient();
		String url = APIConnectionConstants.API + APIConnectionConstants.API_VOTE_FOR_TEACHER;
		
		HttpResponse response = null;
		HttpPost httpPost;
		List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
		try {
			httpPost = new HttpPost(url);
			nameValuePairs.clear();
			
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.TEACHER_ID, String.valueOf(teacherId)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.USER_ID, String.valueOf(userId)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.ENTHUSUASM, String.valueOf(enthusiasm)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.CLARITY, String.valueOf(clarity)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.EVALUATION, String.valueOf(evaluation)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.SCOPE, String.valueOf(scope)));
			nameValuePairs.add(new BasicNameValuePair(
					APIConnectionConstants.SPEED, String.valueOf(speed)));
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
