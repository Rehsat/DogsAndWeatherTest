using Game.Configs;
using Game.Factories;
using Game.Factories.ObjectPool;
using Game.GameStateMachine;
using Game.Server.Parsers.Dogs;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using Game.Server.Requests.Weather;
using Game.UI.Dogs;
using Game.UI.PopUps;
using Game.UI.Weather;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineHandler _coroutineHandler;
        [SerializeField] private Canvas _mainCanvas; //можно сделать хранилище с несолькими канвасами,чтоб делить по слоям. Но тут это без надобности
        [SerializeField] private PrefabsContainer _prefabsContainer;
        [SerializeField] private PopUpsSpawner _popUpsSpawner; // взял из более раннего проекта, не успел сделать не монобехом
        public override void InstallBindings()
        {
            InstallInstances();
            InstallFactories();
            InstallServer();
            InstallPresenters();
            InstallStateMachine();
        }

        private void InstallInstances()
        {
            Container.BindInstance(_coroutineHandler).AsSingle().NonLazy();
            Container.BindInstance(_mainCanvas).AsSingle().NonLazy();
            Container.BindInstance(_prefabsContainer).AsSingle().NonLazy();
            Container.BindInstance(_popUpsSpawner).AsSingle().NonLazy();
        }

        private void InstallFactories()
        {
            Container.BindInterfacesAndSelfTo<WeatherViewFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<PanelSwitchFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<DogListFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<DogButtonsUiViewFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<DogBreedDescriptionPopUpFactory>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<BaseObjectPool<DogDataButtonView>>().FromNew().AsSingle();
        }

        private void InstallServer()
        {
            Container.Bind<ServerRequestsSender>().FromNew().AsSingle();

            Container.Bind<IServerCallbackHandler<WeatherPeriod>>().To<WeatherCallbackHandler>().FromNew().AsSingle();
            Container.Bind<IServerCallbackHandler<DogBreedsDataResponse>>().To<DogsListCallbackHandler>().FromNew().AsSingle();
            Container.Bind<IServerCallbackHandler<BreedResponse>>().To<DogBreedCallbackHandler>().FromNew().AsSingle();
        }

        private void InstallPresenters()
        {
            Container.Bind<DogsListPresenter>().FromNew().AsSingle();
            Container.Bind<DogBreedPopUpPresenter>().FromNew().AsSingle().NonLazy();
        }
        private void InstallStateMachine()
        {
            Container.Bind<IGameState>().To<BootstrapGameState>().AsSingle();
            Container.Bind<IGameState>().To<WeatherDataCollectState>().AsSingle();
            Container.Bind<IGameState>().To<DogsDataCollectState>().AsSingle();
            
            Container.Bind<GameStateMachine.GameStateMachine>().FromNew().AsSingle().NonLazy();
        
            Container.Bind<CurrentGameStateObserver>().FromNew().AsSingle();
        }
    }
}
