using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CAW
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView ivClickableImage;
        TextView tvClickCounter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            ivClickableImage = FindViewById<ImageView>(Resource.Id.imageClicker);
            tvClickCounter = FindViewById<TextView>(Resource.Id.clickCounter);
            ivClickableImage.Click += OnImageClick;
        }

        private void OnImageClick(object sender, EventArgs e)
        {
            using (var prefs = GetPreferences(Android.Content.FileCreationMode.Private))
            using (var edit = prefs.Edit())
            {
                edit.PutInt("clicks", prefs.GetInt("clicks", 0)+1);
                edit.Commit();
                tvClickCounter.Text = $"Clicks: {prefs.GetInt("clicks", 0)}";
            }
            Toast.MakeText(this, "Too-too-loo~!", ToastLength.Short).Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

