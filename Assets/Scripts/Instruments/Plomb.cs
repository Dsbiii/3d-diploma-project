using Assets.Scripts;
using Assets.Scripts.Instruments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plomb : InventoryItem
{
    [SerializeField] private LayerMask _layer;

    public Vector3 _StartPosition;
    public Texture2D cursorTexture;


    public void OnBeginDrag(PointerEventData eventData)
    {

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }


    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
        {
            if (hit.transform.TryGetComponent(out PlombPoint capPoint))
            {
                capPoint.SetupCap();
            }
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

}
