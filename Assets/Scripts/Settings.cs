using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Slider pour le volume principal et le volume des SFX
    [SerializeField] Slider musiqueSlider;
    [SerializeField] Slider sfxSlider;

    // Toggle et label pour le plein écran
    [SerializeField] Toggle fullScreenToggle;

    // TMP_Dropdown pour la résolution
    [SerializeField] TMP_Dropdown resolutionDrop;

    Resolution[] resolutions;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("MusiqueVolume"));
        SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume"));

        musiqueSlider.value = PlayerPrefs.GetFloat("MusiqueVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");

        // Initialize resolutions
        resolutions = Screen.resolutions;
        resolutionDrop.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDrop.AddOptions(options);
        resolutionDrop.value = currentResolutionIndex;
        resolutionDrop.RefreshShownValue();

        // Set initial fullscreen state
        fullScreenToggle.isOn = Screen.fullScreen;

        // Add listeners for sliders, toggle, and dropdown
        musiqueSlider.onValueChanged.AddListener(delegate { SetVolume(musiqueSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume(sfxSlider.value); });
        fullScreenToggle.onValueChanged.AddListener(SetPleinEcran);
        resolutionDrop.onValueChanged.AddListener(SetResolution);
    }

    // Set master volume
    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20 - 20;
        audioMixer.SetFloat("MusiqueVolume", dB);
        PlayerPrefs.SetFloat("MusiqueVolume", volume);
    }

    // Set SFX volume
    public void SetSfxVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SfxVolume", dB);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    // Set full screen mode
    public void SetPleinEcran(bool isPleinEcran)
    {
        Screen.fullScreen = isPleinEcran;
    }

    // Set screen resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}