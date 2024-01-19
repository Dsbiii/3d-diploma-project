using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainPanel : Panel
{
    [SerializeField] private Animator _enterAnimator;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _enterButton;

    private PlayerProfilesPanel _playerProfilesPanel;

    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);
        _enterButton.onClick.AddListener(NextPanel);
    }

    public void Init(PlayerProfilesPanel playerProfilesPanel)
    {
        _playerProfilesPanel = playerProfilesPanel;
    }

    private void NextPanel()
    {
        _playerProfilesPanel.Open();
        Close();
    }

    public override void Close()
    {
        _panel.SetActive(false);
    }

    public override void Open()
    {
        _enterAnimator.CrossFade("BackGround", 0);
        _panel.SetActive(true);
        base.Open();
    }
}
