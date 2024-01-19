using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ChanelFormingTool : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private GameObject _selectedPanel;
        private ChanelFormingTool _chanelFormingTool;

        private void Awake()
        {
            _chanelFormingTool = FindObjectOfType<ChanelFormingTool>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}