﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Variables

    [HideInInspector] private float length = 0f;
    [HideInInspector] private float startpos = 0f;
    [HideInInspector] private GameObject cam = null;
    [HideInInspector] private float parallaxEffect = 0f;
    [HideInInspector] private int bkCount = 0;

    #endregion


    #region Initializers

    //Start
    private void OnEnable()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    #endregion


    //FixedUpdate
    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length*2) startpos += length*bkCount - .2f;
    }


    #region Setters or Variable Modifiers

    public void SetCamera(GameObject c) { cam = c; }

    public void SetParallaxEffect(float pe) { parallaxEffect = pe; }

    public void SetBKCount(int count) { bkCount = count; }

    #endregion

}
