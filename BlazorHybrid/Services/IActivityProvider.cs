#if __ANDROID__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;

namespace BlazorHybrid.Services

{
    public interface IActivityProvider
    {
        Activity GetCurrentActivity();
        void SetCurrentActivity(Activity activity);
        void RemoveActivity(Activity activity);
        IEnumerable<Activity> GetAllActivities();
    }
}
#endif