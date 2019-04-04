using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Chunk", menuName = "Chunk")]
public class Chunk : ScriptableObject
{
    public float lenght;

    public GameObject prefab;
    
}
