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

    [HideInInspector] public float RadiusDetection { get; } = 1.3f;
    [SerializeField] public LayerMask groundMask;

    [HideInInspector] public ParticleSystem shield;
    [HideInInspector] public bool hasJump;

    private void Awake()
    {
        shield = GetComponentInChildren<ParticleSystem>();
        shield.Stop();
        ChangeState(new ESGrounded(this));
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
        isGrounded = detectCollision(groundMask, _playerModel.offset);
        //CalculateDistance();
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void AddDistance()
    {
        if (isDead) return;
        CancelInvoke("reduceDistance");
        distanceToIncrement = gc.enemyDistance + 1;
        InvokeRepeating("incrementDistance", 0, .1f);
    }

    private void incrementDistance()
    {
        if (isDead) return;
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
        if (isDead) return;
        if (gc.enemyDistance > 1.6f)
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
            ChangeState(new ESGrounded(this));
    }

    public void attack()
    {
        switch (gc.getFloor()) {
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
                Instantiate(granadePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                break;
        }
    }

    public void JumpSlideDetect(AMoveController pc)
    {
        EnemyController ec = (EnemyController)pc;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ec.transform.position.x + ec.RadiusDetection, ec.transform.position.y + 1),
            Vector2.right, ec.RadiusDetection);

        if (hit.collider == null) return;

        if (/*ec.isGrounded &&*/ (hit.collider.CompareTag("Box") || hit.collider.CompareTag("Kill")))
        {
            if (hit.collider.bounds.min.y >= 0.25f)
            {
                //SLIDE
                // ESSliding class la he creado pero no la uso, pensando por si lo necesitamos en un furuto con las nuevas animaciones
                //ec.ChangeState(new ESSliding(ec));
                Debug.Log("Slide");
                ec.anim.SetBool("isSliding", true);
            }

            else
            {
                //JUMP
                float limitedHeight = hit.collider.bounds.size.y;

                ec.rb.velocity = (ec.rb.position.y >= limitedHeight) ? Vector2.down * 10f : Vector2.up * 10f;
                //if (ec.rb.position.y <= limitedHeight) ec.rb.velocity = Vector2.up * 10f;
                hasJump = true;
                ec.anim.SetBool("isSliding", false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Gizmos.DrawLine(new Vector3(sp.bounds.max.x, sp.bounds.center.y, 0f), 
            new Vector3(sp.bounds.max.x + RadiusDetection, sp.bounds.center.y));

        DrawGroundRayCast(sp);
        DrawGroundLine(sp);
    }

    private void DrawGroundRayCast(SpriteRenderer sp)
    {
        float distanceBetweenRays = (sp.bounds.size.x - _playerModel.offset) / _playerModel.precisionDown;

        for (int i = 0; i <= _playerModel.precisionDown; i++)
        {
            Vector3 startPoint = new Vector3((sp.bounds.min.x + (_playerModel.offset / 2)) + distanceBetweenRays * i, sp.bounds.min.y, 0);
            Debug.DrawLine(startPoint, startPoint + (Vector3.down * .1f), Color.red);
        }
    }

    private void DrawGroundLine(SpriteRenderer sp)
    {
        Debug.DrawLine(new Vector3(sp.bounds.max.x, sp.bounds.center.y, 0f),
            new Vector3(sp.bounds.max.x, Vector2.down.y, 0f), Color.blue);
    }

    public virtual IEnumerator ChangeState(EnemyController ec)
    {
        yield return new WaitForSeconds(.9f);
        ec.anim.SetBool("isSliding", false);
        ec.ChangeState(new ESGrounded(ec));
    }
}
