using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Setting _settings;
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private GameObject[] _panelsObjects;
        [SerializeField] private List<Panel> _panels = new List<Panel>();
        private Panel _currentPanel;

        private void Awake()
        {
            _settings.Load();
            _currentPanel = _mainPanel;
            Init(_panels);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_currentPanel == _mainPanel)
                {
                    Application.Quit();
                }
                else
                {
                    foreach(var item in _panelsObjects)
                        item.SetActive(false);
                    _currentPanel.Close();
                    _mainPanel.Open();
                }
            }
        }

        public void Init(List<Panel> panels)
        {
            _panels = panels;
            foreach (var item in panels)
                item.OnOpen += OpenPanelHandler;
        }
        
        public void OpenPanelHandler(Panel panel)
        {
            _currentPanel = panel;
        }

    }
}