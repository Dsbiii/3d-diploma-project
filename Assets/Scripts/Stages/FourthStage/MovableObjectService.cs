using Assets.Scripts.Stages.FourthStage.Panels;
using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class MovableObjectService : MonoBehaviour
    {
        [SerializeField] private LayerMask _pointLayerMask;
        [SerializeField] private LayerMask _planeLayerMask;

        [Inject] private PlantPanel _plantObjectPanel;
        [Inject] private FourthStageModel _fourthStageModel;

        private bool TryPickUI()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (var item in results)
                if (item.gameObject.tag == "UI")
                    return true;
            return false;
        }

        public void MoveOnPlane()
        {
            if (_fourthStageModel.SelectedMovableObject != null)
            {
                if (TryPickUI())
                    return;
                if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
                    {
                        _plantObjectPanel.Open();
                        Vector3 objectPosition = hit.point;
                        _fourthStageModel.SelectedMovableObject.transform.position = objectPosition + new Vector3(0.1f, 0, 0);
                    }
                    else
                    {
                        _plantObjectPanel.Close();
                    }


                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit1, Mathf.Infinity, _pointLayerMask))
                    {
                        if (hit1.transform.TryGetComponent(out CounterPoint counterPoint))
                        {
                            _plantObjectPanel.SetSelectedPlant(counterPoint);
                            //_plantObjectPanel.Open();
                        }
                        else
                        {

                            //_plantObjectPanel.Open();
                        }
                    }
                    //else
                    //{
                    //    _plantObjectPanel.Close();
                    //}
                }
            }
        }
    }
}