using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public AudioClip pauseSound;
    public AudioSource audioSource;

    public override void Enter()
    {
        base.Enter();

        audioSource.PlayOneShot(pauseSound);
    }

    public override void Initialize(MenuController menuController)
    {
        base.Initialize(menuController);
        state = MenuStates.PauseMenu;
        allButtons = GetComponentsInChildren<Button>(true);
        if (allButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in SettingsMenu.");
        }
        else
        {
            foreach (Button button in allButtons)
            {
                if (button == null) continue;
                //if (button.name == "Credits") button.onClick.AddListener(() => JumpTo(MenuStates.CreditsMenu));
                //if (button.name == "Back") button.onClick.AddListener(JumpBack);
            }
        }
    }
}
