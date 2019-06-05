using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : AMoveController
{

    #region Variables

    [Header("Layers")]
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask trampolineMask;
    [SerializeField] public LayerMask boxMask;


    [HideInInspector] private GameObject downObject;
    [SerializeField] public Transform handObject;
    

    [HideInInspector] private GameObject lastTriggerObject = null;

    [HideInInspector] public float fuel;

    [HideInInspector] private BoxCollider2D boxCollider;

    #endregion


    #region Initializers

    private void Start()
    {
        gc.player = this;
        fuel = model.maxFuel;
        boxCollider = GetComponent<BoxCollider2D>();
        ChangeState(new PSGrounded(this));
        gc.audioController.PlaySound("playerMove"); // <- cal si ja esta en playOnAwake
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

        isGrounded = detectCollision(groundMask, model.offset);
        detectObstacleCollision(boxMask);
        animator.SetBool("B-Ground",isGrounded);
        currentState.Update();
    }


    #region Other
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
        if (!isGrounded) return; 
        fuel -= Time.deltaTime;
        gc.uiController.UpdateFuel();
    }

    private void ReloadFuel()
    {
        fuel = model.maxFuel;
        gc.uiController.UpdateFuel();
    }

    #endregion


    #region Triggers or Collisions

    private void detectObstacleCollision(LayerMask p_lm)
    {
        var hitList = new List<RaycastHit2D>();

        var headPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.max.y - 0.25f);
        var headHit = Physics2D.Raycast(headPoint, Vector3.right, 0.25f, p_lm);

        hitList.Add(headHit);

        var wheelPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
        var wheelhit = Physics2D.Raycast(wheelPoint, Vector3.right, 0.25f, p_lm);

        hitList.Add(wheelhit);

        foreach (var hit in hitList)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Box"))
                {
                    isStuned = true;
                    Destroy(hit.collider.gameObject);
                }
                else gc.GameWin(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == lastTriggerObject) //Detectar si el collaider que ha entrado no entra otra vez por los dos colliders del player
            return;

        lastTriggerObject = col.gameObject;

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
            zipLine = col.gameObject.transform.parent.GetComponent<ZipLine>();
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
            gc.GameWin(false);
    }

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
        Gizmos.DrawLine(headPoint, headPoint + (Vector3.right * 0.25f));

        var wheelPoint = new Vector3(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
        Gizmos.DrawLine(wheelPoint, wheelPoint + (Vector3.right * 0.25f));
    }

    #endregion

}
