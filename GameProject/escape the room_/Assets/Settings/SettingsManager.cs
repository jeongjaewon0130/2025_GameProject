using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;     // 설정 패널 (켜고 끄기 용)
    public Slider volumeSlider;          // 슬라이더 참조

    void Start()
    {
        settingsPanel.SetActive(false);  // 처음엔 안 보이게

        // 슬라이더 초기값과 현재 볼륨 동기화
        volumeSlider.value = AudioListener.volume;

        // 슬라이더 값이 바뀔 때마다 볼륨 조절
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        // Debug.Log("볼륨: " + value);
    }
}

