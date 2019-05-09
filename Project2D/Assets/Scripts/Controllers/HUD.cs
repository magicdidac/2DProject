using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Image fuelBar;
    private float deltaTime;

    [HideInInspector] public GameObject fpsText;   

    // Start is called before the first frame update
    void Start()
    {
        fuelBar = transform.GetChild(0).GetComponent<Image>();
        fpsText = transform.GetChild(1).gameObject;

        fpsText.SetActive(GameController.instance.fpsShowed);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //if (!GameController.instance.isPaused) 
        fpsText.GetComponent<Text>().text = "FPS: " + Mathf.Ceil(fps).ToString();
    }

    public void ChangeFuelBar(float value)
    {
        fuelBar.fillAmount = value / GameController.instance.player.model.maxCombustible;
    }
}
