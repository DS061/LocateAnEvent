using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidHUD;
using System.Collections.Generic;

namespace LocateAnEvent
{
    [Activity(Label = "Search Results", Icon = "@drawable/OnlyLogo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SearchEventbyName : Activity
    {
        RestHandler objRest;
        ListView lstEventsSearchbyName;
        List<Event> tmpEventsSearchByName;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListView);
            lstEventsSearchbyName = FindViewById<ListView>(Resource.Id.listView1);
            lstEventsSearchbyName.ItemClick += OnlstEventsSearchbyNameClick;
            LoadSearchEventsbyname();
            // Create your application here
        }
        public async void LoadSearchEventsbyname()
        {
            var searchname = Intent.GetStringExtra("SearchName");

            AndHUD.Shared.Show(this, "Searching events", 60);
            objRest = new RestHandler(@"http://api.eventfinda.co.nz/v2/events.xml?autocomplete=" + searchname + "&fields=Event:(name)");
            var Response = await objRest.ExecuteRequestAsync();
            lstEventsSearchbyName.Adapter = new DataAdapter(this, Response.Event);
            tmpEventsSearchByName = Response.Event;
            AndHUD.Shared.Dismiss();
        }
        void OnlstEventsSearchbyNameClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var EventSearchbyNameItem = tmpEventsSearchByName[e.Position];

            var EventSearchbyNameDetail = new Intent(this, typeof(Detail));

            Helper objHelper = new Helper();

            EventSearchbyNameDetail.PutExtra("Title", objHelper.removecdata(EventSearchbyNameItem.Name));
            EventSearchbyNameDetail.PutExtra("Address", objHelper.removecdata(EventSearchbyNameItem.Address));
            EventSearchbyNameDetail.PutExtra("DateTime", EventSearchbyNameItem.Datetime_start);
            EventSearchbyNameDetail.PutExtra("Image", EventSearchbyNameItem.Images.Image[0].Transforms.Transform[3].Url);
            EventSearchbyNameDetail.PutExtra("Restriction", EventSearchbyNameItem.Restrictions);
            if (EventSearchbyNameItem.Ticket_types.Ticket_type.Count > 0)
            {
                EventSearchbyNameDetail.PutExtra("TicketInformation", EventSearchbyNameItem.Ticket_types.Ticket_type[0].Price);
            }
            else
            {
                EventSearchbyNameDetail.PutExtra("TicketInformation", "none");
            }
            EventSearchbyNameDetail.PutExtra("Description", objHelper.removecdata(EventSearchbyNameItem.Description));
            EventSearchbyNameDetail.PutExtra("Website", EventSearchbyNameItem.Url);

            StartActivity(EventSearchbyNameDetail);
        }

    }
}
