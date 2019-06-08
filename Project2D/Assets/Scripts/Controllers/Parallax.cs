using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Variables

    [HideInInspector] private float length = 0f;
    //[HideInInspector] private float selfLength = 0f;
    [HideInInspector] private float startpos = 0f;
    [SerializeField] private GameObject cam = null;
    [SerializeField] private float parallaxEffect = 0f;

    #endregion


    #region Initializers

    //Start
    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        //selfLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    #endregion


    //Update

    //FixedUpdate
    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length*2) startpos += length*6;
        //else if (temp < startpos - length) startpos -= length*5;
    }
}
