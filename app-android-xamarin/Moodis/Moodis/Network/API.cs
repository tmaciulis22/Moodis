using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moodis.Network.Endpoints;

namespace Moodis.Network
{
    class API
    {
        public static IUserEndpoint UserEndpoint { get; set; }

    }
}