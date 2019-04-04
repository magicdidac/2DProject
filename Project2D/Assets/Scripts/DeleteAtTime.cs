using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAtTime : MonoBehaviour
{
    [SerializeField]
    [Header("Time to Destroy (in seconds)")]
    private int time = 15;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
