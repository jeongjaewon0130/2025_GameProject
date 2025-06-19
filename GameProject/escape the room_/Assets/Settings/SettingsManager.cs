using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;     // ���� �г� (�Ѱ� ���� ��)
    public Slider volumeSlider;          // �����̴� ����

    void Start()
    {
        settingsPanel.SetActive(false);  // ó���� �� ���̰�

        // �����̴� �ʱⰪ�� ���� ���� ����ȭ
        volumeSlider.value = AudioListener.volume;

        // �����̴� ���� �ٲ� ������ ���� ����
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        // Debug.Log("����: " + value);
    }
}

