using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CheckToTheRight : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _ipAdress;
        [SerializeField] private TMP_InputField _port;
        public bool RightRout;
        public bool IsCorrect;
        public void Check()
        {
            if (_ipAdress.text == "10.169.35.32" && _port.text == "502" && RightRout) 
            {
                Debug.Log("Robit");//1-2
                IsCorrect = true;
            }
        }
    }
}