using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool active;
    private GameObject pauseMenu;
    private GameObject optionsMenu;
    private AState ant;

    // Use this for initialization
    void Start()
    {
        pauseMenu = transform.GetChild(0).gameObject;
        optionsMenu = transform.GetChild(1).gameObject;

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void Pause()
    {
        active = !active;
        pauseMenu.SetActive(active);
        //GameManager._gameManager.isPaused = active;
        Time.timeScale = (active) ? 0 : 1;

    }
}
