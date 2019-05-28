using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChunkStore", menuName = "Chunks/ChunkStore")]
public class ChunkStore : ScriptableObject
{
    
    [SerializeField] public Chunk[] topTChunks;
    [SerializeField] public Chunk[] midTChunks;
    [SerializeField] public Chunk[] botTChunks;

    [Header("Challenge Chunks")]
    [SerializeField] public Chunk[] easy;
    [SerializeField] public Chunk[] normal;
    [SerializeField] public Chunk[] hard;

}
