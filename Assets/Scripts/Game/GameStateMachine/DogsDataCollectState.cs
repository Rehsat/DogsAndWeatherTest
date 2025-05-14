using System;
using Game.Factories.ObjectPool;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using Game.Server.Requests.Weather;
using Game.UI.Dogs;
using Game.UI.Weather;
using UnityEngine.Pool;
using Zenject;

namespace Game.GameStateMachine
{
    public class DogsDataCollectState : IGameState, IDisposable
    {
        private readonly DogsListPresenter _dogsListPresenter;
        private readonly IServerCallbackHandler<DogBreedsDataResponse> _serverCallbackHandler;
        private readonly ServerRequestsSender _serverRequestsSender;
        private GameStateMachine _stateMachine;

        public DogsDataCollectState(IFactory<DogsListView> dogsListViewFactory
            ,DogsListPresenter dogsListPresenter
            ,IServerCallbackHandler<DogBreedsDataResponse> serverCallbackHandler
            ,ServerRequestsSender serverRequestsSender)
        {
            _dogsListPresenter = dogsListPresenter;
            _serverCallbackHandler = serverCallbackHandler;
            _serverRequestsSender = serverRequestsSender;

            var listView = dogsListViewFactory.Create();
            _dogsListPresenter.SetViewList(listView);
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            var dogsRequest = new DogsDataServerRequest(_serverCallbackHandler.HandleServerCallback);
            _serverRequestsSender.AddRequest(dogsRequest);
        }

        public void Exit()
        {
            _dogsListPresenter.Clear();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}