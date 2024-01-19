using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ExpandCategories : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private Sprite _closeSprite;
        [SerializeField] private Sprite _expandSprite;
        [SerializeField] private GameObject _panel;
        private bool _expanded = false;
        public void CloseOrExpanded()
        {
            if(!_expanded)
            {
                _sprite.sprite = _expandSprite;
                _panel.SetActive(true);
                _expanded = true;
            }
            else
            {
                _sprite.sprite = _closeSprite; 
                _panel.SetActive(false);
                _expanded = false;
            }
        }
    }
}