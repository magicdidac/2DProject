using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AMoveController
{
    [SerializeField] private float reduceFactor = .5f;
    [HideInInspector] public float distanceBetween = 5;
    [HideInInspector] private float distanceToIncrement;

    private void Awake()
    {
        InvokeRepeating("reduceDistance", 5, .1f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + distanceBetween, transform.position.y, transform.position.z), .25f);
    }

    public void AddDistance()
    {
        CancelInvoke("reduceDistance");
        distanceToIncrement = distanceBetween + 1;
        InvokeRepeating("incrementDistance", 0, .1f);
    }

    private void incrementDistance()
    {
        if (distanceBetween >= distanceToIncrement || distanceBetween >= 9.4f)
        {
            CancelInvoke("incrementDistance");
            InvokeRepeating("reduceDistance", 5, .1f);
        }
        else
            distanceBetween += .1f;
    }

    private void reduceDistance()
    {
        if(distanceBetween > 1.6f)
            distanceBetween -= reduceFactor;
    }

}
