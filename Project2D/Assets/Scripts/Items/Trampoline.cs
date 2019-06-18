using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private GameObject particles = null;

    public void Shoot()
    {
        particles.SetActive(true);
    }

}
