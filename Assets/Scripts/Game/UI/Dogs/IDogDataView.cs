namespace Game.UI.Dogs
{
    public interface IDogDataView : IServerDataRequireButton
    {
        public void SetId(string id);
        public void SetName(string dogName);
        public void SetDescription(string description);
    }
}