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

public class RegisterTask extends AsyncTask<Void, Void, Boolean> {

	private String email;
	private String password;
	private String name;
	private String realname;
	private int course;
	private int group; 
	private int year;
	private String major;

	public RegisterTask(String name, String realName, String email, String password, int course, int group, int year, String major ) {
		this.email = email;
		this.password = password;
		this.course = course;
		this.name= name;
		this.group = group;
		this.year = year;
		this.major = major;
	}

	@Override
	protected Boolean doInBackground(Void... params) {
		HttpClient client = new DefaultHttpClient();
		String url = APIConnectionConstants.API + APIConnectionConstants.API_REGISTRATION;
		JSONObject json = new JSONObject();
		HttpResponse response = null;

		HttpPost httpPost;
		//List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
		try {
			httpPost = new HttpPost(url);
			json.put(APIConnectionConstants.ID, 0);
			json.put("Name", name);
			json.put("RealName", realname);
			json.put("Password", password);
			json.put("Email", email);
			json.put("Course", course);
			json.put("Group", group);
			json.put("GraduationYear", year);
			json.put("Major", major);

			StringEntity se = new StringEntity(json.toString());

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

			//String jsonResponse;
			//try {
				//jsonResponse = EntityUtils.toString(response.getEntity());
				//JSONObject jsonObj = new JSONObject(jsonResponse);
				int code =response.getStatusLine().getStatusCode();
//				int code = (int) jsonObj.getJSONObject(
//						APIConnectionConstants.STATUS).getLong(
//						APIConnectionConstants.CODE);

				Log.e("STATUS CODE", "code: " + code);
				if (code != 200 && code != 201) {
					return false;
				}

//			} catch (ClientProtocolException e) {
//				e.printStackTrace();
//				return false;
//			} catch (IOException e) {
//				e.printStackTrace();
//				return false;
//			} catch (JSONException e) {
//				e.printStackTrace();
//				return false;
//			}

		} catch (ParseException e) {
			e.printStackTrace();
			return false;
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
			return false;
		} catch (UnsupportedEncodingException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
			return false;
		}
		return true;

	}
}
