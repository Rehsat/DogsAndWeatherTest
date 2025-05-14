using Game.Configs;
using Game.UI.Dogs;
using UnityEngine;

namespace Game.Factories
{
    public class DogListFactory : SimpleUIFactory<DogsListView>
    {
        public DogListFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas) : base(prefabsContainer, mainCanvas)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.DogList;
        }
    }
}