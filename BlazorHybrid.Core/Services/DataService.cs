using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybrid.Core.Services
{
    public class DataService: IDataService
    {
        public List<BlazorData> GetMockData()
        {
            var mock = new List<BlazorData>
            {
                new BlazorData
                {
                    Name = "Henk",
                    Age = 62
                },
                new BlazorData
                {
                    Name = "Bert",
                    Age = 50
                }
            };
            return mock;
        }
    }
}
