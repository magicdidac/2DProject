using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreParallax : MonoBehaviour
{

    [SerializeField] private GameObject cam = null;

    [Header("All Parallax")]
    [SerializeField] private ParallaxLevel[] parallaxes;

    private void Start()
    {
        foreach(ParallaxLevel pl in parallaxes)
        {
            foreach(Parallax p in pl.parallaxes)
            {
                p.SetCamera(cam);
                p.SetParallaxEffect(pl.parallaxEffect);
            }
        }
    }

}
