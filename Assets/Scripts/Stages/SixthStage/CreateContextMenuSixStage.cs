using Assets.Scripts.Stages.FifthStage.Panels;
using Assets.Scripts.Stages.SixthStage;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixStage
{
    public class CreateContextMenuSixStage : MonoBehaviour
    {
        [SerializeField] private GameObject[] _executePanels;
        [SerializeField] private GameObject[] _openRoute;
        [SerializeField] private GameObject[] _closeRoute;
        [SerializeField] private GameObject[] _openDiagnosticRoute;
        [SerializeField] private GameObject[] _closeDiagnosticRoute;
        [SerializeField] private GameObject _activePanel;
        [SerializeField] private ContextPanelSixStage _contextMenu;
        [SerializeField] private GameObject _parentContextMenu;

        private ContextPanelSixStage _contextPanel1;

        public void OpenDiagnosticRoute()
        {
            foreach (var route in _openDiagnosticRoute)
            {
                route.SetActive(true);
            }
            foreach (var route in _closeDiagnosticRoute)
            {
                route.SetActive(false);
            }
            if (_contextPanel1 != null)
            {
                Destroy(_contextPanel1.gameObject);
                _contextPanel1 = null;
            }
        }
        public void OpenRoute()
        {
            foreach (var route in _openRoute)
            {
                route.SetActive(true);
            }
            foreach (var route in _closeRoute)
            {
                route.SetActive(false);
            }
            if (_contextPanel1 != null)
            {
                Destroy(_contextPanel1.gameObject);
                _contextPanel1 = null;
            }
        }

        private bool TryOpen()
        {
            foreach (var item in _executePanels)
            {
                if (item.activeSelf)
                    return false;
            }
            return true;
        }

        private void LateUpdate()
        {
            if (!_activePanel.activeSelf)
                return;

            if (Input.GetMouseButtonDown(0) && IsPointerOverUIObject())
            {
                if (_contextPanel1 != null)
                {
                    Destroy(_contextPanel1.gameObject);
                    _contextPanel1 = null;
                }
            }

            
        }

        private bool TryGetSelectedObject(out GameObject gameObject)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


            foreach (var item in results)
            {
                if (item.gameObject.tag == "smObject")
                {
                    gameObject = item.gameObject;

                    return true;
                }
            }
            gameObject = null;
            return false;
        }
        public void CreateContextMenu()
        {
            if (TryOpen())
            {
                if (_contextPanel1 != null)
                {
                    Destroy(_contextPanel1.gameObject);
                    _contextPanel1 = null;
                }

                Vector2 spawnPossition = new Vector2(Input.mousePosition.x + 100, Input.mousePosition.y + 65);
                _contextPanel1 = Instantiate(_contextMenu, spawnPossition, Quaternion.identity, _parentContextMenu.transform);
                _contextPanel1.SetCreateContextMenuSixStage(this);
            }
        }
        private bool IsPointerOverUIObject()
        {
            // Создаем объект события указателя
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

            // Устанавливаем позицию указателя мыши
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // Создаем список результатов события
            System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();

            // Проверяем, есть ли объекты UI под указателем мыши
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


            foreach (var item in results)
            {
                if (item.gameObject.tag == "ContextPanel")
                    return false;
            }

            return true;
        }
    }
}