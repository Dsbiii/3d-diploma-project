using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ObjectTypeItemsPanels : MonoBehaviour
    {
        [SerializeField] private TMP_Text _selectedText;
        [SerializeField] private TMP_Text[] _selectedTexts;
        [SerializeField] private List<ObjectTypeItem> _objectTypeItems;
        [SerializeField] private TMP_InputField _search;

        public void Selected()
        {
            foreach (ObjectTypeItem item in _objectTypeItems)
            {
                if (item.IsOn)
                {
                    foreach(var item2 in _selectedTexts)
                    {
                        item2.text = item.Text;
                    }
                }
            }
        }
        public void Select(ObjectTypeItem objectTypeItem)
        {
            foreach(var item in _objectTypeItems)
            {
                if(item != objectTypeItem)
                {
                    item.Unselect();
                }
            }
        }
        public void Clean() 
        {
            foreach(var Item  in _objectTypeItems)
            {
                Item.Unselect();
            }
            _search.text = "";
        }
    }
}