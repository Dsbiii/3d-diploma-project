using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class SIMService : MonoBehaviour
    {
        [SerializeField] private LayerMask _simLayerMask;
        [SerializeField] private Texture2D _simTexutre;
        [SerializeField] private SIMPoint[] _simPoints;

        [Inject] private Inventory _inventory;

        private Item _currentItem;
        public bool IsSelected { get; private set; }

        public void Select(Item itemAntena)
        {
            _currentItem = itemAntena;
            Cursor.SetCursor(_simTexutre, Vector2.zero, CursorMode.Auto);
            IsSelected = true;
            foreach(var sim in _simPoints)
            {
                sim.gameObject.SetActive(true);
            }
        }

        public void TrySetupSIM()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, Mathf.Infinity, _simLayerMask))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out SIMPoint simPoint))
                    {
                        simPoint.SetupPoint();
                        _inventory.RemoveItem(_currentItem);
                        _currentItem = null;
                        return;
                    }
                }
            }
            foreach (var sim in _simPoints)
            {
                sim.gameObject.SetActive(false);
            }
        }

        public void Update()
        {
            if (IsSelected == true)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    TrySetupSIM();
                    IsSelected = false;
                }
            }
        }

    }
}