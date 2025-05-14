using System;
using System.Collections.Generic;
using Game.Factories.ObjectPool;
using Game.Server.Parsers.Dogs;
using Game.Server.Requests;
using Game.Server.Requests.Weather;
using UniRx;

namespace Game.UI.Dogs
{
    public class DogsListPresenter : IDisposable
    {
        private readonly BaseObjectPool<DogDataButtonView> _dogsUIViewPool;
        private readonly List<DogDataButtonView> _activeViews;

        private DogsListView _dogsListView;
        private List<DogDataButtonView> _dogButtons;
        
        private CompositeDisposable _viewsCompositeDisposable;
        private CompositeDisposable _presenterCompositeDisposable;
        
        private const int MAX_DOG_VIEWS = 10;
        
        public DogsListPresenter(
            IServerCallbackHandler<DogBreedsDataResponse> dogsDataCallbackHandler
            ,BaseObjectPool<DogDataButtonView> dogsUIViewPool
            ,ServerRequestsSender requestsSenders)
        {
            _dogsUIViewPool = dogsUIViewPool;
            _dogButtons = new List<DogDataButtonView>();
            _viewsCompositeDisposable = new CompositeDisposable();
            _presenterCompositeDisposable = new CompositeDisposable();
            dogsDataCallbackHandler.OnNewDataFromServer
                .SubscribeWithSkip(CreateListOfDogsView)
                .AddTo(_presenterCompositeDisposable);
        }

        public void SetViewList(DogsListView listView)
        {
            _dogsListView = listView; // не очень это нравится, было бы время - подумал бы как исправить
        }

        private void CreateListOfDogsView(DogBreedsDataResponse dogBreedsDataResponse)
        {
            var listOfDogs = dogBreedsDataResponse.Data;
            var breedsToViewCount = Math.Clamp(dogBreedsDataResponse.Data.Count, 0, MAX_DOG_VIEWS);
            
            for (int i = 0; i < breedsToViewCount; i++)
            {
                var dogData = listOfDogs[i];
                var dogUiView = _dogsUIViewPool.Get();
                
                dogUiView.SetId(i.ToString());
                dogUiView.SetName(dogData.Attributes.Name);
                dogUiView.SetIdToCallFromServer(dogData.Id);
                _dogButtons.Add(dogUiView);
            }

            _dogsListView.SetListOfDogs(_dogButtons);
        }

        public void Clear()
        {
            _dogButtons.ForEach(view => _dogsUIViewPool.Return(view));
            _viewsCompositeDisposable?.Dispose();
            _viewsCompositeDisposable = new CompositeDisposable();
        }

        public void Dispose()
        {
            _viewsCompositeDisposable?.Dispose();
            _presenterCompositeDisposable?.Dispose();
        }
    }
}
