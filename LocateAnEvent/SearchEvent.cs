
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace LocateAnEvent
{
    [Activity(Label = "Search Results", Icon = "@drawable/OnlyLogo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SearchEvent : Activity
    {


        Button btnFree;
        Button btnpaid;
        EditText SearchItem;
        Button Search;

        ArrayAdapter listAdapter;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Search);
            btnFree = FindViewById<Button>(Resource.Id.btnFree);
            btnpaid = FindViewById<Button>(Resource.Id.btnPaid);
            SearchItem = FindViewById<EditText>(Resource.Id.txtEventSearch);


            var location_array = Resources.GetStringArray(Resource.Array.location);
            var category_array = Resources.GetStringArray(Resource.Array.category);

            listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemSingleChoice);
            listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice);

            Search = FindViewById<Button>(Resource.Id.btnSearch);

            Search.Click += onbtnSearchClick;
            btnFree.Click += onbtnFreeClick;
            btnpaid.Click += onbtnPaidClick;
            // Create your application here
        }

        public void onbtnFreeClick(object sender, EventArgs e)
        {
            StartActivity(typeof(FreeList));
        }

        public void onbtnPaidClick(object sender, EventArgs e)
        {
            StartActivity(typeof(PaidList));
        }

        public void onbtnSearchClick(object sender, EventArgs e)
        {
            if (SearchItem.Text != "")
            {
                if (SearchItem.Text != "")
                {
                    var SearchEventbyNameList = new Intent(this, typeof(SearchEventbyName));
                    SearchEventbyNameList.PutExtra("SearchName", SearchItem.Text);
                    StartActivity(SearchEventbyNameList);
                }

            }
            else
            {
                Toast.MakeText(this, "Please enter one of textbox partially to search", ToastLength.Long).Show();
            }


        }
    }
}
