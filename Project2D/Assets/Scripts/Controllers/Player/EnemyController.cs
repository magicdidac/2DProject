﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AMoveController
{

    #region Variables

    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject topGranade = null;
    [SerializeField] private GameObject midGranade = null;
    [SerializeField] private GameObject botGranade = null;
    [Header("Particles")]
    [SerializeField] private ParticleSystem smoke = null;


    [Header("AI Vars")]
    [Range(0, 5)] [SerializeField] public float RadiusDetection = 1.3f;
    [Range(0, 5)] [SerializeField] public float verticalDetectionDistance = 2;
    [Range(0, 5)] [SerializeField] public float verticalDetectionXOffset = .5f;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask obstacleMask;

    [SerializeField] public Animator shield;

    [SerializeField] public Collider2D col = null;


    #endregion

    private void Start()
    {
        gc.enemy = this;
        ChangeState(new ESGrounded(this));
    }


    #region Initializers

    private void FixedUpdate()
    {
        currentState.FixedUpdate();

        try
        {

            if (!isDead)
            {
                if ((!gc.player.isRope && !gc.player.isTrampoline) && (gc.GetFloor() == 0 || (gc.GetFloor() != 0 && gc.GetEnemyDistance() > gc.GetMinimumEnemyDistance() + 1)))
                    rigidbody2d.velocity = new Vector2((gc.player.isSliding) ? gc.GetVelocity(model.slideSpeed) : gc.GetVelocity(model.normalSpeed), rigidbody2d.velocity.y);
                else if (gc.player.isRope || gc.player.isTrampoline)
                    rigidbody2d.velocity = new Vector2(1, rigidbody2d.velocity.y);
                else if (gc.GetEnemyDistance() <= gc.GetMinimumEnemyDistance() + 1)
                    transform.position = new Vector2(gc.player.transform.position.x + (gc.GetMinimumEnemyDistance() + 1), transform.position.y);
            }

        }
        catch { }

    }


    #endregion

    private void Update()
    {
        currentState.Update();
        isGrounded = detectCollision(groundMask, model.offset);
        UpdateParticles();
    }

    private void LateUpdate()
    {
        currentState.CheckTransition();
    }

    
    #region Colliders

    public bool DetectObstacleToJump()
    {
        //Horizontal RayCast
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y + .25f), Vector2.right, RadiusDetection);

        //If doesn't hit anything
        if (hit.collider == null) return false;

        return hit.collider.CompareTag("Box") || hit.collider.CompareTag("Kill");
    }

    public bool DetectObstacleToFall(LayerMask l_m)
    {
        //Horizontal RayCast
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.max.y + .5f), Vector2.right, RadiusDetection, l_m);

        //If doesn't hit anything
        if (hit.collider == null) return false;

        return rigidbody2d.velocity.y <= 0f && hit.collider.CompareTag("Kill");
    }

    public bool DetectGroundToLand()
    {
        return DetectGroundToLand("Platform");
    }

    public bool DetectGroundToLand(string tagToLand)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x + verticalDetectionXOffset, col.bounds.min.y), Vector2.down, verticalDetectionDistance);

        if (hit.collider == null) return false;

        return hit.collider.CompareTag(tagToLand);
    }

    public bool DetectObstacleUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.min.x, col.bounds.max.y), Vector2.up, .5f);

        if (hit.collider == null) return false;

        return hit.collider.CompareTag("Kill") || hit.collider.CompareTag("Box");
    }


    #endregion


    #region Others

    public void attack()
    {
        gc.audioController.PlaySound("charge");
        switch (gc.GetFloor())
        {
            case 1:
                animator.SetTrigger("T-MidGranadeShoot");
                break;
            case 0:

                if (gc.GetEnemyDistance() > 3 && Random.Range(0, 2) == 1)
                    animator.SetTrigger("T-MidLaserShoot");
                else
                    animator.SetTrigger("T-MidGranadeShoot");
                break;
            case -1:
                animator.SetTrigger("T-MidGranadeShoot");
                break;
        }
    }

    private bool isInState(AState state1, AState state2)
    {
        return (state1 == state2);
    }

    public void LaserShoot()
    {
        Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
    }

    public void GranadeShoot()
    {
        switch (gc.GetFloor())
        {
            case 1:
                Instantiate(topGranade, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
            case 0:
                Instantiate(midGranade, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
            case -1:
                Instantiate(botGranade, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
        }

    }

    private void OnBecameInvisible()
    {
        if (gc.player.isDead)
            gameObject.SetActive(false);
    }


    private void UpdateParticles()
    {
        if (!isGrounded)
            StopEmission(smoke);
        else
            SetRateOverTimeEmission(smoke, 50);

    }

    private void StopEmission(ParticleSystem ps)
    {
        SetRateOverTimeEmission(ps, 0);
    }

    private void SetRateOverTimeEmission(ParticleSystem ps, int ammount)
    {
        var emission = ps.emission;

        emission.rateOverTime = ammount;
    }


    #endregion


    #region Gizmos

    private void OnDrawGizmos()
    {
        DrawHorizontalDetectionOnAir();
        DrawHorizontalDetection();
        DrawVerticalDetection();
        DrawVerticalDetectionUp();
        DrawGroundRayCast();
    }

    private void DrawHorizontalDetection()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x, col.bounds.min.y + .25f),
            new Vector3(col.bounds.max.x + RadiusDetection, col.bounds.min.y + .25f)
            );
    }

    private void DrawHorizontalDetectionOnAir()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x, col.bounds.max.y + .5f),
            new Vector3(col.bounds.max.x + RadiusDetection, col.bounds.max.y + .5f)
            );
    }

    private void DrawVerticalDetection()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x + verticalDetectionXOffset, col.bounds.min.y),
            new Vector3(col.bounds.max.x + verticalDetectionXOffset, col.bounds.min.y - verticalDetectionDistance)
            );
    }

    private void DrawVerticalDetectionUp()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.min.x, col.bounds.max.y),
            new Vector3(col.bounds.min.x, col.bounds.max.y + .5f)
            );
    }

    private void DrawGroundRayCast()
    {
        Gizmos.color = Color.red;

        float distanceBetweenRays = (col.bounds.size.x - model.offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((col.bounds.min.x + (model.offset / 2)) + distanceBetweenRays * i, col.bounds.min.y, 0);
            Gizmos.DrawLine(startPoint, startPoint + (Vector3.down * .1f));
        }
    }

    #endregion

}
