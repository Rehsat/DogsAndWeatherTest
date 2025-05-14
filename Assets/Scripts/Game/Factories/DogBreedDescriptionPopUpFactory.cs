using Game.Configs;
using Game.UI.PopUps;
using UnityEngine;

namespace Game.Factories
{
    public class DogBreedDescriptionPopUpFactory : SimpleUIFactory<DogBreedDescriptionPopUp>
    {
        public DogBreedDescriptionPopUpFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas) : base(prefabsContainer, mainCanvas)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.DogBreedDescriptionPopUp;
        }
    }
}