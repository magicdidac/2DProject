using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicOFFButton : MonoBehaviour
{
    public Color pressed;
    public Color nonPressed;
    public GameObject on;

    [HideInInspector]
    public bool isClicked;


    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().color = (PlayerPrefs.GetInt("MusicActive") == 0) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetFloat("MasterVolume") == 1f) ? false : true;
    }

    public void OnClick()
    {
        if (!isClicked)
        { 
            on.GetComponent<MusicONButton>().isClicked = false;
            transform.GetChild(0).GetComponent<Text>().color = pressed;
            on.transform.GetChild(0).GetComponent<Text>().color = nonPressed;
            AudioListener.pause = true;
        }
    }
}
