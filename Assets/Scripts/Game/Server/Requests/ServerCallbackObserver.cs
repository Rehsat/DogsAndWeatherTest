using EasyFramework.ReactiveEvents;

namespace Game.Server.Requests
{
    public interface IServerCallbackHandler<TCallbackType>
    {
        public IReadOnlyReactiveEvent<TCallbackType> OnNewDataFromServer { get; }
        public void HandleServerCallback(string callback);
    }
}
