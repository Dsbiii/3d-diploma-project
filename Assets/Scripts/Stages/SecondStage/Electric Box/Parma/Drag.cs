using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ItemType _ItemType;
    public Vector3 _StartPosition;
    Camera m_cam;
    public MeteringParma meteringParma;
    public GameObject[] Zagim;

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

        m_cam = Camera.main;
        meteringParma = GameObject.Find("Parma").GetComponent<MeteringParma>();

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _StartPosition = transform.position;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.
        GetComponent<Collider>().enabled = false;
        transform.position = P;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.position = _StartPosition;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Zajim") && hit.transform.childCount == 0)
            {
                gameObject.transform.SetParent(hit.transform);
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                if (_ItemType == ItemType.ProbeBlack1)
                {
                    if (meteringParma.MeteringBlack1 != "")
                    {
                        meteringParma.MeteringBlack1 = meteringParma.MeteringBlack1.Substring(0, meteringParma.MeteringBlack1.Length - 1);
                        meteringParma.MeteringBlack1 = meteringParma.MeteringBlack1 + gameObject.transform.parent.gameObject.name;
                    }
                    
                }

                if (_ItemType == ItemType.ProbeBlack2)
                {
                    if (meteringParma.MeteringBlack2 != "")
                    {
                        meteringParma.MeteringBlack2 = meteringParma.MeteringBlack2.Substring(0, meteringParma.MeteringBlack2.Length - 1);
                        meteringParma.MeteringBlack2 = meteringParma.MeteringBlack2 + gameObject.transform.parent.gameObject.name;
                    }
                    
                }

                if (_ItemType == ItemType.ProbeRed)
                {
                    if (meteringParma.MeteringRed != "")
                    {
                        meteringParma.MeteringRed = meteringParma.MeteringRed.Substring(0, meteringParma.MeteringRed.Length - 1);
                        meteringParma.MeteringRed = meteringParma.MeteringRed + gameObject.transform.parent.gameObject.name;
                    }
                   
                }



            }
            



        }
        GetComponent<Collider>().enabled = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("OnPointerDown");
    }



    public void Off(bool Off_Black2)
    {
        if (Off_Black2 == false)
        {
            if (_ItemType == ItemType.ProbeBlack2)
            {
                gameObject.SetActive(Off_Black2);
                gameObject.transform.SetParent(null);

            }
        }
        else
        {

            if (_ItemType == ItemType.ProbeBlack2)
            {
                for (int i = 0; i < Zagim.Length; i++)
                {
                    if (Zagim[i].transform.childCount == 0)
                    {
                        gameObject.transform.SetParent(Zagim[i].transform);
                        gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

                        gameObject.SetActive(Off_Black2);
                        break;
                    }
                }
                

            }
        }
    }


}
