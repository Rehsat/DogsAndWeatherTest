using Game.Server.Parsers.Dogs;
using Game.Server.Requests;
using Game.UI.PopUps;
using Zenject;

namespace Game.UI.Dogs
{
    public class DogBreedPopUpPresenter
    {
        public DogBreedPopUpPresenter(IFactory<DogBreedDescriptionPopUp> popUpFactory
            ,IServerCallbackHandler<BreedResponse> breedDataCallbackHandler
            ,PopUpsSpawner popUpsSpawner)
        {
            var popUp = popUpFactory.Create();
            popUpsSpawner.AddPopUp(PopUpType.DogDataPopUp, popUp);
            breedDataCallbackHandler.OnNewDataFromServer.SubscribeWithSkip(data =>
            {
                SetPopUpData(popUp, data.Data);
                popUpsSpawner.SpawnPopUp(PopUpType.DogDataPopUp);
            });
        }

        private void SetPopUpData(DogBreedDescriptionPopUp popUp, BreedData breedData)
        {
            popUp.SetName(breedData.Attributes.Name);
            popUp.SetDescription(breedData.Attributes.Description);
        }
    }
}