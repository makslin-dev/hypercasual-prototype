using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TowerBase>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BlockSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
