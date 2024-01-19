using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class ToggleOffer : MonoBehaviour
    {
        [SerializeField] private List<Toggle> _toggles;

        private void OnEnable()
        {
            _toggles = GetComponentsInChildren<Toggle>().ToList();

            foreach(Toggle toggle in _toggles)
            {
                toggle.isOn = false;
            }
        }
    }
}