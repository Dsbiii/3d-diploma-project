using Assets.Scripts.Stages.SecondStage;
using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ParmaComponent : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _factor = 1;
    [SerializeField] private bool _isMain;
    [SerializeField] private Cable _cable;
    [SerializeField] private CablePoint _magnitePoint;
    [SerializeField] private bool IsMaginePoint;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _baseTransform;

    private GameObject _parent;
    public ItemType _ItemType;
    public GameObject _EndParma;
    public MeteringParma meteringParma;
    public bool StarDawn;

    public GameObject[] Ind_Faza;
    public GameObject[] Ind_Provod;

    Vector3 _StartPosition;
    Quaternion _StartRotation;
    Camera m_cam;

    public Cable Cable => _cable;
    public CablePoint CablePoint => _magnitePoint;

    private Vector3 _awakePosition;
    private Quaternion _awakeRotation;
    public bool IsPlaced;
    private Vector3 _calculatedPosition;

    public enum ItemType
    {
        Ticks,
        ProbeRed,
        ProbeBlack1,
        ProbeBlack2
    }

    private void Awake()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
            Debug.Log("Camera doesn't ahve a physics raycaster.");

        _parent = GameObject.Find("Main Camera");
        _awakePosition = transform.position;
        _awakeRotation = transform.rotation;
        m_cam = Camera.main;
        meteringParma = GameObject.Find("Parma").GetComponent<MeteringParma>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        _calculatedPosition = _parent.transform.position - transform.position;

        _StartPosition = transform.position;
        _StartRotation = transform.rotation;

        meteringParma.IsTakedPiller = true;
        if (_ItemType == ItemType.ProbeBlack1)
        {
            meteringParma.MeteringBlack1 = "";
            meteringParma.FirstBlackProbe = "";
        }

        if (_ItemType == ItemType.ProbeBlack2)
        {
            meteringParma.MeteringBlack2 = "";
            meteringParma.SecondBlackProbe = "";
        }
        if (_ItemType == ItemType.ProbeRed)
        {
            meteringParma.MeteringRed = "";
            meteringParma.RedProbe = "";
        }
        if(_ItemType == ItemType.Ticks)
        {
            meteringParma.Plier = "";
        }
    }

    public void ResetParma()
    {
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        GetComponent<Collider>().enabled = true;

        if (_baseTransform == null)
        {
            transform.position = _StartPosition;
            transform.rotation = _StartRotation;
        }
        else
        {
            transform.position = _baseTransform.position;
            transform.rotation = _baseTransform.rotation;
        }

        gameObject.SetActive(true);

    }

    public void ResetPositiion()
    {
        if (_baseTransform == null)
        {
            transform.position = _StartPosition;
            transform.rotation = _StartRotation;
        }
        else
        {
            transform.position = _baseTransform.position;
            transform.rotation = _baseTransform.rotation;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        Vector3 position = transform.position;
        float cofficient = 0;
        if (IsPlaced)
        {
            position = transform.position - _parent.transform.position;
            cofficient = 0.5f;
        }


        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.
        GetComponent<Collider>().enabled = false;
        transform.position = P;

        

        if (_ItemType == ItemType.ProbeBlack1 || _ItemType == ItemType.ProbeBlack2 || _ItemType == ItemType.ProbeRed)
        {
            for (int i = 0; i < Ind_Faza.Length; i++)
            {
                Ind_Faza[i].SetActive(true);
            }
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (_ItemType == ItemType.Ticks)
        {
            for (int i = 0; i < Ind_Provod.Length; i++)
            {
                Ind_Provod[i].SetActive(true);
            }
            transform.rotation = Quaternion.Euler(0, -89.802f, 0);
        }


        if (_ItemType == ItemType.ProbeBlack1)
        {
            meteringParma.MeteringBlack1 = "";
            meteringParma.FirstBlackProbe = "";
        }

        if (_ItemType == ItemType.ProbeBlack2)
        {
            meteringParma.MeteringBlack2 = "";
            meteringParma.SecondBlackProbe = "";
        }

        if (_ItemType == ItemType.ProbeRed)
        {
            meteringParma.MeteringRed = "";
            meteringParma.RedProbe = "";
        }

        if (_ItemType == ItemType.Ticks)
        {
            meteringParma.Kleshi = "";
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        meteringParma.IsTakedPiller = false;

        ResetPositiion();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (IsMaginePoint)
        {
            Debug.Log("Clicked");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.Log("Clicked " + hit.transform);
                EndDrag(hit);
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform);
                EndDrag(hit);
            }
        }

        if (_ItemType == ItemType.ProbeBlack1 || _ItemType == ItemType.ProbeBlack2 || _ItemType == ItemType.ProbeRed)
        {
            for (int i = 0; i < Ind_Faza.Length; i++)
            {
                Ind_Faza[i].SetActive(false);
            }
        }


        if (_ItemType == ItemType.Ticks)
        {
            for (int i = 0; i < Ind_Provod.Length; i++)
            {
                Ind_Provod[i].SetActive(false);
            }
        }
        GetComponent<Collider>().enabled = true;
    }

    private void EndDrag(RaycastHit hit)
    {
        PlaceParmaComponent(hit);

        //if(hit.transform != null && hit.transform.childCount > 0 && hit.transform.GetChild(0) != null && hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>() != null)
        //{
        //    Debug.Log(hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().FourthStageCablePoint.IsConnected);
        //    if (FindObjectOfType<SecondStageController>().IsAddmissionStage &&
        //        hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().FourthStageCablePoint != null &&
        //        hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().FourthStageCablePoint.IsConnected &&
        //        ((hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable != null &&
        //        !hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable.IsPullOut) ||
        //        hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable == null))
        //    {
        //        PlaceParmaComponent(hit);
        //    }
        //    else if (!FindObjectOfType<SecondStageController>().IsAddmissionStage &&
        //        hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().FourthStageCablePoint != null &&
        //        ((hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable != null &&
        //        !hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable.IsPullOut) ||
        //        hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable == null))
        //    {
        //        PlaceParmaComponent(hit);
        //    }
        //}
        //else
        //{
        //    PlaceParmaComponent(hit);
        //}


    }

    private void PlaceParmaComponent(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Provod") && _ItemType == ItemType.Ticks)
        {
            Debug.Log(hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint);
            if (hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint != null)
                Debug.Log(hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint.IsConnected);


             //&&
             //   ((hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable != null &&
             //   !hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable.IsPullOut) ||
             //   hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable == null)

            if (FindObjectOfType<SecondStageController>().IsAddmissionStage &&
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint != null &&
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint.IsConnected)
            {
                gameObject.SetActive(false);
                hit.transform.GetChild(0).gameObject.SetActive(true);

                if (StarDawn != hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().StarDawn)
                {
                    hit.transform.GetChild(0).transform.Rotate(0, 180, 0);
                    hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().StarDawn = StarDawn;
                }

                if (StarDawn == false)
                {
                    meteringParma.Kleshi = hit.transform.GetChild(0).gameObject.name + "-" + "Top";
                }
                else
                {
                    meteringParma.Kleshi = meteringParma.Kleshi = hit.transform.GetChild(0).gameObject.name + "-" + "Down";
                }
                meteringParma.Plier = hit.transform.GetChild(0).gameObject.name;
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().IsPlaced = true;
            }
            else if (!FindObjectOfType<SecondStageController>().IsAddmissionStage &&
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().CablePoint != null &&
                ((hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable != null &&
                !hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable.IsPullOut) ||
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().Cable == null))
            {
                gameObject.SetActive(false);
                hit.transform.GetChild(0).gameObject.SetActive(true);

                if (StarDawn != hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().StarDawn)
                {
                    hit.transform.GetChild(0).transform.Rotate(0, 180, 0);
                    hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().StarDawn = StarDawn;
                }

                if (StarDawn == false)
                {
                    meteringParma.Kleshi = hit.transform.GetChild(0).gameObject.name + "-" + "Top";
                }
                else
                {
                    meteringParma.Kleshi = meteringParma.Kleshi = hit.transform.GetChild(0).gameObject.name + "-" + "Down";
                }
                meteringParma.Plier = hit.transform.GetChild(0).gameObject.name;
                hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().IsPlaced = true;
            }

        }
        else if (_ItemType == ItemType.Ticks)
        {
            gameObject.SetActive(false);
            m_cam.transform.GetChild(3).gameObject.SetActive(true);
            meteringParma.Kleshi = "";
            meteringParma.Plier = "";
            IsPlaced = false;
        }



        if (hit.transform.CompareTag("Probe") && _ItemType == ItemType.ProbeBlack1 && hit.transform.GetChild(1).gameObject.activeSelf != true)
        {
            gameObject.SetActive(false);
            hit.transform.GetChild(0).gameObject.SetActive(true);
            hit.transform.GetChild(0).gameObject.GetComponent<ParmaComponent>()._EndParma = hit.transform.GetChild(0).gameObject.GetComponent<ParmaComponent>().transform.Find("StartCabelStick").transform.gameObject.GetComponent<CableComponent>().endPoint.transform.gameObject;
            meteringParma.MeteringBlack1 = hit.transform.gameObject.name + "-" + hit.transform.GetChild(0).gameObject.GetComponent<ParmaComponent>()._EndParma.transform.parent.transform.parent.gameObject.name;
            meteringParma.FirstBlackProbe = hit.transform.name;
            hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().IsPlaced = true;

        }
        else if (_ItemType == ItemType.ProbeBlack1)
        {
            gameObject.SetActive(false);
            m_cam.transform.GetChild(1).gameObject.SetActive(true);
            meteringParma.MeteringBlack1 = "";
            meteringParma.FirstBlackProbe = "";
            IsPlaced = false;
        }


        if (hit.transform.CompareTag("Probe") && _ItemType == ItemType.ProbeBlack2 && hit.transform.GetChild(2).gameObject.activeSelf != true)
        {

            gameObject.SetActive(false);
            hit.transform.GetChild(1).gameObject.SetActive(true);
            hit.transform.GetChild(1).gameObject.GetComponent<ParmaComponent>()._EndParma = hit.transform.GetChild(1).gameObject.GetComponent<ParmaComponent>().transform.Find("StartCabelStick").transform.gameObject.GetComponent<CableComponent>().endPoint.transform.gameObject;
            meteringParma.MeteringBlack2 = hit.transform.gameObject.name + "-" + hit.transform.GetChild(1).gameObject.GetComponent<ParmaComponent>()._EndParma.transform.parent.transform.parent.gameObject.name;
            meteringParma.SecondBlackProbe = hit.transform.name;
            hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().IsPlaced = true;
        }
        else if (_ItemType == ItemType.ProbeBlack2)
        {
            gameObject.SetActive(false);
            m_cam.transform.GetChild(2).gameObject.SetActive(true);
            meteringParma.MeteringBlack2 = "";
            meteringParma.SecondBlackProbe = "";
            IsPlaced = false;
        }


        if (hit.transform.CompareTag("Probe") && _ItemType == ItemType.ProbeRed && hit.transform.GetChild(0).gameObject.activeSelf != true)
        {
            gameObject.SetActive(false);
            hit.transform.GetChild(2).gameObject.SetActive(true);
            hit.transform.GetChild(2).gameObject.GetComponent<ParmaComponent>()._EndParma = hit.transform.GetChild(2).gameObject.GetComponent<ParmaComponent>().transform.Find("StartCabelStick").transform.gameObject.GetComponent<CableComponent>().endPoint.transform.gameObject;
            meteringParma.MeteringRed = hit.transform.gameObject.name + "-" + hit.transform.GetChild(2).gameObject.GetComponent<ParmaComponent>()._EndParma.transform.parent.transform.parent.gameObject.name;
            meteringParma.RedProbe = hit.transform.name;
            hit.transform.GetChild(0).transform.GetComponent<ParmaComponent>().IsPlaced = true;
        }
        else if (_ItemType == ItemType.ProbeRed)
        {
            gameObject.SetActive(false);
            m_cam.transform.GetChild(0).gameObject.SetActive(true);
            meteringParma.MeteringRed = "";
            meteringParma.RedProbe = "";
            IsPlaced = false;
        }
        else
        {
            IsPlaced = false;
        }
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{

    //}

    void Update() 
    {
        if (Input.GetMouseButtonDown(2) && _ItemType == ItemType.Ticks)
        {

            transform.Rotate(0, 180, 0);
            if (StarDawn == false)
            {
                StarDawn = true;
                if (gameObject.name != "kle01")
                {
                    meteringParma.Kleshi = gameObject.name + "-" + "Down";
                }
                
            }
            else
            {
                StarDawn = false;
                if (gameObject.name != "kle01")
                {
                    meteringParma.Kleshi = gameObject.name + "-" + "Top";
                }
                
            }
            
        }
    }
}
