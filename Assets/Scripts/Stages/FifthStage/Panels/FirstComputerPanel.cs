using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage
{
    public class FirstComputerPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _computerPanel;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private GameObject _computerButton;
        [SerializeField] private GameObject _backToTPButton;
        [SerializeField] private FifthStageModel _fifthStageModel;
        [Inject] private FifthStageExam _fifthStageExam;
        private bool _isEnteredWrongPort;

        public bool IsEnteredInCoumputer { get; private set; }

        private void Awake()
        {
            //_fifthStageModel.SetConnectComputerStatus(true);
        }

        public void OpenComputer()
        {
            IsEnteredInCoumputer = true;
            _computerPanel.SetActive(true);
        }

        public void CloseComputer()
        {
            _computerPanel.SetActive(false);
        }

        public void Enter()
        {
            string[] nums = _inputField.text.Split('.');

            if (int.TryParse(nums[nums.Length - 1], out int result))
            {
                if (result == 153)
                {
                    _isEnteredWrongPort = true;
                    _fifthStageModel.SetConnectComputerStatus(false);
                }
                else
                {
                    if (nums.Length == 4 &&
                        nums[0] == "169" &&
                        nums[1] == "254" &&
                        nums[2] == "1")
                    {
                        if(!_isEnteredWrongPort)
                            _fifthStageExam.EnteredIP = true;

                        _fifthStageModel.SetConnectComputerStatus(true);
                    }
                    else
                    {
                        _isEnteredWrongPort = true;
                        _fifthStageModel.SetConnectComputerStatus(false);
                    }
                }
                //_computerPanel.Open();
            }
            else
            {
                _isEnteredWrongPort = true;
                _fifthStageModel.SetConnectComputerStatus(false);
            }
            Close();
            OpenComputer();
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }
    }
}