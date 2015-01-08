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

import com.piss.android.project.utils.APIConnectionConstants;

import android.net.ParseException;
import android.os.AsyncTask;

public class PostCommentTask extends AsyncTask<Void, Void, Boolean> {

	private String what;
	private String text;
	private int rating;
	
	public PostCommentTask(String what, String text, int rating){
		this.what = what;
		this.text = text;
		this.rating = rating;
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
					APIConnectionConstants.COMMENT_TEXT, text));
			

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
