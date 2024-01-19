using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;

public class BreakingInstumentSevice : MonoBehaviour
{
    [SerializeField] private Act _act;
    [SerializeField] private TextMesh _display;
    [SerializeField] private ErrorToggle _coruptedDisplyeErrosToggle;
    [SerializeField] private ErrorToggle _notWorkDisplyeErrosToggle;

    [SerializeField] private MeshRenderer[] _cables;
    [SerializeField] private MeshRenderer[] _offScrewsToStopIndicator;
    [SerializeField] private MeshRenderer[] _onScrewsToStopIndicator;

    public string Problem { get; private set; }
    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private MeshRenderer _indicator;
    [SerializeField] private GameObject _corupt;
    [SerializeField] private GameObject _ce602Panel;

    private List<Screw> _screws = new List<Screw>();
    private List<Cable> _cablesLinks = new List<Cable>();

    private bool _isActive;
    private Coroutine _coroutine;
    private bool _brokenDisplay;

    public void Enable()
    {
        _isActive = true;
    }

    public void Disable()
    {
        _isActive = false;
    }

    public void OffDeffects()
    {
        //_corupt.SetActive(false);
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void DisplayDeffects()
    {
        if (Random.Range(0, 100) > 50)
        {
            Problem = "Поврежден дисплей";
            _coruptedDisplyeErrosToggle.IsError = true;
            _coruptedDisplyeErrosToggle.Error = "трещина в корпусе";
            _corupt.SetActive(true);
        }
        else
        {
            _brokenDisplay = true;
            Problem = "Не работает дисплей";
            _notWorkDisplyeErrosToggle.IsError = true;
            _notWorkDisplyeErrosToggle.Error = "не работает дисплей";
            _coroutine = StartCoroutine(LagTimer());
        }
    }

    private void Awake()
    {
        //if (!PlayerPrefs.HasKey("PreLoadStage"))
        //{
        //    DisplayDeffects();
        //}
        StartCoroutine(IndicatorTimer());

        _screws = _offScrewsToStopIndicator.Select(item => item.gameObject.GetComponent<Screw>()).ToList();
        
        foreach(var item in _onScrewsToStopIndicator)
        {
            if (item.gameObject.TryGetComponent(out Screw screw))
                _screws.Add(screw);
        }

        _cablesLinks = _cables.Select(item => item.gameObject.GetComponent<Cable>()).ToList();

        foreach (var item in _screws)
            item.OnChangeAction += OnChangeScrewsOrCables;

        foreach(var item in _cablesLinks)
            item.OnChangeAction += OnChangeScrewsOrCables;

    }

    private void Update()
    {
        float Tablo = float.Parse(_display.text);
        Tablo += 25 * Time.deltaTime;
        _display.text = Tablo.ToString();
    }

    public void OnChangeScrewsOrCables()
    {
        //    if ((_offScrewsToStopIndicator.Where(item => !item.enabled).ToArray().Length != _offScrewsToStopIndicator.Length ||
        //_onScrewsToStopIndicator.Where(item => item.enabled).ToArray().Length != _onScrewsToStopIndicator.Length) &&
        //_cables.Where(item => item.enabled).ToArray().Length == _cables.Length)
        if ((_offScrewsToStopIndicator.Where(item => !item.enabled).ToArray().Length != _offScrewsToStopIndicator.Length) &&
            _cables.Where(item => item.enabled).ToArray().Length == _cables.Length)
        {
            if (!_brokenDisplay)
            {
                _display.gameObject.SetActive(true);
            }
            _indicator.gameObject.SetActive(true);
        }
        else
        {
            _display.gameObject.SetActive(false);
            _indicator.gameObject.SetActive(false);
        }
    }

    private IEnumerator IndicatorTimer()
    {
        bool isOn = false;
        float t = (2.88f / (_act.Instrumental.W/1000));
        while (true)
        {
            StartCoroutine(Lag(_indicator));

            //if (!isOn)
            //{
            //    _stopwatch._Cout_Inp++;
            //    //_indicator.enabled = true;
            //    isOn = true;
            //}
            //else
            //{
            //    //_indicator.enabled = false;
            //    isOn = false;
            //}
            yield return new WaitForSeconds(t);
        }
    }

    private IEnumerator LagTimer()
    {
        _ce602Panel.SetActive(false);
        while (true)
        {
            if(_isActive)
                StartCoroutine(Lag(_ce602Panel));

            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
    private IEnumerator Lag(MeshRenderer meshRenderer)
    {
        meshRenderer.enabled = false;
        bool isOn = false;
        while (true)
        {
            if (!isOn)
            {
                meshRenderer.enabled = true;
                isOn = true;
            }
            else
            {
                meshRenderer.enabled = false;
                isOn = false;
                break;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
    private IEnumerator Lag(GameObject gameObject)
    {
        gameObject.SetActive(false);
        bool isOn = false;
        while (true)
        {
            if (!isOn)
            {
                gameObject.SetActive(true);
                isOn = true;
            }
            else
            {
                gameObject.SetActive(false);
                isOn = false;
                break;
            }
            yield return new WaitForSeconds(0.6f); 
        }
    }

}
