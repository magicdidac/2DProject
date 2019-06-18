using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopGranade : Granade
{

    #region Variables

    [SerializeField] private Collider2D col = null;

    #endregion

    #region Initializers

    //OnEnable
    private void OnEnable()
    {
        col.enabled = false;
        transform.position = new Vector3(gc.player.transform.position.x + 10, 6, 0);
    }

    #endregion

    protected new void Update()
    {
        base.Update();

        if (!col.enabled && transform.position.y < 9)
            rb2d.velocity = Vector2.up * 5;
        else if (!col.enabled)
        {
            col.enabled = true;
            if (gc.player.fuel < .5f)
            {
                Debug.Log("YAS");
                rb2d.AddForce(Vector2.one * -1, ForceMode2D.Impulse);
            }else
                rb2d.bodyType = RigidbodyType2D.Kinematic;
        }else if (col.enabled && gc.player.fuel > .5f)
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 9.5f, 0), .2f);
        }
        
    }

}
