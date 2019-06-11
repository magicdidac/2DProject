using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Granade : MonoBehaviour
{

    #region Variables

    [HideInInspector] protected GameController gc = null;
    [SerializeField] protected Rigidbody2D rb2d = null;
    [SerializeField] protected GameObject explosion = null;

    #endregion


    #region Initializers

    //Awake
    private void Awake()
    {
        gc = GameController.instance;
    }

    #endregion


    //Update
    protected void Update()
    {
        if (transform.position.x <= gc.player.transform.position.x+.5f)
            Explode();

        if(rb2d.velocity.y == 0)
        {
            rb2d.velocity = Vector2.zero;
        }

    }


    #region Other

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    #endregion


}
