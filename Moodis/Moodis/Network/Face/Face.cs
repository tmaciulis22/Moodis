using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodie.Network.Face
{
    public sealed class Face
    {
        private Face(){}

        private const string SubscriptionKey = "e285e7e510e0493bbaa7ffe5ffc2db77";
        private const string UriBase =
            "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        private static Face instance = null;
        public static Face Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Face();
                }
                return instance;
            }
        }
    }
}  