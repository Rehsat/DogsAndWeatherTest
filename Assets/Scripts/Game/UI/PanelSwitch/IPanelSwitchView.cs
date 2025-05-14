using EasyFramework.ReactiveEvents;

namespace Game.UI.PanelSwitch
{
    public interface IPanelSwitchView
    {
        public IReadOnlyReactiveEvent<Panel> OnPanelSwitch { get; }
    }
}