using EasyFramework.ReactiveEvents;
using Game.UI.Dogs;
using TMPro;
using UnityEngine;

namespace Game.UI.PopUps
{
    public class DogBreedDescriptionPopUp : PopUp, IDogDataView
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;


        public void SetName(string dogName)
        {
            _name.text = dogName;
        }
        public void SetDescription(string description)
        {
            _description.text = description;
        }
        public void SetId(string id)
        {
        }
    }
}
