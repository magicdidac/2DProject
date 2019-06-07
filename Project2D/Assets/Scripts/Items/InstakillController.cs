using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillController : MonoBehaviour
{
    #region Variables

    [HideInInspector] private GameController gc;
    [HideInInspector] private AudioSource audioSource;

    #endregion


    #region Initializers
    void Start()
    {
        gc = GameController.instance;
        audioSource = GetComponent<AudioSource>();
    }

    #endregion


    void Update()
    {
        if (gc.player.isDead) audioSource.Stop();
    }
}
