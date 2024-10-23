using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybrid.Components.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        private string servicetest = "ServiceTest";

        private void IncrementCount()
        {
            currentCount += 10;
        }
    }
}
