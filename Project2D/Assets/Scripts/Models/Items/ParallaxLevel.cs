using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLevel
{
    [Range(0.0f, 1.0f)]
    [SerializeField] public float parallaxEffect = 0f;

    [SerializeField] public Parallax[] parallaxes = null;
}
