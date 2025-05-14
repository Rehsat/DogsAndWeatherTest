using System;
using UnityEngine.Networking;

namespace Game.Server.Requests.Weather
{
    public class DogsListServerRequest : ServerRequest
    {
        public const string DOGS_URL = "https://dogapi.dog/api/v2/breeds"; 
        public DogsListServerRequest(Action<DownloadHandler> callback) : base(DOGS_URL, callback)
        {
        }
    }
}