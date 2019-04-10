﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGizmos : MonoBehaviour
{
    
	[SerializeField]
	private Color _color = Color.red;

    [SerializeField]
    private Vector3 endPoint = Vector3.up * 10;

	private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + endPoint, _color);
    }

}
