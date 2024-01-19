using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : Panel
{
    [SerializeField] private UserData _userData;
    [SerializeField] private Act _act;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Text _dateText;
    [SerializeField] private InputField _name;
    [SerializeField] private InputField _orgName;
    [SerializeField] private InputField _dzoOrgName;
    [SerializeField] private InputField _dzoPlaceName;
    [SerializeField] private InputField _factoryName;
    [SerializeField] private InputField _status;

    private InputField[] _inputFields;

    private MainPanel _enterPanel;
    private GameStagesPanel _gameStagesPanel;
    private bool _isRegistred;

    private void Awake()
    {
        _registerButton.onClick.AddListener(Register);
        _exitButton.onClick.AddListener(Exit);
        _inputFields = new InputField[6] { _name, _orgName, _dzoOrgName, _dzoPlaceName, _factoryName, _status };
        foreach (var field in _inputFields)
            field.onValueChanged.AddListener(CheckEnteredAllFields);
    }

    public void Init(UserData userData, GameStagesPanel gameStagesPanel, MainPanel enterPanel)
    {
        _enterPanel = enterPanel;
        _userData = userData;
        _gameStagesPanel = gameStagesPanel;
    }

  

    public void Register()
    {
        _gameStagesPanel.Open();

        ExamSystem.Instance.EnterUser(new EnemyData()
        {
            Login = _name.text,
            WorkName = _dzoOrgName.text,
            LastFirstname = _name.text,
            DateCreate = DateTime.Now.ToString()
        });
        _userData.Init(_name.text, _orgName.text, _dzoOrgName.text, _dzoPlaceName.text, _factoryName.text, _status.text, DateTime.Now.ToString());
        _isRegistred = true;
        _registerButton.interactable = true;

        //foreach (var field in _inputFields)
        //    field.text = "";
        Close();
    }

    public void Exit()
    {
        Close();
        _enterPanel.Open();
    }

    private void CheckEnteredAllFields(string value)
    {
        int filledFileds = 0;
        for (int i = 0; i < _inputFields.Length; i++)
        {
            if (_inputFields[i].text.Length > 0)
                filledFileds++;
        }

        if (filledFileds == _inputFields.Length)
            _registerButton.interactable = true;
        else
            _registerButton.interactable = false;
    }

    public override void Close()
    {
        _registerButton.interactable = false;
        _panel.SetActive(false);
    }

    public override void Open()
    {
        _dateText.text = System.DateTime.Now.ToString("dd MMMM H:mm");
        _registerButton.interactable = false;
        foreach (var item in _inputFields)
            CheckEnteredAllFields(item.text);
        _panel.SetActive(true);
        base.Open();

    }
}
