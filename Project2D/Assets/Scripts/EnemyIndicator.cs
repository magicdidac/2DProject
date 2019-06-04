using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]

//Esto deberia hacerlo el enemy; crear child en el prefab del enemy

public class EnemyIndicator : MonoBehaviour
{

    [HideInInspector] private GameController gc;
    [HideInInspector] private Image spr;
    [HideInInspector] public Animator anim;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteTop = null;
    [SerializeField] private Sprite spriteMiddle = null;
    [SerializeField] private Sprite spriteBottom = null;

    private void Start()
    {
        gc = GameController.instance;
        spr = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        switch (gc.GetFloor())
        {
            case 1:
                spr.sprite = spriteTop;
                break;
            case 0:
                spr.sprite = spriteMiddle;
                break;
            case -1:
                spr.sprite = spriteBottom;
                break;
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
