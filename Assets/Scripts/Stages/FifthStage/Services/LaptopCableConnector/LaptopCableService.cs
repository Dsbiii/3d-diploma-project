using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector
{
    public class LaptopCableService : MonoBehaviour
    {
        [SerializeField] private LayerMask _simLayerMask;
        [SerializeField] private Texture2D _simTexutre;
        [SerializeField] private LaptopCablePoint[] _laptopCablePoints;

        [Inject] private Inventory _inventory;

        private Item _currentItem;
        public bool IsSelected { get; private set; }

        public void Select(Item itemAntena)
        {
            _currentItem = itemAntena;
            Cursor.SetCursor(_simTexutre, Vector2.zero, CursorMode.Auto);
            IsSelected = true;
            //foreach (var sim in _laptopCablePoints)
            //{
            //    sim.gameObject.SetActive(true);
            //}
        }

        public void TrySetupLaptopPoint()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, Mathf.Infinity, _simLayerMask))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out LaptopCablePoint simPoint))
                    {
                        simPoint.SetupPoint();
                        _inventory.RemoveItem(_currentItem);
                        _currentItem = null;
                        return;
                    }
                }
            }
            //foreach (var sim in _laptopCablePoints)
            //{
            //    sim.gameObject.SetActive(false);
            //}
        }

        public void Update()
        {
            if (IsSelected == true)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    TrySetupLaptopPoint();
                    IsSelected = false;
                }
            }
        }
    }
}