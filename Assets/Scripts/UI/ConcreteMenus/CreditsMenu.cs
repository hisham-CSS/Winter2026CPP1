using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public override void Initialize(MenuController menuController)
    {
        base.Initialize(menuController);
        state = MenuStates.CreditsMenu;
        allButtons = GetComponentsInChildren<Button>(true);
        if (allButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in CreditsMenu.");
        }
        else
        {
            foreach (Button button in allButtons)
            {
                if (button == null) continue;
                if (button.name == "Settings") button.onClick.AddListener(() => JumpTo(MenuStates.SettingsMenu));
                if (button.name == "Back") button.onClick.AddListener(JumpBack);
            }
        }
    }

}
