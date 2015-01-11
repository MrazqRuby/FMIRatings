package com.piss.android.project.activities;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.res.Configuration;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.app.TaskStackBuilder;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.piss.android.project.fmiratings.R;
import com.piss.android.project.fragments.CoursesListFragment;
import com.piss.android.project.fragments.InitialFragment;
import com.piss.android.project.fragments.TeachersListFragment;
import com.piss.android.project.utils.APIConnectionConstants;

public class MainActivity extends ActionBarActivity {
	private DrawerLayout mDrawerLayout;
	private ListView mDrawerList;
	private ActionBarDrawerToggle mDrawerToggle;
	private CharSequence mDrawerTitle = "FMIRatings";
	private CharSequence mTitle;
	private String[] mDrawerItems;
	private boolean backPressed = false;
	private int backCount = 0;
	private Toolbar toolbar;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		mDrawerItems = getResources().getStringArray(R.array.drawer_items);
		mDrawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
		mDrawerList = (ListView) mDrawerLayout.findViewById(R.id.left_drawer);

		toolbar = (Toolbar) findViewById(R.id.toolbar);
		// Set the adapter for the list view
		mDrawerList.setAdapter(new ArrayAdapter<String>(this,
				R.layout.drawer_list_item, mDrawerItems));
		// Set the list's click listener
		mDrawerList.setOnItemClickListener(new DrawerItemClickListener());

		mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, toolbar,
				R.string.drawer_open, R.string.drawer_close) {

			/** Called when a drawer has settled in a completely closed state. */
			public void onDrawerClosed(View view) {
				super.onDrawerClosed(view);
				getSupportActionBar().setTitle(mTitle);
				invalidateOptionsMenu();
			}

			/** Called when a drawer has settled in a completely open state. */
			public void onDrawerOpened(View drawerView) {
				super.onDrawerOpened(drawerView);
				getSupportActionBar().setTitle(mDrawerTitle);
				invalidateOptionsMenu();
			}
		};
		// Set the drawer toggle as the DrawerListener
		mDrawerLayout.setDrawerListener(mDrawerToggle);
		mTitle = mDrawerTitle = getTitle();

		// Set ToolBar
		setSupportActionBar(toolbar);
		getSupportActionBar().setDisplayHomeAsUpEnabled(true);
		getSupportActionBar().setHomeButtonEnabled(true);

		if (savedInstanceState == null) {
			selectItem(0);
		}
	}

	@Override
	public void onCreateSupportNavigateUpTaskStack(TaskStackBuilder builder) {
		super.onCreateSupportNavigateUpTaskStack(builder);
	}

	@Override
	protected void onPostCreate(Bundle savedInstanceState) {
		super.onPostCreate(savedInstanceState);
		// Sync the toggle state after onRestoreInstanceState has occurred.
		mDrawerToggle.syncState();
		// mDrawerList.setSelection(0);
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		mDrawerToggle.onConfigurationChanged(newConfig);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		// getMenuInflater().inflate(R.menu.main, menu);
		// // If the nav drawer is open, hide action items related to the
		// content
		// // view
		// boolean drawerOpen = mDrawerLayout.isDrawerOpen(mDrawerList);
		// // menu.findItem(R.id.action_websearch).setVisible(!drawerOpen);
		// return super.onPrepareOptionsMenu(menu);
		// // return true;

		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.main, menu);
		return super.onCreateOptionsMenu(menu);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		// Pass the event to ActionBarDrawerToggle, if it returns
		// true, then it has handled the app icon touch event
		if (mDrawerToggle.onOptionsItemSelected(item)) {
			return true;
		}
		// Handle your other action bar items...
		// Handle action buttons
		switch (item.getItemId()) {
		case android.R.id.home:
			int backStack = getSupportFragmentManager()
					.getBackStackEntryCount();
			Log.d("DEBUG", "Main onOptionsItemSelected: We have " + backStack
					+ " in the backstack");
			// if (backStack == 1) {
			//
			// finish();
			// return true;
			onBackPressed();
			// }
			return true;

		case R.id.action_search:

			return true;

		default:
			return super.onOptionsItemSelected(item);
		}
		// int id = item.getItemId();
		// if (id == R.id.action_search) {
		// return true;
		// }
		// return super.onOptionsItemSelected(item);
	}

	private class DrawerItemClickListener implements
			ListView.OnItemClickListener {
		@Override
		public void onItemClick(AdapterView parent, View view, int position,
				long id) {
			selectItem(position);
		}
	}

	/** Swaps fragments in the main content view */
	private void selectItem(int position) {

		// Create a new fragment and specify the planet to show based on
		// position
		Fragment fragment = null;
		Log.e("POSITION", position + "");
		switch (position) {
		case 0:
			fragment = new CoursesListFragment();

			break;

		case 1:
			fragment = new CoursesListFragment();
			break;

		case 2:
			fragment = new TeachersListFragment();
			break;
		case 3:
			SharedPreferences setttings = getSharedPreferences(
					APIConnectionConstants.PREFERENCES, MODE_PRIVATE);
			Editor edit = setttings.edit();
			edit.clear();
			edit.commit();
			// Remove all fragments and return default home icon
			getSupportFragmentManager().popBackStack();
			if (mDrawerToggle != null) {
				mDrawerToggle.setDrawerIndicatorEnabled(true);
			}
			Intent i = new Intent(this , LoginActivity.class);
			startActivity(i);
			finish();
			break;
		default:
			// fragment = new CoursesListFragment();
			break;
		}

		// Remove all fragments and return default home icon
		getSupportFragmentManager().popBackStack();
		if (mDrawerToggle != null) {
			mDrawerToggle.setDrawerIndicatorEnabled(true);
		}

		addFragment(fragment);

		// Highlight the selected item, update the title, and close the drawer
		mDrawerList.setItemChecked(position, true);
		setTitle(mDrawerItems[position]);
		mDrawerLayout.closeDrawer(mDrawerList);
	}

	@Override
	public void setTitle(CharSequence title) {
		mTitle = title;
		getSupportActionBar().setTitle(mTitle);
	}

	public void addFragment(Fragment fragment) {
		if (this.isFinishing()) {
			return;
		}
		String fragmentClassName = ((Object) fragment).getClass()
				.getSimpleName();
		FragmentManager fragmentManager = getSupportFragmentManager();

		FragmentTransaction transaction = fragmentManager.beginTransaction();
		transaction.replace(R.id.fragment_container, fragment,
				fragmentClassName);
		transaction.addToBackStack(fragmentClassName);
		transaction.commit();
	}

	public void removeFragmentsInclusive(String name) {
		if (isFinishing()) {
			return;
		}
		getSupportFragmentManager().popBackStack(name,
				FragmentManager.POP_BACK_STACK_INCLUSIVE);
		if (mDrawerToggle != null) {
			mDrawerToggle.setDrawerIndicatorEnabled(true);
		}

		backPressed = true;
	}

	@Override
	public void onBackPressed() {

		int backStack = getSupportFragmentManager().getBackStackEntryCount();
		Log.d("MainActivity", "backstack: " + backStack);
		if (backStack == 0) {
			super.onBackPressed();
		} else if (backStack == 1) {

			if (backCount == 1) {
				finish();
			}
			if (!mDrawerLayout.isDrawerOpen(Gravity.START | Gravity.LEFT)) { // <----
				// Open Drawer
				mDrawerLayout.openDrawer(Gravity.START);
				backCount = 1;
				return;
			}
		} else if (backStack > 1) {
			Fragment fr = getSupportFragmentManager().findFragmentById(
					R.id.fragment_container);
			removeFragmentsInclusive(fr.getTag());
		}

	}

	public void setUpNavigationToolbar() {

		mDrawerToggle.setDrawerIndicatorEnabled(false);
		mDrawerToggle.setHomeAsUpIndicator(getV7DrawerToggleDelegate()
				.getThemeUpIndicator());
		setSupportActionBar(toolbar);

	}
}
