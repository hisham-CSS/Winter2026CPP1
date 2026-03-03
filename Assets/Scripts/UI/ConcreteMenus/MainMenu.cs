using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public override void Initialize(MenuController menuController)
    {
        base.Initialize(menuController);
        state = MenuStates.MainMenu;

        allButtons = GetComponentsInChildren<Button>(true);

        if (allButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in MainMenu.");
        }
        else
        {
            foreach (Button button in allButtons)
            {
                if (button == null) continue;
                if (button.name == "Start") button.onClick.AddListener(() => SceneManager.LoadScene("Game"));
                if (button.name == "Settings") button.onClick.AddListener(() => JumpTo(MenuStates.SettingsMenu));
                if (button.name == "Credits") button.onClick.AddListener(() => JumpTo(MenuStates.CreditsMenu));
                if (button.name == "Quit") button.onClick.AddListener(QuitGame);
            }
        }
    }
}
