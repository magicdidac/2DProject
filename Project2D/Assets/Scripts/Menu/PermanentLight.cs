using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentLight : MonoBehaviour
{
    //public static PermanentLight _light = null;

    private void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        /*if (_light == null) _light = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);*/
        GetComponent/*InChildren*/<Image>().color = new Color(0, 0, 0, (1 - PlayerPrefs.GetFloat("Brightness")) / 255 * 100); //Opacity value between 0-1
    }

    // Update is called once per frame
    void Update()
    {

    }
}
