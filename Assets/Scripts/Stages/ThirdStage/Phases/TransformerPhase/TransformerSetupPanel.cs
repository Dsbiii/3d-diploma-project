using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase
{
    public class TransformerSetupPanel : MonoBehaviour
    {
        [SerializeField] private ThirdStageModel _thirdStageModel;

        [SerializeField] private TransfromerPhase _transformerPhase;
        [SerializeField] private GameObject _panel;

        [SerializeField] private Button _setupButton;
        [SerializeField] private Button _undoButton;

        private void Awake()
        {
            _setupButton.onClick.AddListener(Setup);
            _undoButton.onClick.AddListener(Undo);
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

        public void Setup()
        {
            _transformerPhase.Setup();
            Close();
            _thirdStageModel.PlatedTransformers();
        }

        public void Undo()
        {
            _transformerPhase.Undo();
            Close();
        }

    }
}