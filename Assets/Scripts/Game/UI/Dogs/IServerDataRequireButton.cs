using EasyFramework.ReactiveEvents;

namespace Game.UI.Dogs
{
    public interface IServerDataRequireButton
    {
        public IReadOnlyReactiveEvent<string> OnDataFromServerRequired { get; }
        public void SetIdToCallFromServer(string id);
    }
}