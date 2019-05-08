﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicONButton : MonoBehaviour
{

    public Color pressed;
    public Color nonPressed;
    public GameObject off;

    [HideInInspector]
    public bool isClicked;

    void Start()
    {
        GetComponent<Image>().color = (PlayerPrefs.GetInt("MusicActive") == 1) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetInt("MusicActive") == 1) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            PlayerPrefs.SetInt("MusicActive", 1);
            off.GetComponent<MusicOFFButton>().isClicked = false;
            GetComponent<Image>().color = pressed;
            off.GetComponent<Image>().color = nonPressed;
            /*if (GameManager._manager.mainMenu)
            {
                SoundManager._audioManager.PlayMusic("menuMusic");
                SoundManager._audioManager.PlayMusic("seaSound");
            }*/
        }
    }
}