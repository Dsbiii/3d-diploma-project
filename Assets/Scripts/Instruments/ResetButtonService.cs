using Assets.Scripts.Instruments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonService : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    

    private InstrumentService _currentInstrumentService;

    private void Awake()
    {
        _resetButton.onClick.AddListener(Close);
    }

    public void DisplayButton()
    {
        _resetButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        _resetButton.gameObject.SetActive(false);
    }

    public void SetCurrentInstrumentService(InstrumentService instrumentService)
    {
        _currentInstrumentService = instrumentService;
    }


    public void Close()
    {
        _currentInstrumentService.ResetInstruemnt();
    }

}
