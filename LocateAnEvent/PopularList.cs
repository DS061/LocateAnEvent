using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidHUD;
using System.Collections.Generic;


namespace LocateAnEvent
{
    [Activity(Label = "Popular Events", Icon = "@drawable/OnlyLogo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PopularList : Activity
    {
        RestHandler objRest;
        ListView lstPopularEvents;
        List<Event> tmpPopularList;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListView);
            lstPopularEvents = FindViewById<ListView>(Resource.Id.listView1);
            lstPopularEvents.ItemClick += OnlstPopularEventsClick;
            LoadPopularEvents();

            // Create your application here
        }
        public async void LoadPopularEvents()
        {
            AndHUD.Shared.Show(this, "Finding popular events", 60);
            objRest = new RestHandler(@"http://api.eventfinda.co.nz/v2/events.xml?&order=popularity");
            var Response = await objRest.ExecuteRequestAsync();
            lstPopularEvents.Adapter = new DataAdapter(this, Response.Event);
            tmpPopularList = Response.Event;
            AndHUD.Shared.Dismiss();
        }
        void OnlstPopularEventsClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var PopularItem = tmpPopularList[e.Position];

            var PopularDetail = new Intent(this, typeof(Detail));

            Helper objHelper = new Helper();

            PopularDetail.PutExtra("Title", objHelper.removecdata(PopularItem.Name));
            PopularDetail.PutExtra("Address", objHelper.removecdata(PopularItem.Address));
            PopularDetail.PutExtra("DateTime", PopularItem.Datetime_start);
            PopularDetail.PutExtra("Image", PopularItem.Images.Image[0].Transforms.Transform[3].Url);
            PopularDetail.PutExtra("Restriction", PopularItem.Restrictions);
            if (PopularItem.Ticket_types.Ticket_type.Count > 0)
            {
                PopularDetail.PutExtra("TicketInformation", PopularItem.Ticket_types.Ticket_type[0].Price);
            }
            else
            {
                PopularDetail.PutExtra("TicketInformation", "none");
            }
            PopularDetail.PutExtra("Description", objHelper.removecdata(PopularItem.Description));
            PopularDetail.PutExtra("Website", PopularItem.Url);

            StartActivity(PopularDetail);
        }
    }
}
