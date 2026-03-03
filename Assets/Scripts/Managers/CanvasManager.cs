using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button resumeGame;

    [Header("In Game UI")]
    [SerializeField] private TMP_Text livesText;

    [Header("Menu References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => ChangeScene("Game"));

        if (settingsButton != null)
            settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (backButton != null)
            backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));

        if (returnToMenuButton != null)
            returnToMenuButton.onClick.AddListener(() => ChangeScene("Title"));

        if (resumeGame != null)
            resumeGame.onClick.AddListener(() => SetMenus(null, pauseMenu));
    }

    // Update is called once per frame
    void Update()
    {
        if (livesText != null)
            livesText.text = "Lives: " + GameManager.Instance.Lives;

        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
                SetMenus(null, pauseMenu);
            else
                SetMenus(pauseMenu, null);
        }
    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate != null)
            menuToActivate.SetActive(true);
        if (menuToDeactivate != null)
            menuToDeactivate.SetActive(false);
    }


    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
