using Assets;
using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plakat : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Transform _parent;
    [SerializeField] private PlakatService _plakatService;
    [SerializeField] private Canvas canvas;
    [SerializeField] private LayerMask _plakatPoint;

    private RectTransform rectTransform;
    public Vector3 _StartPosition;
    public Texture2D cursorTexture;
    public GameObject[] _Tochka = new GameObject[3];
    private int _countPlakats = 2;
    public GameObject _Front;
    private int _baseCounts = 2;
    public bool IsCountMoreThanOne { get; private set; }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (_countPlakats > 1)
            IsCountMoreThanOne = true;
        //_baseCounts = _countPlakats;
        _baseCounts = 2;
        IsCountMoreThanOne = true;
    }
    public void TryIncreaseCount()
    {
        _countPlakats++;
    }

    public void ResetPlakat()
    {
        _countPlakats = _baseCounts;
        Debug.Log("Reseted count " + _countPlakats);
        gameObject.SetActive(true);
        //if(_parent != null)
        //{
        //    //transform.SetParent(_parent);
        //    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        _Tochka[0] = GameObject.Find("Plakat Main").transform.GetChild(3).gameObject;
        _Tochka[1] = GameObject.Find("Plakat Main").transform.GetChild(4).gameObject;
        _Tochka[2] = GameObject.Find("Plakat Main").transform.GetChild(5).gameObject;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);



        for (int i = 0; i < _Tochka.Length; i++)
        {
            _Tochka[i].SetActive(true);
        }

    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _plakatPoint))
        {
            if (hit.transform.CompareTag("Placat"))
            {
                GameObject plakatToActive = null;

                if (gameObject.name == "Zemla")
                {
                    plakatToActive = hit.transform.GetChild(0).gameObject;
                    _plakatService.SetSetupPlakatsBeforeScrewDriver();
                    plakatToActive.SetActive(true);
                }
                if (gameObject.name == "DontOn")
                {
                    plakatToActive = hit.transform.GetChild(1).gameObject;
                    _plakatService.SetSetupPlakatsBeforeScrewDriver();
                    plakatToActive.SetActive(true);

                }
                if (gameObject.name == "Work")
                {
                    plakatToActive = hit.transform.GetChild(2).gameObject;
                    _plakatService.SetSetupPlakatsBeforeScrewDriver();
                    plakatToActive.SetActive(true);

                }

                if (gameObject.name == "Stop")
                {
                    plakatToActive = hit.transform.GetChild(3).gameObject;
                    _plakatService.SetSetupPlakatsBeforeScrewDriver();
                    plakatToActive.SetActive(true);

                }
                if (hit.transform.TryGetComponent(out PlakatPack plakat))
                {
                    plakat.SetCurrentPlakat(this);
                    if (plakatToActive != null)
                        plakat.AddPlakat(plakatToActive, this, IsCountMoreThanOne);
                    plakat.OpenPlakat();
                }
                _plakatService.SetupedPlakat();
                //hit.transform.GetComponent<Collider>().enabled = false;
                _countPlakats--;
                if (_countPlakats <= 0)
                {
                    transform.gameObject.SetActive(false);
                    //transform.SetParent(hit.transform);
                }
            }

        }

        for (int i = 0; i < _Tochka.Length; i++)
        {
            _Tochka[i].SetActive(false);
        }

        rectTransform.anchoredPosition = _StartPosition;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _StartPosition = rectTransform.anchoredPosition;

    }


}
