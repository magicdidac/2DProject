using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject _chunkStorage;

    private GameController gc;

    [SerializeField]
    private Chunk[] _chunks;

    [SerializeField]
    private Chunk[] _topTransitions;

    [SerializeField]
    private Chunk[] _midTransitions;

    [SerializeField]
    private Chunk[] _botTransitions;

    [SerializeField]
    private int _perIncrease;

    private int _nextChunk = -1;
    private Chunk _lastChunk;
    private int _currentChunk = -1;
    private int _oldChunk = -1;
    
    private float _lastPos = 2f;
    
    [SerializeField]
    private int transitionCounter = 0;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //transitionCounter = Random.Range(10, 25);
    }

    private void Update()
    {
        if (transform.position.x > _lastPos)
            changeChunk();
        
    }

    private void changeChunk()
    {

        if(Random.Range(0,100) < transitionCounter)
        {
            Debug.Log("change!");

            switch (gc.player.floor)
            {
                case 1: //Top Transitions
                    _nextChunk = Random.Range(0, _topTransitions.Length);
                    _oldChunk = _currentChunk;

                    if (_currentChunk != -1)
                        _lastPos += _lastChunk.lenght / 2;
                    else
                        _lastPos += _topTransitions[_nextChunk].lenght / 2;

                    _lastPos += _topTransitions[_nextChunk].lenght / 2;
                    _lastChunk = _topTransitions[_nextChunk];
                    Instantiate(_topTransitions[_nextChunk].prefab, new Vector3(_lastPos, 8, 0), Quaternion.identity, _chunkStorage.transform);
                    break;
                case 0: //Mid Transitions
                    _nextChunk = Random.Range(0, _midTransitions.Length);
                    _oldChunk = _currentChunk;

                    if (_currentChunk != -1)
                        _lastPos += _lastChunk.lenght / 2;
                    else
                        _lastPos += _midTransitions[_nextChunk].lenght / 2;

                    _lastPos += _midTransitions[_nextChunk].lenght / 2;
                    _lastChunk = _midTransitions[_nextChunk];
                    Instantiate(_midTransitions[_nextChunk].prefab, new Vector3(_lastPos, 0, 0), Quaternion.identity, _chunkStorage.transform);
                    break;
                case -1: //Bot Transitions
                    _nextChunk = Random.Range(0, _botTransitions.Length);
                    _oldChunk = _currentChunk;

                    if (_currentChunk != -1)
                        _lastPos += _lastChunk.lenght / 2;
                    else
                        _lastPos += _botTransitions[_nextChunk].lenght / 2;

                    _lastPos += _botTransitions[_nextChunk].lenght / 2;
                    _lastChunk = _botTransitions[_nextChunk];
                    Instantiate(_botTransitions[_nextChunk].prefab, new Vector3(_lastPos, -8, 0), Quaternion.identity, _chunkStorage.transform);
                    break;
            }
            transitionCounter = 0;
            return;
        }

        do
        {
            _nextChunk = Random.Range(0, _chunks.Length);
        } while (_nextChunk == _currentChunk || _nextChunk == _oldChunk);
        _oldChunk = _currentChunk;

        if(_currentChunk != -1)
        	_lastPos += _lastChunk.lenght/2;
    	else
        	_lastPos += _chunks[_nextChunk].lenght/2;

        _lastPos += _chunks[_nextChunk].lenght/2;
        _lastChunk = _chunks[_nextChunk];
        switch (gc.player.floor)
        {
            case 1: //High Floor
                Instantiate(_chunks[_nextChunk].prefab, new Vector3(_lastPos, 8, 0), Quaternion.identity, _chunkStorage.transform);
                break;
            case 0: //Middle Floor
                Instantiate(_chunks[_nextChunk].prefab, new Vector3(_lastPos, 0, 0), Quaternion.identity, _chunkStorage.transform);
                break;
            case -1: //Low Floor
                Instantiate(_chunks[_nextChunk].prefab, new Vector3(_lastPos, -8, 0), Quaternion.identity, _chunkStorage.transform);
                break;
        }

        transitionCounter += _perIncrease;
        _currentChunk = _nextChunk;
    }

}
