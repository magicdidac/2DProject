using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOFFButton : MonoBehaviour
{

    public Color pressed;
    public Color nonPressed;
    public GameObject on;

    [HideInInspector]
    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().color = (PlayerPrefs.GetInt("MusicActive") == 0) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetInt("MusicActive") == 0) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            PlayerPrefs.SetInt("MusicActive", 0);
            on.GetComponent<MusicONButton>().isClicked = false;
            GetComponent<Image>().color = pressed;
            on.GetComponent<Image>().color = nonPressed;
            GameController.instance.audioController.PauseMusic();
        }
    }
}
