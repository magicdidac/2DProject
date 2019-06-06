using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class AMoveController : MonoBehaviour
{
    //GameController reference
    [HideInInspector] protected GameController gc;

    //Components
    [Header("Components")]
    [SerializeField] public Rigidbody2D rigidbody2d = null;
    [SerializeField] public SpriteRenderer sprite = null;
    [SerializeField] public Animator animator = null;
    

    //State
    [HideInInspector] public AState currentState = null;

    //Model
    [Header("Model")]
    [SerializeField] public PlayerModel model = null;

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

    private void Awake()
    {
        gc = GameController.instance;
        model = Instantiate(model);
    }

    public void ChangeState(AState ps) { currentState = ps; }

    //p_lm: la layer que quieres detectar como suelo
    //p_offset: el offset que deja por cada lado del sprite
    //Función para detectar las colisiones con el suelo siedo este con la layer que se pase por  parametro
    public bool detectCollision(LayerMask p_lm, float p_offset)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = (sprite.bounds.size.x - p_offset) / model.precisionDown;

        for (int i = 0; i <= model.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sprite.bounds.min.x + (p_offset / 2)) + distanceBetweenRays * i, sprite.bounds.min.y, 0);
            hits.Add(Physics2D.Raycast(startPoint, Vector2.down, .1f, p_lm));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
                return true;
        }

        return false;
    }

    public bool detectCollision(LayerMask[] layerMasks, float offset)
    {
        foreach(LayerMask lm in layerMasks)
        {
            bool result = detectCollision(lm, offset);
            if (result)
                return true;
        }

        return false;

    }

}
