using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class AntenaSevice : MonoBehaviour
    {
        [SerializeField] private LayerMask _antenaLayerMask;
        [SerializeField] private Texture2D _antenaTexutre;
        [SerializeField] private AntenaPoint _antenaPoint;

        [Inject] private Inventory _inventory;

        private Item _currentItem;
        public bool IsSelected { get; private set; }

        public void Select(Item itemAntena)
        {
            _currentItem = itemAntena;
            Cursor.SetCursor(_antenaTexutre, Vector2.zero, CursorMode.Auto);
            IsSelected = true;
            _antenaPoint.gameObject.SetActive(true);
        }

        public void TrySetupAntena()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, Mathf.Infinity, _antenaLayerMask))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out AntenaPoint antenaPoint))
                    {
                        antenaPoint.SetupAntena();
                        _inventory.RemoveItem(_currentItem);
                        _currentItem = null;
                        return;
                    }
                }
            }
            _antenaPoint.gameObject.SetActive(false);
        }

        public void Update()
        {
            if(IsSelected == true)
            {
                if(Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    TrySetupAntena();
                    IsSelected = false;
                }
            }
        }

    }
}