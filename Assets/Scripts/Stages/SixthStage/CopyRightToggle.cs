using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CopyRightToggle : MonoBehaviour
    {
        [SerializeField] private List<Toggle> _togglesFrom;
        [SerializeField] private List<Toggle> _togglesTo;

        public void CopyRight()
        {
            for (int i = 0; i < _togglesFrom.Count; i++)
            {
                _togglesTo[i].isOn = _togglesFrom[i].isOn;
            }
        }

    }
}