using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    #region Variables

    [SerializeField] private Sprite[] sprites = null;

    #endregion


    #region Initializers

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    #endregion

    public void OnDestroy()
    {
        //TODO: Create a Particle System to indicate the destruction of the box
    }
}
