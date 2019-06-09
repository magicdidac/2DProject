using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{

    [SerializeField] private Collider2D myCol = null;
    [SerializeField] private Collider2D col = null;

    private void Update()
    {
        Physics2D.IgnoreCollision(col, myCol, true);
    }


}
