using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    //Self Components
    [Header("Self Components")]
    [SerializeField] private Rigidbody2D rb2d = null;
    [HideInInspector] private AudioSource audioSource;

    //External Components
    [HideInInspector] public Transform playerTransform;

    //Enable Rope Movement
    [HideInInspector] private Vector3 initialPosition;
    [HideInInspector] private bool allowMovement = false;

    //Properties
    [Header("Properties")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int offset = 8;

    private void Awake()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!allowMovement)
            return;

        if (transform.position.y < initialPosition.y + offset)
            rb2d.velocity = Vector2.up * speed;
        else if(transform.position.y > initialPosition.y + offset) {
            allowMovement = false;
            playerTransform.parent = null;
            rb2d.velocity = Vector2.zero;
        }
    }

    public void StartMovement(Transform playerT)
    {
        playerTransform = playerT;
        allowMovement = true;
        audioSource.Play();
    }

}
