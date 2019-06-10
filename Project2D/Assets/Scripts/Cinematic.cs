using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{

    [SerializeField] private VideoPlayer video = null;
    [SerializeField] private int changeSceneIndex = 0;


    [HideInInspector] private bool isInvokeActive = false;

    private void Update()
    {
        if(!isInvokeActive && video.isPlaying)
        {
            isInvokeActive = true;
            Invoke("changeScene", 23);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            changeScene();

    }

    private void changeScene()
    {
        SceneManager.LoadSceneAsync(changeSceneIndex);
    }

}
