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

import com.piss.android.project.models.Course;
import com.piss.android.project.models.Material;
import com.piss.android.project.utils.APIConnectionConstants;

public class GetMaterialsTask extends
		AsyncTask<Void, Void, ArrayList<Material>> {

	private int id;

	public GetMaterialsTask(int id) {
		this.id = id;
	}

	@Override
	protected ArrayList<Material> doInBackground(Void... params) {

		String request = null;

		request = APIConnectionConstants.API + APIConnectionConstants.FILES
				+ "/" + APIConnectionConstants.FORCOURSE + "/"
				+ String.valueOf(id);

		HttpClient client = new DefaultHttpClient();
		Log.e("url", request);
		HttpGet get = new HttpGet(request);

		/* Set Authentication token in header */
		// get.addHeader(APIConnectionConstants.AUTHENTICATION,
		// APIConnectionConstants.BASIC + " " + auth);

		HttpResponse response;
		ArrayList<Material> mList;
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
			mList = Material.parseFromJSON(json);

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
