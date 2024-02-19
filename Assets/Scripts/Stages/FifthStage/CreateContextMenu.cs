using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class CreateContextMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _activePanel;
        [SerializeField] private GameObject _port;
        [SerializeField] private ContextPanel _contextMenu;
        [SerializeField] private GameObject _parent;
        [SerializeField] private GameObject _parentContextMenu;
        [SerializeField] private Transform _portParent;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private PortListPanel _portListPanel;

        private ContextPanel _contextPanel1;
        private GameObject _selectedObject;

        public void AddPort()
        {
            GameObject port = Instantiate(_port, _portParent);
            if(port.TryGetComponent(out Port component))
            {
                port.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = _portParent.transform.childCount.ToString();
            }else if (port.TryGetComponent(out Device component1))
            {
                port.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_InputField>().text = "Устройство " + _portParent.transform.childCount.ToString();
                port.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = _portParent.transform.childCount.ToString();
            }
            if (_contextPanel1 != null)
            {
                Destroy(_contextPanel1.gameObject);
                _contextPanel1 = null;
            }
        }

        public void Remove()
        {
            if (_selectedObject != null)
            {
                if (_selectedObject.TryGetComponent(out Port component))
                {
                    _portListPanel.DeleteSelected(component);
                }
                else if (_selectedObject.TryGetComponent(out Device component1))
                {
                    _deviceDataPanel.DeleteSelected(component1);
                }
                _selectedObject = null;
            }
            if (_contextPanel1 != null)
            {
                Destroy(_contextPanel1.gameObject);
                _contextPanel1 = null;
            }
        }

        public void AddDevice()
        {
            GameObject port = Instantiate(_port, _portParent);
            port.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = _portParent.transform.childCount.ToString();
            if (_contextPanel1 != null)
            {
                Destroy(_contextPanel1.gameObject);
                _contextPanel1 = null;
            }
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

            if (Input.GetMouseButtonDown(1))
            {
                if(TryGetSelectedObject(out GameObject gameObject))
                {
                    _selectedObject = gameObject;
                }
            }

            if (Input.GetMouseButtonDown(1) && IsPointerOnSpawnPlace())
            {
                Debug.Log(1);
                if (_contextPanel1 != null)
                {
                    Destroy(_contextPanel1.gameObject);
                    _contextPanel1 = null;
                }

                Vector2 spawnPossition = new Vector2(Input.mousePosition.x + 100, Input.mousePosition.y - 65);
                _contextPanel1 = Instantiate(_contextMenu, spawnPossition, Quaternion.identity, _parentContextMenu.transform.parent);
                _contextPanel1.SetCreateContextMenu(this);
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

        private bool IsPointerOnSpawnPlace()
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
                if (item.gameObject.tag == "ContextPanelSpawnPlace")
                    return true;
            }

            return false;
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