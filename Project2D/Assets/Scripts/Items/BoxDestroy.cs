using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy : MonoBehaviour
{

    [SerializeField] private Rigidbody2D[] parts;
    
    public void Start()
    {
        foreach (Rigidbody2D rb in parts)
        {
            rb.AddForce(rb.transform.position - transform.position + new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), 0), ForceMode2D.Impulse);
        }
        Invoke("Remove", 3);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
