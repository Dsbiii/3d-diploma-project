using Assets.Scripts.Stages.FifthStage;
using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FourthStage.CablesSystem;
using Assets.Scripts.Stages.FourthStage.Panels;
using Assets.Scripts.Stages.FourthStage.SelectingCablesPanel;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class DIInstaller : MonoInstaller
    {
        [SerializeField] private FifthStageExam _fifthStageExam;
        [SerializeField] private FourthStageExamSystem _fourthStageExamSystem;
        [SerializeField] private FourthStagePlombService _fourthStagePlombService;
        [SerializeField] private PlantPanel _plantObjectPanel;
        [SerializeField] private FourthStageModel _fourthStageModel;
        [SerializeField] private FourthStageController _fourthStageController;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private MovableObjectService _movableObjectService;
        [SerializeField] private GameState _gameState;
        [SerializeField] private ItemPreviewPanel _itemPreviewPanel;
        [SerializeField] private ItemPreview _itemPreview;
        [SerializeField] private FourthStageCableConnector _fourthStageCableConnector;
        [SerializeField] private FourthStageCablesSelectingPanel _fourthStageCablesSelectingPanel;
        [SerializeField] private AntenaSevice _antenaSevice;
        [SerializeField] private SIMService _simService;
        [SerializeField] private CounterCableService _counterCableService;
        [SerializeField] private LaptopCableService _laptopCableService;

        public override void InstallBindings()
        {
            Container.Bind<FourthStageExamSystem>().FromInstance(_fourthStageExamSystem).AsSingle();
            Container.Bind<PlantPanel>().FromInstance(_plantObjectPanel).AsSingle();
            Container.Bind<FourthStageModel>().FromInstance(_fourthStageModel).AsSingle();
            Container.Bind<FourthStageController>().FromInstance(_fourthStageController).AsSingle();
            Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
            Container.Bind<MovableObjectService>().FromInstance(_movableObjectService).AsSingle();
            Container.Bind<GameState>().FromInstance(_gameState).AsSingle();
            Container.Bind<ItemPreviewPanel>().FromInstance(_itemPreviewPanel).AsSingle();
            Container.Bind<ItemPreview>().FromInstance(_itemPreview).AsSingle();
            Container.Bind<FourthStageCableConnector>().FromInstance(_fourthStageCableConnector).AsSingle();
            Container.Bind<FourthStageCablesSelectingPanel>().FromInstance(_fourthStageCablesSelectingPanel).AsSingle();
            Container.Bind<AntenaSevice>().FromInstance(_antenaSevice).AsSingle();
            Container.Bind<SIMService>().FromInstance(_simService).AsSingle();
            Container.Bind<FourthStagePlombService>().FromInstance(_fourthStagePlombService).AsSingle();
            Container.Bind<CounterCableService>().FromInstance(_counterCableService).AsSingle();
            Container.Bind<LaptopCableService>().FromInstance(_laptopCableService).AsSingle();
            Container.Bind<FifthStageExam>().FromInstance(_fifthStageExam).AsSingle();
        }
    }
}