using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChunkStore", menuName = "Chunks/ChunkStore")]
public class ChunkStore : ScriptableObject
{

    [Header("Transition Chunks")]
    [SerializeField] public Chunk[] top;
    [SerializeField] public Chunk[] middle;
    [SerializeField] public Chunk[] bottom;

    [Header("Challenge Chunks")]
    [SerializeField] public Chunk[] easy;
    [SerializeField] public Chunk[] normal;
    [SerializeField] public Chunk[] hard;

    [Header("")]
    [SerializeField] public Chunk[] reward;

}
