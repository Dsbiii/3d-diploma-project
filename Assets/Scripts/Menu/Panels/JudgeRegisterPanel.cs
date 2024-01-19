using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JudgeRegisterPanel : Panel
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _exitButton;

    private PlayerProfilesPanel _playerProfilesPanel;

    private void Awake()
    {
        _exitButton.onClick.AddListener(Exit);
    }

    public void Init(PlayerProfilesPanel playerProfilesPanel)
    {
        _playerProfilesPanel = playerProfilesPanel;
    }

    public void Exit()
    {
        _playerProfilesPanel.Open();
        Close();
    }

    public override void Open()
    {
        _panel.SetActive(true);
        base.Open();
    }

    public override void Close()
    {
        _panel.SetActive(false);
    }
}