using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage.Structure
{
    public class FactPower : MonoBehaviour
    {
        [SerializeField] private Act _Act;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Type _type;
        private enum Type
        {
            A, B, C
        }

        public void OnEnable()
        {
            float aPhase = (float.Parse(_Act.Instrumental._TokA) * float.Parse(_Act.Instrumental._Ua) * float.Parse(_Act.Instrumental._CosA)) / 1000;
            float bPhase = (float.Parse(_Act.Instrumental._TokB) * float.Parse(_Act.Instrumental._Ub) * float.Parse(_Act.Instrumental._CosB)) / 1000;
            float cPhase = (float.Parse(_Act.Instrumental._TokC) * float.Parse(_Act.Instrumental._Uc) * float.Parse(_Act.Instrumental._CosC)) / 1000;

            if(_type == Type.A)
            {
                _value.text = System.Math.Round(aPhase, 3).ToString();
            }
            else if( _type == Type.B)
            {
                _value.text = System.Math.Round(bPhase, 3).ToString();
            }
            else if(_type == Type.C)
            {
                _value.text = System.Math.Round(cPhase, 3).ToString();
            }
        }
    }
}