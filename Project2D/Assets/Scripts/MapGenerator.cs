using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject _chunkStorage;

    [SerializeField]
    private GameObject[] _chunks;

    private int _currentChunk = -1;
    private int _oldChunk = -1;

    private float _lasPos = 3.5f;

    private void Update()
    {
        if (transform.position.x > _lasPos)
            changeChunk();

        checkChunkOutOfBounds();
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
        Instantiate(_chunks[v_rngChunk], new Vector3(_lasPos, 0, 0), Quaternion.identity, _chunkStorage.transform);
        _currentChunk = v_rngChunk;
    }

    private void checkChunkOutOfBounds()
    {
        try
        {
            if (transform.position.x > _chunkStorage.transform.GetChild(0).transform.position.x + 20)
                Destroy(_chunkStorage.transform.GetChild(0).gameObject);
        }
        catch { };
    }

}
