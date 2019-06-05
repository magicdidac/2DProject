using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    //GameController instance
    [HideInInspector] private GameController gc;

    //Components
    [Header("Self Components")]
    [SerializeField] private Rigidbody2D rb = null;

    //Properties
    [Header("Properties")]
    [SerializeField] private float speed = 0;

    private void Start()
    {
        gc = GameController.instance;
        gc.audioController.PlaySound("shot");
        rb.velocity = Vector2.left * speed;
        GameObject.Destroy(gameObject,2);
    }


}
