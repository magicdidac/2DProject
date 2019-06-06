using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy03 : MonoBehaviour
{
    private void Start()
    {
        Invoke("Remove", 5f);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
