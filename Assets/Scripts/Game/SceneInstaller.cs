using Game.Configs;
using Game.Factories;
using Game.GameStateMachine;
using Game.Server.Parsers.Weather;
using Game.Server.Requests;
using Game.Server.Requests.Weather;
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
        public override void InstallBindings()
        {
            InstallInstances();
            InstallFactories();
            InstallServer();
            InstallStateMachine();
        }

        private void InstallInstances()
        {
            Container.BindInstance(_coroutineHandler).AsSingle().NonLazy();
            Container.BindInstance(_mainCanvas).AsSingle().NonLazy();
            Container.BindInstance(_prefabsContainer).AsSingle().NonLazy();
        }

        private void InstallFactories()
        {
            Container.BindInterfacesAndSelfTo<WeatherViewFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<PanelSwitchFactory>().FromNew().AsSingle();
        }

        private void InstallServer()
        {
            Container.Bind<RequestSendHandler>().FromNew().AsSingle();

            Container.Bind<IServerCallbackHandler<WeatherData>>().To<WeatherCallbackHandler>().FromNew().AsSingle();
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
