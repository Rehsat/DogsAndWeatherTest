using EasyFramework.ReactiveEvents;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Server.Requests.Weather
{
    public class SpriteCallbackHandler : IServerCallbackHandler<Sprite>
    {
        private readonly ReactiveEvent<Sprite> _onNewDataFromServer;
        public IReadOnlyReactiveEvent<Sprite> OnNewDataFromServer => _onNewDataFromServer;

        public SpriteCallbackHandler()
        {
            _onNewDataFromServer = new ReactiveEvent<Sprite>();
        }
        public void HandleServerCallback(DownloadHandler callback)
        {
            byte[] imageData = callback.data;
            Texture2D texture = new Texture2D(2, 2);
    
            if (texture.LoadImage(imageData))
            {
                var newSprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );
                _onNewDataFromServer.Notify(newSprite);
            }
            else
            {
                Debug.LogError("Failed to load texture from downloaded data");
            }
        }
    }
}