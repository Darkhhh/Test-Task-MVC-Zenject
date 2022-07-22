using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private View view;
        [SerializeField] private int winGoldNumber;
        [SerializeField] private int defaultShovelsNumber;
        
        
        public override void InstallBindings()
        {
            Container.Bind<Model>().AsSingle().WithArguments(defaultShovelsNumber).NonLazy();
            Container.Bind<View>().FromInstance(view);
            Container.Bind<Controller>().AsSingle().WithArguments(view, winGoldNumber, defaultShovelsNumber).NonLazy();
        }
    }
}