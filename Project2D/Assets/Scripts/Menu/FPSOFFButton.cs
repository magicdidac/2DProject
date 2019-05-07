using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSOFFButton : MonoBehaviour
{
    public Color pressed;
    public Color nonPressed;
    public GameObject on;

    [HideInInspector]
    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().color = (PlayerPrefs.GetInt("ShowFPS") == 0) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetInt("ShowFPS") == 0) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            PlayerPrefs.SetInt("ShowFPS", 0);
            on.GetComponent<FPSONButton>().isClicked = false;
            GetComponent<Image>().color = pressed;
            on.GetComponent<Image>().color = nonPressed;
            GameController.instance.ShowFPS(false);
        }
    }
}
