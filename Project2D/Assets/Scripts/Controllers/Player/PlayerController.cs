using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{   

    //Layers
    [Header("Layers")]
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask trampolineMask;
    

    [HideInInspector] private GameObject downObject;
    

    [HideInInspector] private GameObject lastTriggerObject = null;

    [HideInInspector] public float fuel;

    private void Start()
    {
        gc.player = this;
        fuel = model.maxFuel;
        ChangeState(new PSGrounded(this));
        AudioController._audioManager.PlaySound("playerMove"); // <- cal si ja esta en playOnAwake
    }
    
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

        isGrounded = detectCollision(groundMask, model.offset);
        animator.SetBool("B-Ground",isGrounded);
        currentState.Update();
    }

    private void LateUpdate()
    {
        if (isDead)
            return;

        currentState.CheckTransition();
    }


    public void Kill()
    {
        if (isDead)
            return;

        ChangeState(new PSDead(this));
        
        isDead = true;

        Debug.LogWarning("Add Animation Trigger");
    }

    public bool HaveFuel()
    {
        return fuel > 0;
    }

    public void ConsumeFuel()
    {
        fuel -= Time.deltaTime;
        gc.uiController.UpdateFuel();
    }

    private void ReloadFuel()
    {
        fuel = model.maxFuel;
        gc.uiController.UpdateFuel();
    }

    //Detect collisions
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == lastTriggerObject) //Detectar si el collaider que ha entrado no entra otra vez por los dos colliders del player
            return;

        lastTriggerObject = col.gameObject;

        if (col.CompareTag("Box")) //Si colisiona con una Box
        {
            if (isGrounded && !animator.GetBool("B-Slide")) //si no está deslizandose entonces se relantiza
                isStuned = true;

            col.gameObject.SetActive(false); //elimina el obstaculo
        }

        else if (col.CompareTag("Rope") && !isRope) //Si colisiona con una Rope
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
            zipLine = col.gameObject.transform.parent.GetComponent<ZipLine>();
            isTirolina = true;
            if (zipLine.floorDiference < 0)
                gc.SubtractFloor();
            else if(zipLine.floorDiference > 0)
                gc.AddFloor();
        }
        else if (col.CompareTag("Coin"))
        {
            gc.scoreController.AddCoins(1);
            AudioController._audioManager.PlayNestedSound("coin");
            col.gameObject.SetActive(false);
        }

        else if (col.CompareTag("SuperCoin"))
        {
            gc.scoreController.AddCoins(5);
            AudioController._audioManager.PlayNestedSound("coin");
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Combustible"))
        {
            ReloadFuel();
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
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        drawGroundRayCast();
        drawTrampolineRayCast();
    }

    private void drawGroundRayCast()
    {
        float distanceBetweenRays = (sprite.bounds.size.x - model.offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sprite.bounds.min.x + (model.offset / 2)) + distanceBetweenRays * i, sprite.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    private void drawTrampolineRayCast()
    {
        float distanceBetweenRays = (sprite.bounds.size.x - model.trampolineOffset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sprite.bounds.min.x + (model.trampolineOffset / 2)) + distanceBetweenRays * i, sprite.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.magenta);
        }
    }

    #endregion

}
