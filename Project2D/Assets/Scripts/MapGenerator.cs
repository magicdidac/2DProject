using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _chunks;

    private int _currentChunk = -1;
    private int _oldChunk = -1;

    private int _lasPos = 0;

    private void Start()
    {
        //changeChunk();
    }

    private void Update()
    {
        if (transform.position.x > _lasPos)
            changeChunk();
    }

    private void changeChunk()
    {
        int v_rngChunk = -1;
        do
        {
            v_rngChunk = Random.Range(0, _chunks.Length);
        } while (v_rngChunk == _currentChunk || v_rngChunk == _oldChunk);
        _oldChunk = _currentChunk;
        _lasPos += 18;
        _chunks[v_rngChunk].transform.position = new Vector3(_lasPos, 0, 0);
        _currentChunk = v_rngChunk;
    }

}
