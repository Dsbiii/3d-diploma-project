using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameStagesPanel : Panel
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _firstStageButton;
    [SerializeField] private Button _secondStageButton;
    [SerializeField] private Button _thirdStageButton;

    private PlayerProfilesPanel _playerProfilesPanel;


    private void Awake()
    {
        _firstStageButton.onClick.AddListener(StartFirstStage);
        _secondStageButton.onClick.AddListener(StartFifthStage);
        _exitButton.onClick.AddListener(Exit);
        _thirdStageButton.onClick.AddListener(StartSixStage);
    }

    public void Init(PlayerProfilesPanel playerProfilesPanel)
    {
        _playerProfilesPanel = playerProfilesPanel;
    }

    public void StartFirstStage()
    {
        SceneManager.LoadScene("FirstStage");
    }

    public void StartSecondStage()
    {
        PlayerPrefs.SetString("USDPStage","1");
        if (PlayerPrefs.HasKey("USDPFifthStage"))
            PlayerPrefs.DeleteKey("USDPFifthStage");
        if (PlayerPrefs.HasKey("USDPSixStage"))
            PlayerPrefs.DeleteKey("USDPSixStage");
        SceneManager.LoadScene("FirstStage");
    }

    public void StartSixStage()
    {
        PlayerPrefs.SetString("USDPStage", "1");
        PlayerPrefs.SetString("USDPFifthStage", "1");
        PlayerPrefs.SetString("USDPSixStage", "1");
        SceneManager.LoadScene("FirstStage");
    }

    public void StartFifthStage()
    {
        PlayerPrefs.SetString("USDPStage", "1");
        PlayerPrefs.SetString("USDPFifthStage", "1");
        SceneManager.LoadScene("FirstStage");
    }

    public void Exit()
    {
        Close();
        _playerProfilesPanel.Open();
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
