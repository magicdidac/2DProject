using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicOFFButton : MonoBehaviour
{
    public AudioMixer mixer;
    public Color pressed;
    public Color nonPressed;
    public GameObject on;

    [HideInInspector]
    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().color = (PlayerPrefs.GetInt("MusicActive") == 0) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetFloat("MasterVolume") == 1f) ? false : true;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            //PlayerPrefs.SetInt("MusicActive", 0);
            on.GetComponent<MusicONButton>().isClicked = false;
            transform.GetChild(0).GetComponent<Text>().color = pressed;
            on.transform.GetChild(0).GetComponent<Text>().color = nonPressed;
            mixer.SetFloat("MasterVolume", 0.0001f);
            PlayerPrefs.SetFloat("MasterVolume", 0.0001f);
            Debug.Log("MusicOOFFButton: " + PlayerPrefs.GetFloat("MasterVolume"));

            //GameController.instance.audioController.PauseAudio();
        }
    }
}
