using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGizmos : MonoBehaviour
{
    
	[SerializeField]
	private Color _color;

	private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 10, _color);
    }

}
