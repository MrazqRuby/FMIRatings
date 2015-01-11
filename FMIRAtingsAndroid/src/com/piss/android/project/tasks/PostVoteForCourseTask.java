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
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicHeader;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import android.net.ParseException;
import android.os.AsyncTask;
import android.util.Log;

import com.piss.android.project.utils.APIConnectionConstants;

public class PostVoteForCourseTask extends AsyncTask<Void, Void, Boolean>{

	private String auth;
	private int courseId;
	private int userId;
	private int clarity;
	private int workload;
	private int interests;
	private int simplicity;
	private int usefulness;
	private String comment;
	
	public PostVoteForCourseTask(String auth, int courseId,
								int userId, int clarity, 
								int workload, int interests,
								int simplicity, int usefulness,
								String comment 
								){
		this.auth = auth;
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
		String url = APIConnectionConstants.API +
				APIConnectionConstants.API_VOTE_FOR_COURSE;
		
		JSONObject json = new JSONObject();
		HttpResponse response = null;

		HttpPost httpPost;
		//List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
		try {
			httpPost = new HttpPost(url);
			json.put(APIConnectionConstants.COURSE_ID, courseId);
			json.put(APIConnectionConstants.USER_ID, userId);
			json.put(APIConnectionConstants.CLARITY, clarity);
			json.put(APIConnectionConstants.WORKLOAD, workload);
			json.put(APIConnectionConstants.INTEREST, interests);
			json.put(APIConnectionConstants.SIMPLICITY, simplicity);
			json.put(APIConnectionConstants.USEFULNESS, usefulness);
			json.put(APIConnectionConstants.COMMENT, comment);
			
			Log.i("DEBUG", json.toString());

			StringEntity se = new StringEntity(json.toString());
			httpPost.addHeader(APIConnectionConstants.AUTHENTICATION,APIConnectionConstants.BASIC + " " + auth);
//			nameValuePairs.clear();
//
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.COURSE_ID, String.valueOf(courseId)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.USER_ID, String.valueOf(userId)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.CLARITY, String.valueOf(clarity)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.WORKLOAD, String.valueOf(workload)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.INTEREST, String.valueOf(interests)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.SIMPLICITY, String.valueOf(simplicity)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.USEFULNESS, String.valueOf(usefulness)));
//			nameValuePairs.add(new BasicNameValuePair(
//					APIConnectionConstants.COMMENT, comment));
			se.setContentType(new BasicHeader(HTTP.CONTENT_TYPE,
					"application/json"));
			httpPost.setEntity(se);

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
				JSONObject jsonObj = new JSONObject(jsonResponse);

				int code = response.getStatusLine().getStatusCode();
				Log.d("DEBUG","code: " +code);
				Log.d("DEBUG", jsonResponse);
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
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (UnsupportedEncodingException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		return true;
	}

}

