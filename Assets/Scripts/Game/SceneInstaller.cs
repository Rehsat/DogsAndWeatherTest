using System.Collections;
using System.Collections.Generic;
using Game.GameStateMachine;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallStateMachine();
    }

    private void InstallStateMachine()
    {
        Container.Bind<IGameState>().To<BootstrapGameState>().AsSingle();
        Container.Bind<IGameState>().To<WeatherDataCollectState>().AsSingle();

        Container.Bind<GameStateMachine>().FromNew().AsSingle().NonLazy();
        
        Container.Bind<CurrentGameStateObserver>().FromNew().AsSingle();
    }
}
