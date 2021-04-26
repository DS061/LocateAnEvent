
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.Net;

namespace LocateAnEvent
{
    [Activity(Label = "Event Details", Icon = "@drawable/OnlyLogo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Detail : Activity
    {
        TextView TextName;
        TextView TextVenue;
        TextView TextDate;
        TextView TextRestrictions;
        TextView TextTicketType;
        TextView TextWebsite;
        TextView TextDescription;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);
            TextName = FindViewById<TextView>(Resource.Id.textTitle);
            TextVenue = FindViewById<TextView>(Resource.Id.textVenue);
            TextDate = FindViewById<TextView>(Resource.Id.textDate);
            TextRestrictions = FindViewById<TextView>(Resource.Id.textRestrictions);
            TextTicketType = FindViewById<TextView>(Resource.Id.textTicketInfo);
            TextWebsite = FindViewById<TextView>(Resource.Id.textWebsite);
            TextDescription = FindViewById<TextView>(Resource.Id.textDescription);



            LoadDetail();


            // Create your application here
        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            try
            {
                Bitmap imageBitmap = null;
                if (!(url == "null"))
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                        return imageBitmap;
                    }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error:" + e.Message);

            }
            return null;
        }

        void LoadDetail()
        {
            TextName.Text = Intent.GetStringExtra("Title");
            TextVenue.Text = Intent.GetStringExtra("Address");
            TextDate.Text = Intent.GetStringExtra("DateTime");
            var imagefromweb = Intent.GetStringExtra("Image");
            var imageBitmap = GetImageBitmapFromUrl(imagefromweb);
            FindViewById<ImageView>(Resource.Id.imageView1).SetImageBitmap(imageBitmap);
            TextRestrictions.Text = Intent.GetStringExtra("Restriction");
            TextDescription.Text = Intent.GetStringExtra("Description");
            TextTicketType.Text = Intent.GetStringExtra("TicketInformation");
            TextWebsite.Text = Intent.GetStringExtra("Website");


        }
    }
}
