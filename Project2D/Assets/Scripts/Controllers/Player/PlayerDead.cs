using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    public void SetDeadAnimation(PlayerController.DeathType dt)
    {
        GameController.instance.audioController.PlaySound("playerDeath");
        switch (dt)
        {
            case PlayerController.DeathType.Shoot:
                anim.SetTrigger("ShootDead");
                break;
            case PlayerController.DeathType.Electricity:
                anim.SetTrigger("ElectricityDead");
                break;
            case PlayerController.DeathType.Granade:
                anim.SetTrigger("ExplosionDead");
                break;
            case PlayerController.DeathType.Fall:
                anim.SetTrigger("ShootDead");
                break;
            case PlayerController.DeathType.CatchEnemy:
                anim.SetTrigger("ShootDead");
                break;
            case PlayerController.DeathType.EnemyRunAway:
                anim.SetTrigger("ShootDead");
                break;
            default:
                anim.SetTrigger("ShootDead");
                break;
        }
    }
}
