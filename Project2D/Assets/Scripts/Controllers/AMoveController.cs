using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class AMoveController : MonoBehaviour
{

    [HideInInspector] public GameController gc;

    //Components
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public SpriteRenderer spr;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float combustible;

    //State
    [HideInInspector] public AState currentState;

    //Model
    [SerializeField] public PlayerModel model;

    //Control
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isTrampoline = false;
    [HideInInspector] public bool isStuned = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isRope = false;
    [HideInInspector] public bool isTirolina = false;
    [HideInInspector] public bool isTirolinaD = false;
    [HideInInspector] public bool isDead = false;

    //Others
    [HideInInspector] public ZipLine zipLine = null;

    private void Start()
    {
        gc = GameController.instance;
        anim = GetComponent<Animator>();
        model = Instantiate(model);
        combustible = model.maxCombustible;
    }

    public void ChangeState(AState ps) { currentState = ps; }

    public bool detectCollision(LayerMask p_lm, float p_offset)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = (spr.bounds.size.x - p_offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((spr.bounds.min.x + (p_offset / 2)) + distanceBetweenRays * i, spr.bounds.min.y, 0);
            hits.Add(Physics2D.Raycast(startPoint, Vector2.down, .1f, p_lm));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
                return true;
        }

        return false;
    }
}
