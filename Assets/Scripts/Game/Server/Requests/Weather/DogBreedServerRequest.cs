using System;
using UnityEngine.Networking;

namespace Game.Server.Requests.Weather
{
    public class DogBreedServerRequest : ServerRequest
    {
        private readonly string _dogId; 
        public string URL => $"{DogsListServerRequest.DOGS_URL}/{_dogId}"; // костылек, потому что немного не успеваю
        public DogBreedServerRequest(string dogId, Action<DownloadHandler> callback) :
            base($"{DogsListServerRequest.DOGS_URL}/{dogId}", callback)
        {
            _dogId = dogId;
        }
    }
}