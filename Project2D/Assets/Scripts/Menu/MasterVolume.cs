using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MasterVolume : MonoBehaviour
{
    public AudioMixer mixer;
    [HideInInspector] private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
    }

    public void ChangeVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
