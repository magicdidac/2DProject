using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject _chunkStorage;

    [SerializeField]
    private Chunk[] _chunks;

    private int _nextChunk = -1;
    private int _currentChunk = -1;
    private int _oldChunk = -1;

    private float _lastPos = 2f;

    private void Update()
    {
        if (transform.position.x > _lastPos)
            changeChunk();

        checkChunkOutOfBounds();
    }

    private void changeChunk()
    {
        do
        {
            _nextChunk = Random.Range(0, _chunks.Length);
        } while (_nextChunk == _currentChunk || _nextChunk == _oldChunk);
        _oldChunk = _currentChunk;

        if(_currentChunk != -1)
        	_lastPos += _chunks[_currentChunk].lenght/2;
    	else
        	_lastPos += _chunks[_nextChunk].lenght/2;

        _lastPos += _chunks[_nextChunk].lenght/2;
        Instantiate(_chunks[_nextChunk].prefab, new Vector3(_lastPos, 0, 0), Quaternion.identity, _chunkStorage.transform);
        _currentChunk = _nextChunk;
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
