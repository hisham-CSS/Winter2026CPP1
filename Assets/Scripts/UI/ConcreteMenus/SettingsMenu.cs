using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public AudioMixer mixer;

    public TMP_Text masterVolText;
    public Slider masterVolSlider;

    public TMP_Text musicVolText;
    public Slider musicVolSlider;

    public TMP_Text sfxVolText;
    public Slider sfxVolSlider;

    public override void Initialize(MenuController menuController)
    {
        base.Initialize(menuController);
        state = MenuStates.SettingsMenu;
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
                if (button.name == "Credits") button.onClick.AddListener(() => JumpTo(MenuStates.CreditsMenu));
                if (button.name == "Back") button.onClick.AddListener(JumpBack);
            }
        }

        if (masterVolSlider)
        {
            SetupSliderInformation(masterVolSlider, masterVolText, "MasterVol");
            OnSliderValueChanged(masterVolSlider.value, masterVolSlider, masterVolText, "MasterVol");
        }

        if (musicVolSlider)
        {
            SetupSliderInformation(musicVolSlider, musicVolText, "MusicVol");
            OnSliderValueChanged(musicVolSlider.value, musicVolSlider, musicVolText, "MusicVol");
        }

        if (sfxVolSlider)
        {
            SetupSliderInformation(sfxVolSlider, sfxVolText, "SFXVol");
            OnSliderValueChanged(sfxVolSlider.value, sfxVolSlider, sfxVolText, "SFXVol");
        }
    }

    private void SetupSliderInformation(Slider slider, TMP_Text text, string parameterName)
    {
        slider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, slider, text, parameterName));
    }

    private void OnSliderValueChanged(float value, Slider slider, TMP_Text text, string parameterName)
    {
        if (value == 0)
        {
            value = -80;
            text.text = "0%";
        }
        else
        {
            value = Mathf.Log10(value) * 20;
            text.text = $"{Mathf.RoundToInt(slider.value * 100)}%";
        }

        mixer.SetFloat(parameterName, value);
    }
}
