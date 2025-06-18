using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("UI Panels")]
    public GameObject settingsPanel;

    [Header("Settings Controls")]
    public Slider volumeSlider;

    private bool isPaused = false;

    void Start()
    {
        Screen.fullScreen = false;
        settingsPanel.SetActive(false);

        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
        isPaused = true;
    }

    public void ResumeGame()
    {
        float volume = volumeSlider.value;
        SetVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();

        settingsPanel.SetActive(false);
        Time.timeScale = 1f; // 게임 재개
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void OnVolumeChanged(float value)
    {
        SetVolume(value);
    }

    private void SetVolume(float value)
    {
        if (value <= 0.0001f) value = 0.0001f;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
