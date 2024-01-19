using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase
{
    public enum CablePointType { First , Second}

    public class CablesConnector : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pribors;
        [SerializeField] private List<CablePoint> _cablePoints;
        [SerializeField] private LayerMask _cableLayer;
        private CablePoint _seletingCable;
        private Counter _counter;
        private IKKTS _iKKTS;


        public void SetCounter(Counter counter)
        {
            _cablePoints = new List<CablePoint>(counter.CablePoints);
            _counter = counter;
        }

        public void SetIKK(IKKTS iKKTS)
        {
            _iKKTS = iKKTS;
        }

        public void TryConnectCable()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,Mathf.Infinity, _cableLayer))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out CablePoint cablePoint))
                    {
                        if (cablePoint.IsConnecting)
                            return;
                        if(_seletingCable == null)
                        {
                            _seletingCable = cablePoint;
                        }
                        else
                        {
                            if (_seletingCable.CablePointType != cablePoint.CablePointType)
                            {
                                _seletingCable.ConnectCables(cablePoint.transform);
                                cablePoint.Connect();
                                _seletingCable = null;
                                if(_cablePoints.Where(item => item.IsConnecting).ToArray().Length >= _cablePoints.Count)
                                {
                                    _counter.gameObject.SetActive(false);
                                    _iKKTS.gameObject.SetActive(false);
                                    foreach (var item in _pribors)
                                    {
                                        item.SetActive(true);
                                        if(item.TryGetComponent(out MeshRenderer meshRenderer))
                                        {
                                            meshRenderer.enabled = true;
                                        }
                                    }
                                    foreach (var item in _cablePoints)
                                        item.PullOut();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}