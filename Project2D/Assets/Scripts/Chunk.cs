using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Chunk", menuName = "Chunks/Chunk")]
public class Chunk : ScriptableObject, IEqualityComparer<Chunk>
{
    [SerializeField] public float lenght;

    [SerializeField] public GameObject prefab;

    public bool Equals(Chunk x, Chunk y)
    {
        return (x.prefab.name == y.prefab.name && x.lenght == y.lenght);
    }

    public int GetHashCode(Chunk obj)
    {
        throw new System.NotImplementedException();
    }
}
