using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private void Start()
    {
        Invoke("Remove", 1.5f);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }


}
