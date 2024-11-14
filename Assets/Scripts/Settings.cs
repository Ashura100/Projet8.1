using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] GameObject performances;
    // Slider pour le volume principal et le volume des SFX
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    // Toggle et label pour le plein écran
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Toggle devModToggle;

    // TMP_Dropdown pour la résolution
    [SerializeField] TMP_Dropdown resolutionDrop;

    Resolution[] resolutions;

    private bool isActive = false;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("Music"));
        SetSfxVolume(PlayerPrefs.GetFloat("Sfx"));

        musicSlider.value = PlayerPrefs.GetFloat("Music");
        sfxSlider.value = PlayerPrefs.GetFloat("Sfx");

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
        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume(sfxSlider.value); });
        fullScreenToggle.onValueChanged.AddListener(SetPleinEcran);
        resolutionDrop.onValueChanged.AddListener(SetResolution);
    }

    // Set master volume
    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20 - 20;
        audioMixer.SetFloat("Music", dB);
        PlayerPrefs.SetFloat("Music", volume);
    }

    // Set SFX volume
    public void SetSfxVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("Sfx", dB);
        PlayerPrefs.SetFloat("Sfx", volume);
    }

    // Set full screen mode
    public void SetPleinEcran(bool isPleinEcran)
    {
        Screen.fullScreen = isPleinEcran;
    }

    // Méthode pour afficher ou masquer les statistiques de performance
    public void ShowPerformanceStats()
    {
        isActive = !isActive;  // Inverse la visibilité
        performances.SetActive(isActive);
    }

    // Set screen resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}