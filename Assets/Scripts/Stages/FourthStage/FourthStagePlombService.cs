using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStagePlombService : MonoBehaviour
    {
        [SerializeField] private LayerMask _lidLayerMask;
        [SerializeField] private Texture2D _plombTexutre;
        [SerializeField] private FourthStagePlomb[] _plombsPoints;

        [Inject] private Inventory _inventory;

        private Item _currentItem;
        public bool IsSelected { get; private set; }

        public void Select(Item itemAntena)
        {
            _currentItem = itemAntena;
            Cursor.SetCursor(_plombTexutre, Vector2.zero, CursorMode.Auto);
            IsSelected = true;
            foreach (var sim in _plombsPoints)
            {
                sim.gameObject.SetActive(true);
            }
        }

        public void TrySetupPlomb()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, Mathf.Infinity, _lidLayerMask))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out FourthStagePlomb plomb))
                    {
                        plomb.SetupPlomb();
                        _currentItem = null;
                        return;
                    }
                }
            }
            foreach (var sim in _plombsPoints)
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
                    TrySetupPlomb();
                    IsSelected = false;
                }
            }
        }
    }
}