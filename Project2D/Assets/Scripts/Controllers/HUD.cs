using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Image fuelBar;
 
    // Start is called before the first frame update
    void Start()
    {
        fuelBar = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFuelBar(float value)
    {
        fuelBar.fillAmount = value / 10;
    }
}
