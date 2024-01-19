using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage
{
    public class MoveObjectService : MonoBehaviour
    {
        [SerializeField] private LayerMask _pointLayerMask;
        [SerializeField] private LayerMask _planeLayerMask;
        private ThirdStageModel _thirdStageModel;
        private PlantObjectPanel _plantObjectPanel;

        public void Init(PlantObjectPanel plantObjectPanel,ThirdStageModel thirdStageModel)
        {
            _thirdStageModel = thirdStageModel;
            _plantObjectPanel = plantObjectPanel;
        }

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
            if(_thirdStageModel.CurrentTSObject != null)
            {

                if (Input.GetMouseButton(0))
                {
                    if (!TryPickUI() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
                    {
                        Vector3 objectPosition = hit.point;
                        _thirdStageModel.CurrentTSObject.transform.position = objectPosition + new Vector3(0.1f,0,0);
                    }


                    if (!TryPickUI() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit1, Mathf.Infinity, _pointLayerMask))
                    {
                        if (hit1.transform.TryGetComponent(out CounterPoint counterPoint))
                        {
                            _plantObjectPanel.Open();
                        }
                        else
                        {

                            _plantObjectPanel.Close();
                        }
                    }
                    else if(!TryPickUI())
                    {
                        _plantObjectPanel.Close();
                    }
                }
            }
        }

    }
}