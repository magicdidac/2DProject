using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void ChangeVolume()
    {
        AudioController._audioManager.setAllVolumes(GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("MasterVolume", GetComponent<Slider>().value);
    }
}
