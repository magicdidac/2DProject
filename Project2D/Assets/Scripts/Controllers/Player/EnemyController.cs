using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AMoveController
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject granadePrefab = null;
    

    [Range(0,5)][SerializeField] public float RadiusDetection = 1.3f;
    [Range(0,5)][SerializeField] public float verticalDetectionDistance = 2;
    [Range(0,5)][SerializeField] public float verticalDetectionXOffset = .5f;
    [SerializeField] public LayerMask groundMask;

    [SerializeField] public ParticleSystem shield;

    [SerializeField] public Collider2D col = null;

    private void Awake()
    {
        shield.Stop();
        ChangeState(new ESGrounded(this));
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);

        if (!isDead)
        {
            if (!gc.player.isRope && (gc.getFloor() == 0 || (gc.getFloor() != 0 && gc.getEnemyDistance() > gc.minEnemyDistance + 1)))
                rb.velocity = new Vector2((gc.player.isSliding) ? model.slideSpeed : model.normalSpeed, rb.velocity.y);
            else if (gc.player.isRope)
                rb.velocity = new Vector2(1, rb.velocity.y);
            else if (gc.getEnemyDistance() <= gc.minEnemyDistance + 1)
                transform.position = new Vector2(gc.player.transform.position.x + (gc.minEnemyDistance + 1), transform.position.y);
        }

    }

    private void Update()
    {
        currentState.Update(this);
        isGrounded = detectCollision(groundMask, model.offset);
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public  void attack()
    {
        switch (gc.getFloor()) {
            case 1:
                gc.enemyIndicator.LoadShoot();
                break;
            case 0:
                if (gc.getEnemyDistance() > 3 && Random.Range(0, 2) == 1)
                    anim.SetTrigger("T-MidLaserShoot");
                else
                    anim.SetTrigger("T-MidGranadeShoot");
                break;
            case -1:
                gc.enemyIndicator.LoadShoot();
                break;
        }
    }

    public void LaserShoot()
    {
        Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
    }

    public void GranadeShoot()
    {
        Instantiate(granadePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
        //granade.rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

    public bool DetectObstacleToJump()
    {
        //Horizontal RayCast
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y+.25f), Vector2.right, RadiusDetection);

        //If doesn't hit anything
        if (hit.collider == null) return false;

        return hit.collider.CompareTag("Box") || hit.collider.CompareTag("Kill");
    }

    public bool DetectObstacleToSlide()
    {
        //Horizontal RayCast
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.max.y + .5f), Vector2.right, RadiusDetection-.5f);

        //If doesn't hit anything
        if (hit.collider == null) return false;

        return hit.collider.CompareTag("Box") || hit.collider.CompareTag("Kill");
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

    #region Gizmos

    private void OnDrawGizmos()
    {
        DrawHorizontalDetectionUp();
        DrawHorizontalDetection();
        DrawVerticalDetection();
        DrawVerticalDetectionUp();
        DrawGroundRayCast();
    }

    private void DrawHorizontalDetection()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x, col.bounds.min.y+.25f),
            new Vector3(col.bounds.max.x + RadiusDetection, col.bounds.min.y+.25f)
            );
    }

    private void DrawHorizontalDetectionUp()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x, col.bounds.max.y + .5f),
            new Vector3(col.bounds.max.x + RadiusDetection-.5f, col.bounds.max.y + .5f)
            );
    }

    private void DrawVerticalDetection()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(col.bounds.max.x + verticalDetectionXOffset, col.bounds.min.y),
            new Vector3(col.bounds.max.x + verticalDetectionXOffset, col.bounds.min.y-verticalDetectionDistance)
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
