#if __ANDROID__
using Android.App;
using Android.Content;
using BlazorHybrid.Services;

namespace BlazorHybrid.Services
{
    public class ActivityProvider : IActivityProvider
    {
        private Activity _currentActivity;
        private readonly List<Activity> _activities = new();

        public Activity GetCurrentActivity()
        {
            Console.WriteLine($"GetCurrentActivity: {_currentActivity}");
            return _currentActivity;
        }

        public void SetCurrentActivity(Activity activity)
        {
            _currentActivity = activity;
            if (!_activities.Contains(activity))
            {
                Console.WriteLine($"SetCurrentActivity: {activity}");
                _activities.Add(activity);
            }
        }

        public void RemoveActivity(Activity activity)
        {
            if (_activities.Contains(activity))
            {
                _activities.Remove(activity);
            }
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return _activities;
        }
    }
}
#endif
