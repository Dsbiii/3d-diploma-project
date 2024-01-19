using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public abstract class OptionsWindow : Window
    {
        [SerializeField] private CounterOption[] _counterOptions;
        private int _currentCounterID = 0;

        public CounterOption CurrentCounterOption => _counterOptions[_currentCounterID];

        public override void Open()
        {
            OpenHandler();
            base.Open();

            if (_counterOptions.Length <= 0)
                return;
            _counterOptions[_currentCounterID].Exit();
            _currentCounterID = 0;
            _counterOptions[_currentCounterID].Enter();
        }

        public virtual void OpenHandler()
        {

        }

        public void DisplayFirstOption()
        {
            _counterOptions[_currentCounterID].Exit();
            _currentCounterID = 0;
            _counterOptions[_currentCounterID].Enter();
        }

        public void ClickOnNumberButton(int number)
        {
            for(int i = 0; i < _counterOptions.Length; i++)
            {
                if(_counterOptions[i].OptionNumber == number)
                {
                    _counterOptions[_currentCounterID].Exit();
                    _currentCounterID = i;
                    _counterOptions[i].Enter();
                    Select();
                }
            }
        }

        public void Select()
        {
            SelectHandler();

        }

        public virtual void SelectHandler()
        {

        }

        public virtual void Up()
        {
            if (_currentCounterID + 1 < _counterOptions.Length)
            {
                _counterOptions[_currentCounterID].Exit();
                _currentCounterID++;
                _counterOptions[_currentCounterID].Enter();
            }
        }

        public virtual void Down()
        {
            if (_currentCounterID - 1 >= 0)
            {
                _counterOptions[_currentCounterID].Exit();
                _currentCounterID--;
                _counterOptions[_currentCounterID].Enter();
            }
        }
    }
}