using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public class CounterOption : MonoBehaviour
    {
        [SerializeField] private int _optionNumber;
        [SerializeField] private OptionType _optionType;
        [SerializeField] private GameObject _selectPanel;

        public int OptionNumber => _optionNumber;
        public OptionType OptionType => _optionType;

        public void Enter()
        {
            _selectPanel.SetActive(true);
        }

        public void Exit()
        {
            _selectPanel.SetActive(false);
        }
    }
}