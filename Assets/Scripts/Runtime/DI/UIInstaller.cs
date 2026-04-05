using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SettingsViewUI>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GameViewUI>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
