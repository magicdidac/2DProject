using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{

    #region Variables

    [HideInInspector] private GameController gc = null;
    [SerializeField] private Rigidbody2D rb2d = null;
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private Collider2D col = null;

    [HideInInspector] private Collider2D playerColOne = null;
    [HideInInspector] private Collider2D playerColTwo = null;
    [HideInInspector] private Collider2D enemyCol = null;

    [SerializeField] private Collider2D[] otherRocks = null;

    #endregion


    #region Initializers

    //Start
    private void Start()
    {
        gc = GameController.instance;

        float yForce = (Random.Range(0, 2) == 1) ? Random.Range(5.0f, 8.0f) : Random.Range(-2.0f, -4.0f);

        rb2d.AddForce(new Vector2(Random.Range(7.0f, 10.0f), yForce), ForceMode2D.Impulse);

        Invoke("Remove", 5);
    }

    #endregion


    //Update
    private void Update()
    {
        if (gc.player != null)
        {
            if(enemyCol == null || enemyCol == null)
            {
                playerColOne = gc.player.GetComponent<CircleCollider2D>();
                playerColTwo = gc.player.GetComponent<BoxCollider2D>();
                enemyCol = gc.enemy.GetComponent<Collider2D>();
            }

            Physics2D.IgnoreCollision(playerColOne, col, true);
            Physics2D.IgnoreCollision(playerColTwo, col, true);
            Physics2D.IgnoreCollision(enemyCol, col, true);
            foreach(Collider2D c in otherRocks)
            {
                Physics2D.IgnoreCollision(c, col, true);
            }

        }

        if(rb2d.velocity.x < .2f && rb2d.velocity.y > -.2f)
        {
            var emission = ps.emission;
            emission.rateOverTime = 0;
        }

        /*if (rb2d.velocity.y == 0)
            rb2d.velocity = Vector2.zero;*/

    }


    #region Other

    private void Remove()
    {
        Destroy(gameObject);
    }

    #endregion

}
