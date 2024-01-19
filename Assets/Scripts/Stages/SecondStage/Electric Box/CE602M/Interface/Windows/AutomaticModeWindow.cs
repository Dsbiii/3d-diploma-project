using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class AutomaticModeWindow : Window
    {
        [SerializeField] private Act _act;
        [SerializeField] private TMP_Text _date;
        [SerializeField] private TMP_Text _e;
        [SerializeField] private TMP_Text _power;
        [SerializeField] private TMP_Text _post;
        [SerializeField] private TMP_Text _w;
        [SerializeField] private TMP_Text _textTimeFullLoadBar;
        [SerializeField] private TMP_Text _textTimeSmallLoadBar;

        [SerializeField] private Image _loadBarFull;
        [SerializeField] private Image _loadBarSmall;

        [SerializeField] private float _time = 60;

        private float _startTime;

        private void Awake()
        {
            //Debug.Log("Error " + _act.Instrumental._Error);
            //_e.text = "E " + _act.Instrumental._Error + " %";
            //_power.text = "PΣ 0,53 кВт";
            _startTime = _time;
            StartCoroutine(Timer());
        }

        public override void Open()
        {
            Debug.Log("Error " + _act.Instrumental._Error);
            _post.text = "Пост 1250";
            _w.text = "PΣ " + System.Math.Round((_act.Instrumental.W/1000),2).ToString() + " кВт";
            _e.text = "E " + _act.Instrumental.X + " %";
            _date.text = DateTime.Now.ToString();
            //_power.text = "PΣ 0,53 кВт";
            base.Open();
        }

        //private void OnEnable()
        //{
        //    Debug.Log("Error " + _act.Instrumental._Error);
        //    _e.text = "E " + _act.Instrumental._Error + " %";
        //    _power.text = "PΣ 0,53 кВт";
        //}

        private IEnumerator Timer()
        {
            while (true)
            {
                if (Panel.activeSelf)
                {
                    _time--;
                    _textTimeFullLoadBar.text = $"t = {_time}с";
                    _textTimeSmallLoadBar.text = $"t = {_time}с";
                    _loadBarFull.fillAmount = _time / _startTime;
                    _loadBarSmall.fillAmount = _time / _startTime;
                    if (_time <= 0)
                    {
                        break;
                    }
                }

                yield return new WaitForSeconds(1);
            }
        }

    }
}