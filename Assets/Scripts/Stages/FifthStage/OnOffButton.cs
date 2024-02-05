using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class OnOffButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject[] _panels;

        private void Start()
        {
            List<Button> buttons = _panel.GetComponentsInChildren<Button>().ToList();
            foreach (var button in buttons)
            {
                button.onClick.AddListener(Off);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        private void Off()
        {
            foreach (var item in _panels)
            {
                if (item != _panel)
                    item.SetActive(false);
            }
            _panel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                foreach (var item in _panels)
                {
                    if (item != _panel)
                        item.SetActive(false);
                }
                _panel.SetActive(false);
            }
        }

        private bool IsPointerOverUIElement()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();

            EventSystem.current.RaycastAll(eventData, results);

            foreach(var result in results)
            {
                if (result.gameObject.tag == "UISettings")
                {
                    return true;
                }
            }

            return false;
        }

        public void Click()
        {
            foreach(var item in _panels)
            {
                if(item != _panel)
                    item.SetActive(false);
            }
            _panel.SetActive(!_panel.activeSelf);
        }
    }
}