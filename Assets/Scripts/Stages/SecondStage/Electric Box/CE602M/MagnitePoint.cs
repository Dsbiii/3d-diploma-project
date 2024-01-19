using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box
{
    

    //[ExecuteInEditMode]
    public class MagnitePoint : MonoBehaviour
    {
        [SerializeField] private Cable _cable;
        [SerializeField] private CablePoint _cablePoint;
        [SerializeField] private bool _isActiveIndicator;
        [SerializeField] private Type[] _magnitePointsTypes;
        [SerializeField] private GameObject _pointObject;

        [SerializeField] private GameObject _indicate;
        
        private CablePort _cablePort;
        private MovebleObject _movebleObject;
        public bool IsMagnited { get; private set; }
        public MagnitePointsTypes MagnitedPointsType { get; private set; }
        public Type[] MagnitePointsTypes => _magnitePointsTypes;
        private GameObject _currentPointObjects;
        public GameObject PointObject => _pointObject;

        private void OnEnable()
        {
            //_cablePoint = transform.GetChild(0).transform.GetComponent<ParmaComponent>().FourthStageCablePoint;
            //_cable = transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable;

            var indicte = transform.Find("Indicate(Clone)");
            if (indicte != null)
            {
                _indicate = indicte.gameObject;
            }
        }

        public void Magnited(MagnitePointsTypes magnitePointsTypes, MagnitePointsTypes kleshColor)
        {
            foreach (var item in _magnitePointsTypes)
            {
                if (item.MagnitePointsTypes == magnitePointsTypes)
                {
                    MagnitedPointsType = magnitePointsTypes;

                    item.Object.SetActive(true);
                    _currentPointObjects = item.Object;
                    MagnitedHandler();
                }
            }
            IsMagnited = true;

        }

        public void DisplayIndicate()
        {
            if(_isActiveIndicator)
                _indicate.SetActive(true);
        }

        public void HideIndicate()
        {
            _indicate.SetActive(false);
        }

        public void UnmagniteMovebleObject()
        {
            if (_movebleObject == null || _cablePort == null)
                return;
            _cablePort.PullOutCable();
            _movebleObject.BackInStorage();
            _movebleObject = null;
        }

        public virtual void MagnitedHandler()
        {

        }

        public void SetCablePort(CablePort cablePort)
        {
            _cablePort = cablePort;
        }

        public void SetMovebleObject(MovebleObject movebleObject)
        {
            _movebleObject = movebleObject;
        }

        public bool TryMagnite()
        {
            if (FindObjectOfType<SecondStageController>().IsAddmissionStage &&
                _cablePoint != null &&
                _cablePoint.IsConnected &&
                ((_cable != null &&
                !_cable.IsPullOut) ||
                _cable == null))
            {
                return true;
            }
            else if (!FindObjectOfType<SecondStageController>().IsAddmissionStage &&
                _cablePoint != null &&
                ((_cable != null &&
                !_cable.IsPullOut) ||
                _cable == null))
            {
                return true;
            }
            return false;
        }

        public void Magnited(MagnitePointsTypes magnitePointsTypes)
        {
            MagnitedPointsType = magnitePointsTypes;
            _pointObject.SetActive(true);
            IsMagnited = true;

        }

        public void Unmagnite()
        {
            if (_movebleObject == null || _cablePort == null)
                return;
            _cablePort.PullOutCable();
            MagnitedPointsType = default;
            _movebleObject = null;
            if (_pointObject != null)
                _pointObject.SetActive(false);
            if(_currentPointObjects != null)
                _currentPointObjects.SetActive(false);
            if(_magnitePointsTypes.Length > 0)
                foreach (var item in _magnitePointsTypes)
                    if(item.Object != null)
                        item.Object.SetActive(false);
            IsMagnited = false;
        }
    }
}