using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSONButton : MonoBehaviour
{
    public Color pressed;
    public Color nonPressed;
    public GameObject off;

    [HideInInspector]
    public bool isClicked;

    void Start()
    {
        GetComponent<Image>().color = (PlayerPrefs.GetInt("ShowFPS") == 1) ? pressed : nonPressed;
        isClicked = (PlayerPrefs.GetInt("ShowFPS") == 1) ? true : false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            PlayerPrefs.SetInt("ShowFPS", 1);
            off.GetComponent<FPSOFFButton>().isClicked = false;
            GetComponent<Image>().color = pressed;
            off.GetComponent<Image>().color = nonPressed;
            GameController.instance.uiController.SwitchFPS(true);
        }
    }
}
