using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void PlayGame()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnFadeComplete(int index)
    {
        GameController.instance.LoadScene(index);
    }
}
