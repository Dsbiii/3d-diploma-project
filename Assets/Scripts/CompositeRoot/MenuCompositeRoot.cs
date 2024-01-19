using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.CompositeRoot
{
    public class MenuCompositeRoot : CompositeRoot
    {
        [SerializeField] private UserData _userData;
        [SerializeField] private MainPanel _enterPanel;
        [SerializeField] private GameStagesPanel _gameStagesPanel;
        [SerializeField] private PlayerProfilesPanel _playerProfilesPanel;
        [SerializeField] private RegisterPanel _registerPanel;
        [SerializeField] private JudgeRegisterPanel _judgeRegisterPanel;

        public override void Composite()
        {
            _gameStagesPanel.Init(_playerProfilesPanel);
            _enterPanel.Init(_playerProfilesPanel);
            _playerProfilesPanel.Init(_userData, _registerPanel, _judgeRegisterPanel);
            _registerPanel.Init(_userData, _gameStagesPanel , _enterPanel);
            _judgeRegisterPanel.Init(_playerProfilesPanel);
        }
    }
}