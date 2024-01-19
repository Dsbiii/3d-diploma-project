using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlakatPack : MonoBehaviour
{
    [SerializeField] private GameObject[] _plakats;
    public bool IsBeforeUsedInstruments { get; private set; }
    private bool _isOpen;
    private List<GameObject> _activePlakats = new List<GameObject>();
    private List<Plakat> _activePlakatsSources = new List<Plakat>();

    public bool IsPlakatsWithIsMoreThanOne { get; private set; }
    private Plakat _currentPlakat;
    public bool IsHaveCurrentPlakat => _currentPlakat != null;

    public void AddPlakat(GameObject plakat, Plakat plakat1, bool isPlaktasWithIsMoreThanOne)
    {
        _activePlakatsSources.Add(plakat1);
           IsPlakatsWithIsMoreThanOne = isPlaktasWithIsMoreThanOne;
        _activePlakats.Add(plakat);
    } 

    public void TryIncreaseCurrentPlakat()
    {
        if (_currentPlakat != null)
        {
            _currentPlakat.TryIncreaseCount();
            _currentPlakat.gameObject.SetActive(true);
        }
    }

    public void OffActivePlakats()
    {
        foreach (var item in _activePlakatsSources)
        {
            item.TryIncreaseCount();
            item.gameObject.SetActive(true);
            //if (item != _currentPlakat)
            //{
            //    item.TryIncreaseCount();
            //    item.gameObject.SetActive(true);
            //}
        }

        foreach(var item in _plakats)
        {
            item.SetActive(false);
        }
        
        foreach (var item in _activePlakats)
            item.SetActive(false);
        
        _activePlakats.Clear();
        _activePlakatsSources.Clear();
        _currentPlakat = null;
    }

    public void SetCurrentPlakat(Plakat plakat)
    {
        _currentPlakat = plakat;
    }

    public void OpenPlakat()
    {
        _isOpen = true;
        gameObject.SetActive(true);
    }

    public void UsedInstument()
    {
        if (!_isOpen)
            IsBeforeUsedInstruments = true;
    }
}
