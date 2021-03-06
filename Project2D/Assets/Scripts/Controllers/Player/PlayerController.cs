﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{

    public enum DeathType {Fall, Electricity, Shoot, Granade, EnemyRunAway, CatchEnemy};

    #region Variables

    [Header("Layers")]
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask trampolineMask;
    [SerializeField] public LayerMask boxMask;


    [HideInInspector] private GameObject downObject;
    [Header("Other Objects")]
    [SerializeField] public Transform handObject;
    [SerializeField] private GameObject deadPlayer = null;

    [Header("Particles")]
    [SerializeField] private ParticleSystem smoke = null;
    [SerializeField] private ParticleSystem sparks = null;


    [HideInInspector] private GameObject lastTriggerObject = null;

    [HideInInspector] public float fuel;

    [HideInInspector] private BoxCollider2D boxCollider;
    [HideInInspector] public bool canStunt;

    #endregion


    #region Initializers

    private void Start()
    {
        gc.player = this;
        fuel = model.maxFuel;
        boxCollider = GetComponent<BoxCollider2D>();
        ChangeState(new PSGrounded(this));
        gc.audioController.PlaySound("playerMove"); // <- cal si ja esta en playOnAwake
        canStunt = true;
    }

    #endregion

    private void FixedUpdate()
    {
        if (isDead)
            return;

        currentState.FixedUpdate();
    }

    private void Update()
    {
        if (isDead)
            return;

        UpdateParticles();        

        LayerMask[] groundMasks = new LayerMask[2];
        groundMasks[0] = groundMask;
        groundMasks[1] = boxMask;

        isGrounded = detectCollision(groundMasks, model.offset);
        detectObstacleCollision(boxMask);
        animator.SetBool("B-Ground", isGrounded);
        currentState.Update();
    }


    #region Other

    private void UpdateParticles()
    {
        if (!isGrounded)
            StopEmission(smoke);
        else
            SetRateOverTimeEmission(smoke, 50);

        if (!isTirolina)
            StopEmission(sparks);
        else
            SetRateOverTimeEmission(sparks, 50);
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

    private void LateUpdate()
    {
        if (isDead)
            return;

        currentState.CheckTransition();
    }


    public float GetVerticalDifferenceHand()
    {
        return handObject.position.y - transform.position.y;
    }

    public void Kill(DeathType dt)
    {
        if (isDead)
            return;


        isDead = true;

        GameObject playerDead = Instantiate(deadPlayer, transform.position, Quaternion.identity);
        playerDead.GetComponent<Rigidbody2D>().velocity = new Vector2(-rigidbody2d.velocity.x, Mathf.Abs(rigidbody2d.velocity.y)) * .25f;
        playerDead.GetComponent<PlayerDead>().SetDeadAnimation(dt);
        Destroy(gameObject);

    }

    public bool HaveFuel()
    {
        return fuel > 0;
    }

    public void ConsumeFuel()
    {
        if (!isGrounded) return;
        fuel -= Time.deltaTime;
        gc.uiController.UpdateFuel();
    }

    private void ReloadFuel()
    {
        fuel = model.maxFuel;
        gc.uiController.UpdateFuel();
        gc.audioController.PlaySound("pickFuel");
    }

    #endregion


    #region Triggers or Collisions

    private void detectObstacleCollision(LayerMask p_lm)
    {
        var hitList = new List<RaycastHit2D>();

        var headPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.max.y - 0.25f);
        var headHit = Physics2D.Raycast(headPoint, Vector3.right, 0.5f, p_lm);

        hitList.Add(headHit);

        var wheelPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
        var wheelhit = Physics2D.Raycast(wheelPoint, Vector3.right, 0.5f, p_lm);

        hitList.Add(wheelhit);

        foreach (var hit in hitList)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Box"))
                {
                    if (!isSliding && canStunt)
                    {
                        isStuned = true;
                    }

                    hit.collider.GetComponent<Box>().DestroyBox();
                }
                else gc.GameWin(false, DeathType.Electricity);
            }
        }
    }

    public void EnableStun()
    {
        Invoke("_EnableStun", 1f);
        
    }

    private void _EnableStun()
    {
        canStunt = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == lastTriggerObject && !col.tag.Contains("Tirolina")) //Detectar si el collaider que ha entrado no entra otra vez por los dos colliders del player
            return;



        

        if (col.CompareTag("Rope") && !isRope) //Si colisiona con una Rope
        {
            transform.SetParent(col.gameObject.transform); //Se hace hijo de la Rope
            rigidbody2d.velocity = Vector2.zero; //Detenemos al Player
            rigidbody2d.bodyType = RigidbodyType2D.Kinematic; //Le cambiamos el tipo de rb
            gc.AddFloor();
            isRope = true;
            col.GetComponent<Rope>().StartMovement(transform);
        }
        else if (col.CompareTag("Down") && col.gameObject != downObject)
        {
            downObject = col.gameObject;
            gc.SubtractFloor();
        }
        else if (col.CompareTag("Trampoline"))
        {
            isTrampoline = detectCollision(trampolineMask, model.trampolineOffset);
            if (isTrampoline)
                gc.AddFloor();
        }
        else if (col.tag.Contains("Tirolina"))
        {

            if (rigidbody2d.velocity.y > 0)
            {
                if (zipLine != null && zipLine.transform.GetChild(0).gameObject == col.gameObject)
                    return;
                if (col.gameObject == lastTriggerObject)
                    return;
            }

            zipLine = col.transform.parent.GetComponent<ZipLine>();
            isTirolina = true;
            if (zipLine.floorDiference < 0)
                gc.SubtractFloor();
            else if (zipLine.floorDiference > 0)
                gc.AddFloor();
        }
        else if (col.CompareTag("Coin"))
        {
            gc.scoreController.AddCoins(1);
            gc.audioController.PlayNestedSound("coin");
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("SuperCoin"))
        {
            gc.scoreController.AddCoins(5);
            gc.audioController.PlayNestedSound("coin");
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Combustible"))
        {
            ReloadFuel();
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Shoot"))
            gc.enemy.attack();
        else if (col.CompareTag("InstaKill"))
        {
            gc.GameWin(false, DeathType.Fall);
        }
        else if (col.CompareTag("EnemyShoot"))
        {
            Destroy(col.gameObject);
            gc.GameWin(false, DeathType.Shoot);
        }
        else if (col.CompareTag("EnemyGranade"))
        {
            gc.GameWin(false, DeathType.Granade);
        }

        lastTriggerObject = col.gameObject;

    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tirolina") && collision.gameObject == lastTriggerObject)
            lastTriggerObject = null;
    }*/

    #endregion


    #region Gizmos

    private void OnDrawGizmos()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        drawGroundRayCast();
        drawTrampolineRayCast();
        drawBoxDetectRayCast();
    }

    private void drawGroundRayCast()
    {
        float distanceBetweenRays = (sprite.bounds.size.x - model.offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sprite.bounds.min.x + (model.offset / 2)) + distanceBetweenRays * i, sprite.bounds.min.y, 0);
            Gizmos.DrawLine(startPoint, startPoint + (Vector3.down * .1f));
        }
    }

    private void drawTrampolineRayCast()
    {
        float distanceBetweenRays = (sprite.bounds.size.x - model.trampolineOffset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sprite.bounds.min.x + (model.trampolineOffset / 2)) + distanceBetweenRays * i, sprite.bounds.min.y, 0);
            Gizmos.DrawLine(startPoint, startPoint + (Vector3.down * .1f));
        }
    }

    private void drawBoxDetectRayCast()
    {
        Gizmos.color = Color.red;

        var boxCollider = GetComponent<BoxCollider2D>();
        var headPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.max.y - 0.25f);
        Gizmos.DrawLine(headPoint, headPoint + (Vector3.right * 0.5f));

        var wheelPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
        Gizmos.DrawLine(wheelPoint, wheelPoint + (Vector3.right * 0.5f));
    }

    #endregion

}
