using EasyFramework.ReactiveEvents;
using UnityEngine.Networking;

namespace Game.Server.Requests
{
    public interface IServerCallbackHandler<TCallbackType>
    {
        public IReadOnlyReactiveEvent<TCallbackType> OnNewDataFromServer { get; }
        public void HandleServerCallback(DownloadHandler callback);
    }
}
