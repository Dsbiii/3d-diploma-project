using UnityEngine;
using Zenject;

namespace Assets.Scripts.DIInstallers
{
    public class GameModeInstaller : MonoInstaller
    {
        [SerializeField] private GameMode _gameMode;

        public override void InstallBindings()
        {
            Container.Bind<GameMode>().FromInstance(_gameMode).AsSingle();
        }
    }
}