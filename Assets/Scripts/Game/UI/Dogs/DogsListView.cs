using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Dogs
{
    public class DogsListView : MonoBehaviour
    {
        [SerializeField] private LayoutGroup _dogsViewRoot;

        public void SetListOfDogs(List<DogDataButtonView> dogDataViews)
        {
            dogDataViews.ForEach(view => view.transform.parent = _dogsViewRoot.transform);
            
            //включаем лэйаут чтобы раположить вьюшки по местам и спустя кадр выключаем, т.к. LayoutGroup относительно требовательна
            _dogsViewRoot.enabled = true;
            Observable.TimerFrame(1).Subscribe((l =>
                _dogsViewRoot.enabled = false));
        }
    }
}