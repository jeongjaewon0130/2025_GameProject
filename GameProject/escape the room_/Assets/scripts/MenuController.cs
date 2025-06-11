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
    public TMP_Dropdown resolutionDropdown;
    public Slider sensitivitySlider;

    private Resolution[] resolutions;

    


    void Start()
    {
        Screen.fullScreen = false;

        // 설정 패널 비활성화
        settingsPanel.SetActive(false);

        // 해상도 목록 불러오기
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }

        // 🔄 저장된 설정 불러오기
        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentIndex);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();

        Resolution res = resolutions[savedIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume); // 볼륨 초기 적용

        float savedSensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
        sensitivitySlider.value = savedSensitivity;
    }

    public void StartNewGame()
    {
        Debug.Log("새 게임 시작!");
        // 씬 전환 가능
    }

    public void LoadGame()
    {
        Debug.Log("불러오기 클릭됨");
        // 저장된 게임 데이터 로드
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // 🔉 볼륨 저장 및 적용
        float volume = volumeSlider.value;
        SetVolume(volume);

        // 🎮 설정 저장
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.Save();

        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        Debug.Log($"해상도 변경: {res.width} x {res.height}");
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
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
}
