using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElectricClamp : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private ElectricClamp _electricClamp;
    [SerializeField] private LayerMask _layermask;
    [SerializeField] private Transform _baseTransform;

    [SerializeField] private GameObject _endParma;

    Vector3 _StartPosition;
    Quaternion _StartRotation;
    Camera m_cam;

    public GameObject[] Ind_Provod;



    private void Awake()
    {

        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
            Debug.Log("Camera doesn't ahve a physics raycaster.");
        m_cam = Camera.main;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _StartPosition = transform.position;
        _StartRotation = transform.rotation;
        ResetPosition();
    }


    public void OnDrag(PointerEventData eventData)
    {
        for (int i = 0; i < Ind_Provod.Length; i++)
        {
            Ind_Provod[i].SetActive(true);
        }
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.
        GetComponent<Collider>().enabled = false;
        transform.position = P;

    }

    public void ResetClamp()
    {
        gameObject.SetActive(false);
    }

    public void ResetPosition()
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

        gameObject.SetActive(true);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("ProvodClamp"))
            {
                gameObject.SetActive(false);
                hit.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                gameObject.SetActive(false);
                m_cam.transform.Find("SIZ_safety_tools_electric_clamp").gameObject.SetActive(true);
            }
            for (int i = 0; i < Ind_Provod.Length; i++)
            {
                Ind_Provod[i].SetActive(false);
            }
        }
        GetComponent<Collider>().enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Collider>().enabled = true;
    }

}
