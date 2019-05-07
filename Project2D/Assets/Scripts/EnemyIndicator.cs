using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyIndicator : MonoBehaviour
{

    private GameController gc;
    private SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    private void Start()
    {
        gc = GameController.instance;
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(gc.enemy.transform.position.x, transform.position.y, transform.position.z), .25f);
    }

    private void Update()
    {
        if (transform.parent.position.y < gc.enemy.transform.position.y)
        {
            spr.flipY = true;
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 2, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y - 3, transform.position.z);
            spr.flipY = false;
        }
    }

    public void LoadShoot()
    {
        anim.SetTrigger("T-Shoot");
    }

    public void Shoot()
    {
        gc.enemy.GranadeShoot();
    }

}
