using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{   

    [SerializeField] public LayerMask groundMask;   
    [SerializeField] public LayerMask trampolineMask;
    [SerializeField] private GameObject explosion = null;

    [HideInInspector] private GameObject downObject;
    [HideInInspector] private bool isExploded = false;

    [HideInInspector] private GameObject lastTriggerObject = null;

    private void Awake()
    {
        
        ChangeState(new PSGrounded(this));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isDead)
            return;

        currentState.FixedUpdate(this);
    }

    private void Update()
    {
        if (isDead)
        {
            if (!isExploded)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                spr.enabled = false;
                isExploded = true;
            }
            return;

        }

        if(anim != null)
            anim.SetBool("B-Ground",isGrounded);
        currentState.Update(this);
        isGrounded = detectCollision(groundMask, model.offset);
    }

    private void LateUpdate()
    {
        if (isDead)
            return;

        currentState.CheckTransition(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == lastTriggerObject)
            return;

        lastTriggerObject = col.gameObject;

        if (col.CompareTag("Box"))
        {
            if (isGrounded && !anim.GetBool("B-Slide"))
            {
                isStuned = true;
                col.gameObject.SetActive(false);
            }
            else col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Rope") && !isRope)
        {
            transform.SetParent(col.gameObject.transform);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            gc.setFloor(gc.getFloor() + 1);
            isRope = true;
            col.GetComponent<Rope>().startMovement();
        }
        else if (col.CompareTag("Down") && col.gameObject != downObject)
        {
            downObject = col.gameObject;
            gc.setFloor(gc.getFloor() - 1);
        }
        else if (col.CompareTag("Trampoline"))
        {
            isTrampoline = detectCollision(trampolineMask, model.trampolineOffset);
            if (isTrampoline)
                gc.setFloor(gc.getFloor() + 1);
        }
        else if (col.tag.Contains("Tirolina"))
        {
            zipLine = col.gameObject.transform.parent.GetComponent<ZipLine>();
            isTirolina = true;
            gc.setFloor(gc.getFloor() + zipLine.floorDiference);
        }
        else if (col.CompareTag("Coin"))
        {
            gc.AddCoins(1);
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("SuperCoin"))
        {
            gc.AddCoins(5);
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Combustible"))
        {
            gc.GetCombustible(col.gameObject.GetComponent<Combustible>()._combustibleModel.quantity);
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Shoot"))
            gc.enemy.attack();
        else if (col.CompareTag("Kill"))
            gc.GameWin(false);
    }


    #region Gizmos

    private void OnDrawGizmos()
    {
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        drawGroundRayCast();
        drawTrampolineRayCast();
    }

    private void drawGroundRayCast()
    {
        float distanceBetweenRays = (spr.bounds.size.x - model.offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (model.offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    private void drawTrampolineRayCast()
    {
        float distanceBetweenRays = (spr.bounds.size.x - model.trampolineOffset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (model.trampolineOffset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.magenta);
        }
    }

    #endregion

}
