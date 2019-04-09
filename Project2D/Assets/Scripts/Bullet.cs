using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [HideInInspector] private GameController gc;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float speed = 0;

    private void Start()
    {
        gc = GameController.instance;
        switch (gc.floor)
        {
            case 0:
                rb.velocity = Vector2.left * speed;
                break;
            case -1:

                break;
        }
        GameObject.Destroy(gameObject,2);
    }


}
