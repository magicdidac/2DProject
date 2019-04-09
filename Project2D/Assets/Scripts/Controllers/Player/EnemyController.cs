using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AMoveController
{
    [SerializeField] private float reduceFactor = .5f;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject granadePrefab = null;
    [HideInInspector] private float distanceToIncrement;

    [HideInInspector] public bool canShoot = false;
    [HideInInspector] public bool canCharge = false;
    [HideInInspector] public float shootPosition;

    private void Awake()
    {
        ChangeState(new ESWaiting(this));
        InvokeRepeating("reduceDistance", 5, .1f);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
        transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + gc.enemyDistance, transform.position.y, transform.position.z), .25f);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void AddDistance()
    {
        CancelInvoke("reduceDistance");
        distanceToIncrement = gc.enemyDistance + 1;
        InvokeRepeating("incrementDistance", 0, .1f);
    }

    private void incrementDistance()
    {
        if (gc.enemyDistance >= distanceToIncrement || gc.enemyDistance >= gc.maxDistance)
        {
            CancelInvoke("incrementDistance");
            InvokeRepeating("reduceDistance", 5, .1f);
        }
        else
            gc.enemyDistance += .1f;
    }

    private void reduceDistance()
    {
        if(gc.enemyDistance > 1.6f)
            gc.enemyDistance -= reduceFactor;
    }

    public void calculateTimeToNextShoot()
    {
        Invoke("setupShootScenario", Random.Range(10f, 20f));
    }

    private void setupShootScenario()
    {
        if (gc.enemyDistance > 3f)
        {
            ChangeState(new ESShootingUp(this));
            gc.mapController.enemyNeedShoot = true;
        }
        else
            ChangeState(new ESWaiting(this));
    }

    public void attack()
    {
        switch (gc.floor) {
            case 1:
                Instantiate(granadePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
            case 0:
                if(gc.enemyDistance > 3 && Random.Range(0,2)==1)
                    Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                else
                    Instantiate(granadePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
            case -1:
                Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
        }
    }

}
