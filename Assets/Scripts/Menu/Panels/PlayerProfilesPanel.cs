using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Data;
using UnityEngine.UI;

public class PlayerProfilesPanel : Panel
{
    [SerializeField] private UserData _userData;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _participantButton;
    [SerializeField] private Button _judgeButton;

    private RegisterPanel _registerPanel;
    private JudgeRegisterPanel _judgeRegisterPanel;

    private void Awake()
    {
        _participantButton.onClick.AddListener(ParticipantChoose);
        _judgeButton.onClick.AddListener(JudeChoose);
    }

    public void Init(UserData userData, RegisterPanel registerPanel, JudgeRegisterPanel judgeRegisterPanel)
    {
        _registerPanel = registerPanel;
        _judgeRegisterPanel = judgeRegisterPanel;
    }

    public void ParticipantChoose()
    {
        _userData.SetPlayerProfile(PlayerProfile.Participant);
        Close();
        _registerPanel.Open();
    }

    public void JudeChoose()
    {
        _userData.SetPlayerProfile(PlayerProfile.Judge);
        Close();
        _judgeRegisterPanel.Open();
    }

    public override void Close()
    {
        _panel.SetActive(false);
    }

    public override void Open()
    {
        _panel.SetActive(true);
        base.Open();
    }
}
