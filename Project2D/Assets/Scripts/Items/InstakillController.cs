using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillController : MonoBehaviour
{
    #region Variables

    [HideInInspector] private GameController gc;
    [HideInInspector] private AudioSource audioSource;
    [SerializeField] private Sprite[] sprites = null;

    #endregion


    #region Initializers
    void Start()
    {
        gc = GameController.instance;
        audioSource = GetComponent<AudioSource>();

        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

    }

    #endregion


    void Update()
    {

        if (gc.player != null && gc.player.isDead)
            audioSource.Stop();
    }
}
