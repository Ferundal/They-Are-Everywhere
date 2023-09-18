using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Sprite _mutedSprite;
    [SerializeField] private Sprite _unmutedSprite;
    [SerializeField] private VisualTreeAsset _settingsButtonTemplate;
    [SerializeField] private VisualElement _settingsButtons;
    private UIDocument _uiDocument;
    private Button _playButton;
    private Button _settingsButton;
    private Button _exitButton;
    private Button _muteButton;
    private VisualElement _buttonWraper;
    private bool _muted;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");
        _playButton.clicked += LoadScene;
        _settingsButton = _uiDocument.rootVisualElement.Q<Button>("SettingsButton");
        _settingsButton.clicked += ShowSettings;
        _exitButton = _uiDocument.rootVisualElement.Q<Button>("ExitButton");
        _exitButton.clicked += Exit;
        _muteButton = _uiDocument.rootVisualElement.Q<Button>("MuteButton");
        _muteButton.clicked += MuteGame;
        _buttonWraper = _uiDocument.rootVisualElement.Q<VisualElement>("Buttons");
        _settingsButtons = _settingsButtonTemplate.CloneTree();
        var backButton = _settingsButtons.Q<Button>("BackButton");
        backButton.clicked += BackButtonTriggered;
    }

    private void Click()
    {
        AudioManager.instance.PlaySfx("Click");
    }
    private void LoadScene()
    {
        Click();
        SceneManager.LoadScene("SampleScene");
    }

    private void Exit()
    {
        Click();
        Application.Quit();
    }

    private void MuteGame()
    {
        _muted = !_muted;
        var bg = _muteButton.style.backgroundImage;
        bg.value = Background.FromSprite(_muted ? _mutedSprite : _unmutedSprite);
        _muteButton.style.backgroundImage = bg;
        AudioListener.volume = _muted ? 0 : 1;
        Click();
    }

    private void ShowSettings()
    {
        Click();
        _buttonWraper.Clear();
        _buttonWraper.Add(_settingsButtons);
    }

    private void BackButtonTriggered()
    {
        Click();
        _buttonWraper.Clear();
        _buttonWraper.Add(_playButton);
        _buttonWraper.Add(_settingsButton);
        _buttonWraper.Add(_exitButton);
    }
}
