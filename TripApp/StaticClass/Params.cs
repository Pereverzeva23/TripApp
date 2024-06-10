using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.StaticClass
{
    public static class Params
    {
        private static string apiUrl;

        //public const string Api = @"http://localhost:8001//api/";
        public const string Api = @"http://192.168.0.104:8001//api/";

        /*
        private static float k_water;
        private static float k_sleep;
        private static float k_force;
        */

        public static string ApiUrl
        {
            get { return apiUrl; }
            set { apiUrl = value; }
        }

        /*
        public static float K_water
        {
            get { return k_water; }
            set { k_water = value; }
        }

        public static float K_sleep
        {
            get { return k_sleep; }
            set { k_sleep = value; }
        }

        public static float K_force
        {
            get { return k_force; }
            set { k_force = value; }
        }
        */
    }
}
