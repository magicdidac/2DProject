using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    [HideInInspector] private GameController gc = null;
    [SerializeField] private Rigidbody2D rb2d = null;
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private Collider2D col = null;

    [HideInInspector] private Collider2D playerColOne = null;
    [HideInInspector] private Collider2D playerColTwo = null;
    [HideInInspector] private Collider2D enemyCol = null;

    [SerializeField] private Collider2D[] otherRocks = null;

    [HideInInspector] private bool isLaunched = false;

    private void Start()
    {
        gc = GameController.instance;
    }


    private void Update()
    {

        if (gc.player != null)
        {
            if (enemyCol == null || enemyCol == null)
            {
                playerColOne = gc.player.GetComponent<CircleCollider2D>();
                playerColTwo = gc.player.GetComponent<BoxCollider2D>();
                enemyCol = gc.enemy.GetComponent<Collider2D>();
            }

            Physics2D.IgnoreCollision(playerColOne, col, true);
            Physics2D.IgnoreCollision(playerColTwo, col, true);
            Physics2D.IgnoreCollision(enemyCol, col, true);
            foreach (Collider2D c in otherRocks)
            {
                Physics2D.IgnoreCollision(c, col, true);
            }

        }

        if (isLaunched && rb2d.velocity.x > 0)
            transform.Rotate(new Vector3(0, 0, -5));
    }

    public void Launch()
    {
        if (isLaunched)
            return;

        isLaunched = true;

        rb2d.bodyType = RigidbodyType2D.Dynamic;
        var emission = ps.emission;
        emission.rateOverTime = 0;

        float yForce = (Random.Range(0, 2) == 1) ? Random.Range(5.0f, 8.0f) : Random.Range(2.0f, 4.0f);

        rb2d.AddForce(new Vector2(Random.Range(7.0f, 10.0f), yForce), ForceMode2D.Impulse);


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
