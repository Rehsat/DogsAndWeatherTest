using Game.Configs;
using Game.UI.PanelSwitch;
using UnityEngine;

namespace Game.Factories
{
    public class PanelSwitchFactory : SimpleUIFactory<PanelSwitchView>
    {
        public PanelSwitchFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas) : base(prefabsContainer, mainCanvas)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.PanelSwitchView;
        }
    }
}