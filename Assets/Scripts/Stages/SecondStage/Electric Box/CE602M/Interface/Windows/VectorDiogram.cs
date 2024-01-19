using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class VectorDiogram : OptionsWindow
    {
        [SerializeField] private Act _Act;
        [SerializeField] private MeteringParma _meteringParma;
        [SerializeField] private GameObject _firstDiogram;
        [SerializeField] private GameObject _secondDiogram;
        [SerializeField] private Image _Ia;
        [SerializeField] private Image _Ib;
        [SerializeField] private Image _Ic;

        private float Ib;
        private float Ic;
        private float Ia;
        private float a;
        private float c;
        private float b;

        public override void OpenHandler()
        {
            b = -30f;
            Ib = b += float.Parse(_Act.Instrumental._Fb) * -1;
            c = 30f;
            Ic = c += float.Parse(_Act.Instrumental._Fc) * -1;

            if (_Act.Instrumental._Va == -2)
            {
                a = float.Parse(_Act.Instrumental._Fa) * 1;
            }
            else
            {
                a = float.Parse(_Act.Instrumental._Fa) * -1;
            }

            float Ta = 0f;
            Ia = Ta += a;
            Down();
        }

        public override void Up()
        {
            //_Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -_meteringParma.Phase_A[2]);
            //_Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90 - _meteringParma.Phase_B[2]);
            //_Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, - (90 + _meteringParma.Phase_C[2]));

            if (Ia == 0)
            {
                _Ia.enabled = false;
            }
            else
            {
                _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ia);
            }
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ib);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ic);
        }
        public override void Down()
        {
            if (Ia == 0)
            {
                _Ia.enabled = false;
            }
            else
            {
                _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ia);
            }
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ib);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ic);
        }

        

        public override void SelectHandler()
        {

        }
    }
}