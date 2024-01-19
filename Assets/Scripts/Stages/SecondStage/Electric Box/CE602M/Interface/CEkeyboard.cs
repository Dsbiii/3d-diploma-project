using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public class CEkeyboard : MonoBehaviour
    {
        [SerializeField] private NumberButton[] _numberButtons;
        [SerializeField] private Button _enterButton;
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _escapeButton;

        private CEView _cEView;

        private void Awake()
        {
            _enterButton.onClick.AddListener(Enter);
            _upButton.onClick.AddListener(Down);
            _downButton.onClick.AddListener(Up);
            _rightButton.onClick.AddListener(Right);
            _leftButton.onClick.AddListener(Left);
            _escapeButton.onClick.AddListener(Escape);


            foreach (var item in _numberButtons)
            {
                item.OnClicked += NumberButtonClickHandler;
            }
        }

        public void NumberButtonClickHandler(int number)
        {
            if (_cEView.CurrentWindow is OptionsWindow optionsWindow)
            {
                optionsWindow.ClickOnNumberButton(number);
            }
        }

        public void Init(CEView cEView)
        {
            _cEView = cEView;
        }

        public void Right()
        {
            if (_cEView.CurrentWindow is ILeftRightSwipable leftRightSwipable)
            {
                //_cEView.CurrentWindow.Close();
                leftRightSwipable.SwipeRight();
            }
        }

        public void Left()
        {
            if (_cEView.CurrentWindow is ILeftRightSwipable leftRightSwipable)
            {
                //_cEView.CurrentWindow.Close();
                leftRightSwipable.SwipeLeft();
            }
        }

        public void Up()
        {
            if(_cEView.CurrentWindow is OptionsWindow optionsWindow)
            {
                optionsWindow.Up();
            }
        }

        public void Down()
        {
            if (_cEView.CurrentWindow is OptionsWindow optionsWindow)
            {
                optionsWindow.Down();
            }
        }

        public void Enter()
        {
            if (_cEView.CurrentWindow is OptionsWindow optionsWindow)
            {
                optionsWindow.Select();
            }
        }

        public void Escape()
        {
            _cEView.BackInLastWindow();
        }
    }
}