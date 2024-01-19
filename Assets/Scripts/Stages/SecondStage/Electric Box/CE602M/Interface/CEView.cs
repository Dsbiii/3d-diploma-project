using Assets.Scripts.Instruments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public class CEView : MonoBehaviour
    {
        [SerializeField] private GameObject _ce602MInterface;
        [SerializeField] private List<Window> _windows;
        private Window _currentWindow;
        private Window _previousWindow;
        private int _currentWindowIndex;

        public Window CurrentWindow => _currentWindow;

        private void Awake()
        {
            _currentWindow = _windows[0];
            _previousWindow = _currentWindow;
            foreach (var item in _windows)
                item.OnOpen += OpenWindowHandler;
        }

        public void CloseView()
        {
            foreach (var item in _windows)
                item.Close();
            _ce602MInterface.SetActive(false);
        }

        public void OpenView()
        {
            foreach (var item in _windows)
                item.Close();
            _ce602MInterface.SetActive(true);
            OpenFirstPanel();
        }

        public void BackInLastWindow()
        {
            Debug.Log("back in last window");
            if (_currentWindow != null)
                _currentWindow.Close();
            OpenFirstPanel();
        }

        public void OpenFirstPanel()
        {
            _currentWindow = _windows[0];
            if(_currentWindow is OptionsWindow window)
            {
                window.DisplayFirstOption();
            }
            _currentWindow.OpenWithOutEvent();

            _currentWindowIndex = 0;
        }

        public void OpenWindowHandler(Window window)
        {
            if (_currentWindow != null)
                _currentWindow.Close();
            _previousWindow = _currentWindow;
            _currentWindow = window;
            _currentWindowIndex = _windows.IndexOf(window);
        }

    }
}