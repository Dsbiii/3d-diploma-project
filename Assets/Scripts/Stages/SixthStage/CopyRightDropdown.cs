using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CopyRightDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _type1;
        [SerializeField] private TMP_Dropdown _type2;
        [SerializeField] private TMP_Dropdown _abonent1;
        [SerializeField] private TMP_Dropdown _abonent2;
        public void CopyRight1()
        {
            _type2.value = _type1.value;
            _abonent2.value = _abonent1.value;
        }
        public void CopyRight2()
        {
            _type1.value = _type2.value;
            _abonent1.value = _abonent2.value;
        }
    }
}