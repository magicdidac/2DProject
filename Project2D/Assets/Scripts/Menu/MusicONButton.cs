using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicONButton : MonoBehaviour
{
    public AudioMixer mixer;
    public Color pressed;
    public Color nonPressed;
    public GameObject off;

    [HideInInspector]
    public bool isClicked;

    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().color = (PlayerPrefs.GetInt("MusicActive") == 1) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetFloat("MasterVolume") == 1f) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            //PlayerPrefs.SetInt("MusicActive", 1);
            off.GetComponent<MusicOFFButton>().isClicked = false;
            transform.GetChild(0).GetComponent<Text>().color = pressed;
            off.transform.GetChild(0).GetComponent<Text>().color = nonPressed;
            mixer.SetFloat("MasterVolume", 1f);
            PlayerPrefs.SetFloat("MasterVolume", 1f);
            Debug.Log("MusicOOFFButton: " + PlayerPrefs.GetFloat("MasterVolume"));

            //GameController.instance.audioController.ResumeAudio();

        }
    }
}
