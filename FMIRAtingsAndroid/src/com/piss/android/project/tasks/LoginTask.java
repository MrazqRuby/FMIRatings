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
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import android.net.ParseException;
import android.os.AsyncTask;
import android.util.Base64;
import android.util.Log;

import com.piss.android.project.utils.APIConnectionConstants;

public class LoginTask extends AsyncTask<Void, Void, Boolean> {

	private String name;
	private String password;

	public LoginTask(String name, String password) {
		this.name = name;
		this.password = password;
	}

	@Override
	protected Boolean doInBackground(Void... params) {
		HttpClient client = new DefaultHttpClient();
		String url = APIConnectionConstants.API
				+ APIConnectionConstants.API_LOGIN;

		HttpResponse response = null;
		JSONObject json = new JSONObject();
		HttpPost httpPost;
		// List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
		try {
			httpPost = new HttpPost(url);

			json.put(APIConnectionConstants.NAME, name);
			json.put(APIConnectionConstants.PASSWORD, password);

			String ecode = name + ":" + password;
			byte[] bytes = ecode.getBytes();
			String auth = Base64.encodeToString(bytes, Base64.DEFAULT);
			httpPost.addHeader(APIConnectionConstants.AUTHENTICATION,
					APIConnectionConstants.BASIC + " " + auth);
			
			
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

			String jsonResponse;
			try {
				int code = response.getStatusLine().getStatusCode();
				Log.e("STATUS CODE", "code: " + code);
				jsonResponse = EntityUtils.toString(response.getEntity());
				Log.e("DEBUG", "login response: " + jsonResponse);
				JSONObject jsonObj = new JSONObject(jsonResponse);

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
			return false;
		} catch (UnsupportedEncodingException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
			return false;
		}
		return true;

	}
}
