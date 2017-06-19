using Android.Widget;
using MonkeyHubApp.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(BetterSearchBarRenderer))]
namespace MonkeyHubApp.Droid.Renderers
{
    public class BetterSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            UpdateSearchIcon();
            UpdateCursorColor();
            UpdateSearchPlate();
        }

        private void UpdateSearchIcon()
        {
            try
            {
                var searchId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                if (searchId == 0)
                    return;

                var image = FindViewById<ImageView>(searchId);
                if (image == null)
                    return;

                image.SetImageResource(Resource.Drawable.ic_search_white_24dp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }
        }

        private void UpdateCursorColor()
        {
            AutoCompleteTextView textView = null;
            try
            {
                var searchId = Control.Resources.GetIdentifier("android:id/search_src_text", null, null);
                if (searchId == 0)
                    return;

                textView = FindViewById<AutoCompleteTextView>(searchId);
                if (textView == null)
                    return;

                var theClass = Java.Lang.Class.FromType(typeof(TextView));
                var theField = theClass.GetDeclaredField("mCursorDrawableRes");
                theField.Accessible = true;
                theField.Set(textView, Resource.Drawable.cursor_white);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }

            try
            {
                if (textView == null)
                    return;
                textView.SetBackgroundResource(Resource.Drawable.searchview_background);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }
        }

        private void UpdateSearchPlate()
        {
            var searchId = Control.Resources.GetIdentifier("android:id/search_plate", null, null);
            if (searchId == 0)
                return;

            var image = FindViewById<Android.Views.View>(searchId);
            if (image == null)
                return;

            image.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}