using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.ReactiveTriggers;
using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.PopUps
{
    public class PopUpsSpawner : MonoBehaviour
    {
        // Подключил свое давнишнее решение для попапов, его бы по науке переписать, но сейчас нет на это времени
        [SerializeField] private Image _backGround;
        Dictionary<PopUpType, PopUp> _popUps;
        private ReactiveTrigger _onHide;

        [Inject]
        public void Construct(Canvas sceneObjectsContainer)
        {
            return;
            _onHide = new ReactiveTrigger();
            _popUps = new Dictionary<PopUpType, PopUp>();
            _onHide.Subscribe((() =>
            {
                _backGround.gameObject.SetActive(false);
            }));
        }

        public void AddPopUp(PopUpType popUpType, PopUp popUp)
        {
            if (_popUps.ContainsKey(popUpType))
            {
                Debug.LogError($"Already contains {popUpType}");
                return;
            }
            _popUps.Add(popUpType, popUp);
            popUp.Construct(_onHide);
            popUp.transform.parent = this.transform;
        }

        public void SpawnPopUp(PopUpType popUpType, Action onComplete = null)
        {
            IDisposable subscribe = null;
            void OnComplete()
            {
                onComplete?.Invoke();
                subscribe?.Dispose();
            }
            subscribe = _onHide.SubscribeWithSkip(OnComplete);
        
            _backGround.gameObject.SetActive(true);
            var popUp = _popUps[popUpType];
            popUp.gameObject.SetActive(true);
            popUp.Show();
        }
    }
    public enum PopUpType
    {
        DogDataPopUp
    }
}