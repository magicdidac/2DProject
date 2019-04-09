using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Granade : MonoBehaviour
{

    [HideInInspector] private CircleCollider2D col;
    [HideInInspector] private BoxCollider2D box;
    [HideInInspector] private Rigidbody2D rb;
    [HideInInspector] private ParticleSystem ps;
    [HideInInspector] private GameObject spr;
    [HideInInspector] private GameController gc;

    [HideInInspector] private bool isExploted = false;
    [HideInInspector] private bool isFalling = false;

    private void Start()
    {
        gc = GameController.instance;
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spr = transform.GetChild(0).gameObject;
        ps = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!isFalling)
        {
            if((gc.floor > -1 && transform.position.y > (gc.floor * 8)+1.5f) || (gc.floor == -1 && transform.position.y < (gc.floor * 8) + 1.5f))
            {
                isFalling = true;
                box.enabled = true;
                rb.velocity = (gc.floor == -1) ? new Vector2(4, -7) : new Vector2(4, 7);
                return;
            }
            rb.velocity = (gc.floor == -1) ? new Vector2(7.5f, -15) : new Vector2(7.5f, 15);
            return;
        }

        if(!isExploted && (transform.position.x - gc.player.transform.position.x) < 1)
        {
            isExploted = true;
            explote();
            InvokeRepeating("increaseCol", 0, .05f);
        }
    }

    private void increaseCol()
    {

        col.radius += .25f;

        if (col.radius > 1.5f)
        {
            CancelInvoke();
            Destroy(gameObject, .1f);
        }
    }

    private void explote()
    {
        ps.Play();
        spr.SetActive(false);
    }

}
