using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class NetworkSettingsWindow : Window , ILeftRightSwipable
    {
        [SerializeField] private NetwrokSettingsWindow2 _netwrokSettingsWindow2;
        [SerializeField] private MeteringParma _meteringParma;
        [SerializeField] private TMP_Text _u1B;
        [SerializeField] private TMP_Text _u2B;
        [SerializeField] private TMP_Text _u3B;

        [SerializeField] private TMP_Text _i1B;
        [SerializeField] private TMP_Text _i2B;
        [SerializeField] private TMP_Text _i3B;

        [SerializeField] private TMP_Text _u12B;
        [SerializeField] private TMP_Text _u23B;
        [SerializeField] private TMP_Text _u31B;

        [SerializeField] private TMP_Text _f1B;
        [SerializeField] private TMP_Text _f2B;
        [SerializeField] private TMP_Text _f3B;

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
            _u1B.text ="Ua " + _meteringParma.Phase_A[0].ToString("0.0") + "B";
            _u2B.text ="Ub " + _meteringParma.Phase_B[0].ToString("0.0")+ "B";
            _u3B.text ="Uc " + _meteringParma.Phase_C[0].ToString("0.0") + "B";

            _i1B.text ="Ia " + _meteringParma.Phase_A[1].ToString("0.0") + "A";
            _i2B.text ="Ib " + _meteringParma.Phase_B[1].ToString("0.0") + "A";
            _i3B.text ="Ic " + _meteringParma.Phase_C[1].ToString("0.0") + "A";

            _u12B.text ="Uab "+ _meteringParma._Uab.ToString("0.0")+ "B";
            _u23B.text ="Ubc "+ _meteringParma._Ubc.ToString("0.0") + "B";
            _u31B.text ="Uca "+ _meteringParma._Uab.ToString("0.0") + "B";

            _f1B.text ="фa "+ _meteringParma.Phase_A[2] + "L";
            _f2B.text ="фb "+ _meteringParma.Phase_B[2] + "L";
            _f3B.text ="фc "+ _meteringParma.Phase_C[2] + "L";
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
            _netwrokSettingsWindow2.Open();
        }

        public void SwipeLeft()
        {
        }
    }
}