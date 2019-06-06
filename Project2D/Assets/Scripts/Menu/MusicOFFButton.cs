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
        transform.GetChild(0).GetComponent<Text>().color = (PlayerPrefs.GetInt("MusicActive") == 0) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetInt("MusicActive") == 0) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            PlayerPrefs.SetInt("MusicActive", 0);
            on.GetComponent<MusicONButton>().isClicked = false;
            transform.GetChild(0).GetComponent<Text>().color = pressed;
            on.transform.GetChild(0).GetComponent<Text>().color = nonPressed;
            GameController.instance.audioController.PauseMusic();
        }
    }
}
