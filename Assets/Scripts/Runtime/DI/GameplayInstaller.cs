using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BlockSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
