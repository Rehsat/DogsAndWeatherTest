using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public abstract class SimpleUIFactory<TTypeOfUI> :  IFactory<TTypeOfUI> where TTypeOfUI : MonoBehaviour
    {
        private readonly Canvas _mainCanvas;
        private readonly TTypeOfUI _weatherViewUI;
        
        public SimpleUIFactory(PrefabsContainer prefabsContainer, Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            _weatherViewUI = prefabsContainer.GetPrefabsComponent<TTypeOfUI>(GetPrefabType());
        }

        public TTypeOfUI Create()
        {
            var uiView = Object.Instantiate(_weatherViewUI, _mainCanvas.transform);
            if(uiView is ISimpleConstructable constructable)
                constructable.Construct();
            
            return uiView;
        }

        protected abstract Prefab GetPrefabType();
    }
}