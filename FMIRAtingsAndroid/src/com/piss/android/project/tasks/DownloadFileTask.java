package com.piss.android.project.tasks;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;

import org.apache.http.util.ByteArrayBuffer;

import android.app.Notification;
import android.app.NotificationManager;
import android.content.Context;
import android.os.AsyncTask;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.utils.APIConnectionConstants;

public class DownloadFileTask extends AsyncTask<Void, Void, Boolean> {

	
	private String filename;
	private Context context;
	private int fileId;

	public DownloadFileTask( int fileId, String filename,
			Context context) {
		
		this.filename = filename;
		this.context = context;
		this.fileId = fileId;
	}

	@Override
	protected void onPreExecute() {
		NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(
				context).setSmallIcon(R.drawable.brand)
				.setContentTitle("FMIRatings")
				.setContentText("Downloading file " + filename);

		Notification notification = mBuilder.build();
		// Sets an ID for the notification
		int mNotificationId = fileId;
		// Gets an instance of the NotificationManager service
		NotificationManager mNotifyMgr = (NotificationManager) context
				.getSystemService(Context.NOTIFICATION_SERVICE);
		// Builds the notification and issues it.
		mNotifyMgr.notify(mNotificationId, notification);
		// Log.d("DEBUG", "buidNotification: " + notification);
	}

	@Override
	protected Boolean doInBackground(Void... params) {

		String request = APIConnectionConstants.API
				+ APIConnectionConstants.DOWNLOAD + fileId;
		boolean result = DownloadFile(request, filename);
		return result;
	}

	@Override
	protected void onPostExecute(Boolean result) {
//		NotificationManager mNotifyMgr = (NotificationManager) context
//				.getSystemService(Context.NOTIFICATION_SERVICE);
//		mNotifyMgr.cancel(fileId);
	}

	public Boolean DownloadFile(String DownloadUrl, String fileName) {
		try {
			File root = android.os.Environment.getExternalStorageDirectory();
			File dir = new File(root.getAbsolutePath() + "/FMIRatings/");
			if (dir.exists() == false) {
				dir.mkdirs();
			}

			URL url = new URL(DownloadUrl);
			File file = new File(dir, fileName.trim());

			long startTime = System.currentTimeMillis();
			Log.d("DEBUG", "download url:" + url);
			Log.d("DEBUG", "download file name:" + fileName.trim());

			URLConnection uconn = url.openConnection();
			uconn.setReadTimeout(200000);
			uconn.setConnectTimeout(200000);

			InputStream is = uconn.getInputStream();
			BufferedInputStream bufferinstream = new BufferedInputStream(is);

			ByteArrayBuffer baf = new ByteArrayBuffer(5000);
			int current = 0;
			while ((current = bufferinstream.read()) != -1) {
				baf.append((byte) current);
			}

			FileOutputStream fos = new FileOutputStream(file);
			fos.write(baf.toByteArray());
			fos.flush();
			fos.close();
			Log.d("DEBUG",
					"download ready in"
							+ ((System.currentTimeMillis() - startTime) / 1000)
							+ "sec");
			int dotindex = fileName.lastIndexOf('.');
			if (dotindex >= 0) {
				fileName = fileName.substring(0, dotindex);

			}
		} catch (IOException e) {
			Log.d("DEBUG", "Error:" + e);
			return false;
		}
		return true;

	}
}
