using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DirectoriePanel : PanelSixStage
    {
        [SerializeField] private GameObject _leftPanel;
        [SerializeField] private GameObject _channelingEquipment;
        [SerializeField] private GameObject _config;


        public override void Close()
        {
            base.Close();
            _leftPanel.SetActive(false);
            _channelingEquipment.SetActive(false);
            _config.SetActive(false);
        }

        public override void Open()
        {
            _leftPanel.SetActive(true);
            _channelingEquipment.SetActive(false);
            _config.SetActive(false);
            base.Open();
        }

    }
}