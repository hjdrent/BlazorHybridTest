using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using BlazorHybrid.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorHybrid
{
    [Activity(Label = "BlazorHybrid", Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the current activity in the ActivityProvider
            var activityProvider = (IActivityProvider)IPlatformApplication.Current.Services.GetService(typeof(IActivityProvider));
            activityProvider?.SetCurrentActivity(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Update the current activity when resumed
            var activityProvider = (IActivityProvider)IPlatformApplication.Current.Services.GetService(typeof(IActivityProvider));
            activityProvider?.SetCurrentActivity(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove the current activity from the ActivityProvider
            var activityProvider = (IActivityProvider)IPlatformApplication.Current.Services.GetService(typeof(IActivityProvider));
            activityProvider?.RemoveActivity(this);
        }
    }
}
