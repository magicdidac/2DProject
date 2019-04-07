using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class AMoveController : MonoBehaviour
{

    //Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spr;
    [HideInInspector] public Animator anim;

    //State
    [HideInInspector] public AState currentState;

    //Model
    [SerializeField] public PlayerModel _playerModel;

    //Control
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isTrampoline = false;
    [HideInInspector] public bool isStuned = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isRope = false;
    [HideInInspector] public int floor = 0;

    public void ChangeState(AState ps) { currentState = ps; }

    public bool detectCollision(LayerMask p_lm, float p_offset)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        float distanceBetweenRays = (spr.bounds.size.x - p_offset) / _playerModel.precisionDown;

        for (int i = 0; i <= _playerModel.precisionDown; i++)
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
