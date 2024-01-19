using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMagnite : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _baseTransform;

    public bool IsFindingGercon { get; private set; }
    public AudioSource Audio;
    public GameObject _Bell;
    public Vector3 _StartPosition;
    public bool Detect;
    Camera m_cam;
    public GameObject Gercon;
    public bool foundMagnite;
    

    private void Awake()
    {  
        
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
            Debug.Log("Camera doesn't ahve a physics raycaster.");

        Gercon = GameObject.Find("Gercon").gameObject;

        m_cam = Camera.main;

    }

    public void ResetUsingMagnite()
    {
        IsFindingGercon = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        for (int i = 0; i < Gercon.transform.childCount; i++)
        {
            Gercon.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        _StartPosition = transform.localPosition;
    }


    public void OnDrag()
    {
        IsFindingGercon = true;
        Debug.Log("OnDrag");
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.
        GetComponent<Collider>().enabled = false;
        transform.position = P;
        _Bell.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Gercon") && hit.transform.name == "GerconOn")
            {
                float dist = Vector3.Distance(hit.transform.position, transform.position);
                if (dist <= 0.6)
                {
                    Audio.Play(0);
                    _Bell.SetActive(true);
                    _Bell.GetComponent<Animator>().Play("Bell");
                    print("Good");
                }
               
            }

            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_baseTransform == null)
        {
            transform.localPosition = _StartPosition;
        }
        else
        {
            transform.localPosition = _StartPosition;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Gercon"))
            {
                float dist = Vector3.Distance(hit.transform.position, transform.position);
                if (dist <= 0.6)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.transform.SetParent(hit.transform);
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                    _Bell.SetActive(false);
                    Detect = this;
                    foundMagnite = true;
                }
            }


        }

        GetComponent<Collider>().enabled = true;
        for (int i = 0; i < Gercon.transform.childCount; i++)
        {
            if (Gercon.transform.GetChild(i).gameObject.name != "GerconOn")
            {
                Gercon.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("OnPointerDown");
    }

    void Update()
    {
        var _TargetGercon1 = GameObject.Find("TargetGercon1").gameObject;
        var _TargetGercon2 = GameObject.Find("TargetGercon2").gameObject;
        var _TargetGercon3 = GameObject.Find("TargetGercon3").gameObject;
        var _TargetGercon4 = GameObject.Find("TargetGercon4").gameObject;
        var _TargetGercon5 = GameObject.Find("TargetGercon5").gameObject;
        float dist = Vector3.Distance(_TargetGercon1.transform.position, transform.position);
        float dist2 = Vector3.Distance(_TargetGercon2.transform.position, transform.position);
        float dist3 = Vector3.Distance(_TargetGercon3.transform.position, transform.position);
        float dist4= Vector3.Distance(_TargetGercon4.transform.position, transform.position);
        float dist5 = Vector3.Distance(_TargetGercon5.transform.position, transform.position);
        if (dist <= 0.5f && transform.parent.gameObject.name != "Gercon" || dist2 <= 0.3f && transform.parent.gameObject.name != "Gercon"
            || dist3 <= 0.3f && transform.parent.gameObject.name != "Gercon" || dist4 <= 0.5f && transform.parent.gameObject.name != "Gercon"
            || dist5 <= 0.5f && transform.parent.gameObject.name != "Gercon")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    
}
