using EasyFramework.ReactiveEvents;
using Game.Factories.ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dogs
{
    public class DogDataButtonView : MonoBehaviour, IDogDataView, ILoadViewContainer, IPoolableObject
    {
        [SerializeField] private TMP_Text _id;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private RectTransform _loadingIcon;
        [SerializeField] private Button _button;

        private ReactiveEvent<string> _onDataFromServerRequired = new ReactiveEvent<string>();
        public IReadOnlyReactiveEvent<string> OnDataFromServerRequired => _onDataFromServerRequired;
        public void SetId(string id)
        {
            _id.text = id;
        }

        public void SetName(string dogName)
        {
            _name.text = dogName;
        }

        public void SetLoadViewEnableState(bool isEnabled)
        {
            _loadingIcon.gameObject.SetActive(isEnabled);
        }
        
        public void SetIdToCallFromServer(string id)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> _onDataFromServerRequired.Notify(id));
        }

        public void SetDescription(string description)
        {
            
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}