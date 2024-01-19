using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public class MovebleObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private LayerMask _magnitePointLayer;
        [SerializeField] private Transform _baseTransform;
        private MagnitePoint _magnitePoint;
        [SerializeField] private MagnitePointsTypes _magnitePointsTypes;
        [SerializeField] private CE602M _cE602M;

        public MagnitePointsTypes MagnitePointsTypes => _magnitePointsTypes;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Camera _camera;

        public CE602M CE602M => _cE602M;

        private void Awake()
        {
            if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
                Debug.LogError("Camera doesn't ahve a physics raycaster.");

            _camera = Camera.main;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            ResetPositiion();
            OnBeginDrag();
        }

        public virtual void OnBeginDrag()
        {
            _cE602M.DisplayIndicatorsOnMagnitePoints(MagnitePointsTypes);
        }

        public virtual void OnEndDrag()
        {
            _cE602M.HideIndicatorsOnMagnitePoints();
        }

        public void ResetPositiion()
        {
            if (_baseTransform == null)
            {
                transform.position = _startPosition;
                transform.rotation = _startRotation;
            }
            else
            {
                transform.position = _baseTransform.position;
                transform.rotation = _baseTransform.rotation;
            }

        }
        public void OnDrag(PointerEventData eventData)
        {
            Ray R = _camera.ScreenPointToRay(Input.mousePosition); 
            Vector3 PO = transform.position;
            Vector3 PN = -_camera.transform.forward; 
            float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN);
            Vector3 P = R.origin + R.direction * t;
            GetComponent<Collider>().enabled = false;
            transform.position = P;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            ResetPositiion();
            EndDragHandler();
            GetComponent<Collider>().enabled = true;
        }

        public void BackInStorage()
        {
            if (_magnitePoint == null)
                return;
            _magnitePoint.Unmagnite();
            gameObject.SetActive(true);
        }

        public virtual void EndDragHandler()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _magnitePointLayer))
            {
                MagnitePoint(hit);
            }
            GetComponent<Collider>().enabled = true;
            OnEndDrag();
        }

        public virtual void MagnitePoint(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent(out MagnitePoint magnitePoint))
            {
                foreach (var point in magnitePoint.MagnitePointsTypes)
                {
                    if (point.MagnitePointsTypes == _magnitePointsTypes && !magnitePoint.IsMagnited)
                    {
                        if (_cE602M.TryConnect(magnitePoint, _magnitePointsTypes))
                        {
                            _magnitePoint = magnitePoint;
                            _magnitePoint.SetMovebleObject(this);
                            gameObject.SetActive(false);
                        }
                        return;
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

    }
}