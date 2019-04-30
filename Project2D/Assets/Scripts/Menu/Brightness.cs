using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Brightness");
    }

    public void ChangeBrightness()
    {
        /*PermanentLight._light*/GameObject.FindGameObjectWithTag("Light").GetComponent/*InChildren*/<Image>().color = new Color(0, 0, 0, (1 - GetComponent<Slider>().value) / 255 * 100); //Opacity value between 0-1
        PlayerPrefs.SetFloat("Brightness", GetComponent<Slider>().value); //Slider value beetween 0-1
    }
}
