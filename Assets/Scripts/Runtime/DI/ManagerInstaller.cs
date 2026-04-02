using UnityEngine;
using Zenject;

public class ManagerInstaller : MonoInstaller
{
    [SerializeField] private Canvas[] _views;
    public override void InstallBindings()
    {
        Container.Bind<Canvas[]>().FromInstance(_views).AsSingle();
        Container.BindInterfacesAndSelfTo<VibrationManager>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ScoreManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerPrefsManager>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ViewManager>().FromNew().AsSingle().NonLazy();
    }
}
