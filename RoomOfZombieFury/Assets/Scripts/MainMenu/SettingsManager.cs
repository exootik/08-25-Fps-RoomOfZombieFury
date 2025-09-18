using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Button normalDifficultyButton;
    public Button hardcoreDifficultyButton;
    public Slider smoothSpeedSlider;
    public Slider sensibilitySlider;
    public Slider musicVolumeSlider;

    public Button saveButton;
    [SerializeField] private float waveInterval;
    [SerializeField] private int spawnZombieIncrease;
    [SerializeField] private float musicVolume;

    [SerializeField] private float smoothSpeed;
    [SerializeField] private float sensibility;

    public Button map1Button;
    public Button continueButton;
    [SerializeField] private string selectedMap;

    void Start()
    {
        smoothSpeedSlider.minValue = 1f;
        smoothSpeedSlider.maxValue = 10f;
        sensibilitySlider.minValue = 1f;
        sensibilitySlider.maxValue = 5f;

        waveInterval = 20f;
        spawnZombieIncrease = 2;
        musicVolume = 0.5f;
        smoothSpeed = 10f;
        sensibility = 1f;

        LoadSettings();

        normalDifficultyButton.onClick.AddListener(SetNormalDifficulty);
        hardcoreDifficultyButton.onClick.AddListener(SetHardcoreDifficulty); 

        map1Button.onClick.AddListener(() => SelectMap("Game"));
        continueButton.onClick.AddListener(LoadSelectedMap);

        saveButton.onClick.AddListener(SaveSettings);
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("WaveInterval"))
        {
            waveInterval = PlayerPrefs.GetFloat("WaveInterval");
        }
        if (PlayerPrefs.HasKey("SpawnZombieIncrease"))
        {
            spawnZombieIncrease = PlayerPrefs.GetInt("SpawnZombieIncrease");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicVolumeSlider.value = musicVolume;
        }
        if (PlayerPrefs.HasKey("SmoothSpeed"))
        {
            smoothSpeed = PlayerPrefs.GetFloat("SmoothSpeed");
            smoothSpeedSlider.value = smoothSpeed;
        }
        if (PlayerPrefs.HasKey("Sensibility"))
        {
            sensibility = PlayerPrefs.GetFloat("Sensibility");
            sensibilitySlider.value = sensibility;
        }
        if (PlayerPrefs.HasKey("SelectedMap"))
        {
            selectedMap = PlayerPrefs.GetString("SelectedMap");
        }
    }

    public void SaveSettings()
    {
        smoothSpeed = smoothSpeedSlider.value;
        sensibility = sensibilitySlider.value;

        PlayerPrefs.SetFloat("WaveInterval", waveInterval);
        PlayerPrefs.SetInt("SpawnZombieIncrease", spawnZombieIncrease);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SmoothSpeed", smoothSpeed);
        PlayerPrefs.SetFloat("Sensibility", sensibility);
        //PlayerPrefs.SetString("SelectedMap", selectedMap);

        PlayerPrefs.Save();

        Debug.Log("Settings Saved");
    }

    private void SetNormalDifficulty()
    {
        waveInterval = 20f;
        spawnZombieIncrease = 2;
    }
    private void SetHardcoreDifficulty()
    {
        waveInterval = 10f;
        spawnZombieIncrease = 10;
    }

    private void SelectMap(string mapName)
    {
        selectedMap = mapName;

        PlayerPrefs.SetString("SelectedMap", selectedMap);
    }
    public void LoadSelectedMap()
    {
        SceneManager.LoadScene(selectedMap);
    }
}
