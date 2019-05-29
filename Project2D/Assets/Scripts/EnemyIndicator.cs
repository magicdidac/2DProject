using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

//Esto deberia hacerlo el enemy; crear child en el prefab del enemy

public class EnemyIndicator : MonoBehaviour
{

    [HideInInspector] private GameController gc;
    [HideInInspector] private SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteTop = null;
    [SerializeField] private Sprite spriteBottom = null;

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
            spr.sprite = spriteBottom;
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 2, transform.position.z);
        }
        else
        {
            spr.sprite = spriteTop;
            transform.position = new Vector3(transform.position.x, transform.parent.position.y - 3, transform.position.z);
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
