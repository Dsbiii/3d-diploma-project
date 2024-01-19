using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase
{
    public class SelectingTransformatorPanel : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TransformatorOption[] _transformatorOptions;
        private Inventory _inventory;
        private GameState _gameState;
        private ThirdStageModel _thirdStageModel;

        private void Awake()
        {
            _thirdStageModel = FindObjectOfType<ThirdStageModel>();

            foreach (var item in _transformatorOptions)
                item.OnClicked += SelectTransformatorOption;

            int rnd = Random.Range(3, 6);
            for (int i = 0; i < rnd; i++)
            {

                foreach (var option in _transformatorOptions)
                {
                    if(Random.Range(0,2) == 0)
                    {
                        option.transform.SetAsFirstSibling();
                    }
                    else
                    {
                        option.transform.SetAsLastSibling();
                    }
                }
            }
        }

        public void Init(Inventory inventory , GameState gameState)
        {
            _inventory = inventory;
            _gameState = gameState;
        }

        public void SelectTransformatorOption(TransformatorOption transformatorOption)
        {
            if (transformatorOption.IsRight)
                _thirdStageModel.IsRightSelectedTransformers = true;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(transformatorOption.Transformer, _positions[i].position, Quaternion.identity);
            }
            Close();

        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }
    }
}