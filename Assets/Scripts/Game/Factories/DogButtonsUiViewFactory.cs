using Game.Configs;
using Game.UI.Dogs;
using UnityEngine;

namespace Game.Factories
{
    public class DogButtonsUiViewFactory : SimpleUIFactory<DogDataButtonView>
    {
        public DogButtonsUiViewFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas) : base(prefabsContainer, mainCanvas)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.DogButtonView;
        }
    }
}