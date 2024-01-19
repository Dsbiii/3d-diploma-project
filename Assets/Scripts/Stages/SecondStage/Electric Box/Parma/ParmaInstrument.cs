using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.Parma
{
    public class ParmaInstrument : MonoBehaviour
    {
        [SerializeField] private ElectricBoxView _electricBoxView;
        [SerializeField] private GameObject _displayPlliersButton;
        [SerializeField] private GameObject _hidePlliersButton;

        [SerializeField] private ItemPreview _itemPreview;
        [SerializeField] private GameObject[] _parmasElemnts;
        [SerializeField] private ParmaComponent[] _parmaComponents;

        public void Open()
        {
            foreach(var item in _parmasElemnts)
            {
                item.SetActive(true);
            }
            _hidePlliersButton.GetComponent<Button>().onClick?.Invoke();
            _displayPlliersButton.GetComponent<Button>().onClick?.Invoke();
            _displayPlliersButton.SetActive(false);
            _hidePlliersButton.SetActive(true);
        }

        public void Close()
        {
            _electricBoxView.ResetParma();
            _itemPreview.HideCurrentInstrumentItem();
            foreach (var item in _parmasElemnts)
            {
                item.SetActive(false);
            }
            _displayPlliersButton.SetActive(false);
            _hidePlliersButton.SetActive(false);
        }

        public void ResetInstruemnt()
        {
            _electricBoxView.ResetParma();
            foreach (var item in _parmaComponents)
            {
                item.ResetPosition();
            }
            _displayPlliersButton.SetActive(false);
            _hidePlliersButton.SetActive(true);
        }
    }
}