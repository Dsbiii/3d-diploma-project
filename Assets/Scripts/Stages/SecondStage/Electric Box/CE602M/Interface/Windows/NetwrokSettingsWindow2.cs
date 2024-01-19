using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class NetwrokSettingsWindow2 : Window, ILeftRightSwipable
    {
        [SerializeField] private Act _act;
        [SerializeField] private NetworkSettingsWindow _netwrokSettingsWindow;
        [SerializeField] private MeteringParma _meteringParma;
        [SerializeField] private TMP_Text _p1;
        [SerializeField] private TMP_Text _p2;
        [SerializeField] private TMP_Text _p3;
        [SerializeField] private TMP_Text _pE;

        [SerializeField] private TMP_Text _q1;
        [SerializeField] private TMP_Text _q2;
        [SerializeField] private TMP_Text _q3;
        [SerializeField] private TMP_Text _qE;


        [SerializeField] private TMP_Text _s1;
        [SerializeField] private TMP_Text _s2;
        [SerializeField] private TMP_Text _s3;
        [SerializeField] private TMP_Text _sE;

        [SerializeField] private Setting _Setting;
        [SerializeField] private TMP_Text _textTimeSmallLoadBar;

        [SerializeField] private Image _loadBarSmall;

        [SerializeField] private float _time = 60;

        private float _startTime;

        private void Awake()
        {
            _startTime = _time;
            StartCoroutine(Timer());
            OnOpen += DisplayValues;
        }

        public void DisplayValues(Window window)
        {

            _p1.text = "P1  " + Math.Round(_meteringParma.Pa_A, 1).ToString() + "Вт";
            _p2.text = "P2  " + Math.Round(_meteringParma.Pa_B, 1).ToString() + "Вт";
            _p3.text = "P3  " + Math.Round(_meteringParma.Pa_C, 1).ToString() + "Вт";
            _pE.text = "PΣ  " + _act.Instrumental.P + "Вт";

            _q1.text = "Q1  " + Math.Round(_meteringParma.Pr_A,1).ToString() + "вар";
            _q2.text = "Q2  " + Math.Round(_meteringParma.Pr_B, 1).ToString() + "вар";
            _q3.text = "Q3  " + Math.Round(_meteringParma.Pr_C, 1).ToString() + "вар";
            _qE.text = "QΣ  " + Math.Round((_meteringParma.Pr_A + _meteringParma.Pr_B + _meteringParma.Pr_C),1).ToString() + "вар";

            _s1.text = "S1  " + Math.Round((_meteringParma.Pa_A + _meteringParma.Pr_A), 1).ToString() + "В*A";
            _s2.text = "S2  " + Math.Round((_meteringParma.Pa_B + _meteringParma.Pr_B), 1).ToString() + "В*A";
            _s3.text = "S3  " + Math.Round((_meteringParma.Pa_C + _meteringParma.Pr_C), 1).ToString() + "В*A";
            _sE.text = "SΣ  " + Math.Round(((_meteringParma.Pa_A + _meteringParma.Pr_A) + (_meteringParma.Pa_B + _meteringParma.Pr_B) + (_meteringParma.Pa_C + _meteringParma.Pr_C)), 1).ToString() + "В*A";

            //_u12B.text = "Uab " + _meteringParma._Uab.ToString() + "B";
            //_u23B.text = "Ubc " + _meteringParma._Ubc.ToString() + "B";
            //_u31B.text = "Uca " + _meteringParma._Uab.ToString() + "B";

            //_f1B.text = "фa " + _meteringParma.Phase_A[2] + "L";
            //_f2B.text = "фb " + _meteringParma.Phase_B[2] + "L";
            //_f3B.text = "фc " + _meteringParma.Phase_C[2] + "L";
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                if (Panel.activeSelf)
                {
                    _time--;
                    _textTimeSmallLoadBar.text = $"t = {_time}с";
                    _loadBarSmall.fillAmount = _time / _startTime;
                    if (_time <= 0)
                    {
                        break;
                    }
                    // _time = _startTime;
                }

                yield return new WaitForSeconds(1);
            }
        }

        public void SwipeRight()
        {
        }

        public void SwipeLeft()
        {
            _netwrokSettingsWindow.Open();

        }
    }
}