using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Chunk", menuName = "Chunks/Chunk")]
public class Chunk : ScriptableObject
{
    [SerializeField] public float lenght;

    [SerializeField] public GameObject prefab;
    
}
