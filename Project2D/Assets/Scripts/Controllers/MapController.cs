using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private GameController gc;

    private Chunk _lastChunk;
    private int _oldChunk = -1;
    private int _currentChunk = -1;
    private int _nextChunk = -1;
    private float _lastPos = 0;
    private float transitionProbability = .0f;
    private Queue<GameObject> chunksQueue = new Queue<GameObject>();

    [SerializeField] private float _perIncrease = .05f;

    #region Chunks'Arrays

    [Header("Normal chunks...")]
    [Tooltip("Array of chunks that will generated but they aren't transitional chunks.")]
    [SerializeField] private Chunk[] _normalChunks = new Chunk[0];

    [Header("Top transition chunks...")]
    [Tooltip("Array of chunks that will generated to pass from top floor [1] to mid floor [0].")]
    [SerializeField] private Chunk[] _topTransitions = new Chunk[0];

    [Header("Mid transition chunks...")]
    [Tooltip("Array of chunks that will generated to pass from mid floor [0] to top floor [1] or/and bot floor [-1].")]
    [SerializeField] private Chunk[] _midTransitions = new Chunk[0];

    [Header("Bot transition chunks...")]
    [Tooltip("Array of chunks that will generated to pass from bot floor [-1] to mid floor [0].")]
    [SerializeField] private Chunk[] _botTransitions = new Chunk[0];

    #endregion

    private void Start()
    {
        gc = GameController.instance;
    }

    private void Update()
    {
        if (gc.player.transform.position.x > _lastPos)
            changeChunk();
        
    }

    private void changeChunk()
    {
        if(Random.value < transitionProbability)
        {
            switch (gc.player.floor)
            {
                case 1: //Top Transitions
                    _nextChunk = Random.Range(0, _topTransitions.Length);
                    instantiateChunk(_topTransitions[_nextChunk]);
                    break;
                case 0: //Mid Transitions
                    _nextChunk = Random.Range(0, _midTransitions.Length);
                    instantiateChunk(_midTransitions[_nextChunk]);
                    break;
                case -1: //Bot Transitions
                    _nextChunk = Random.Range(0, _botTransitions.Length);
                    instantiateChunk(_botTransitions[_nextChunk]);
                    break;
            }
            transitionProbability = .0f;
            return;
        }

        do
        {
            _nextChunk = Random.Range(0, _normalChunks.Length);
        } while (_nextChunk == _currentChunk || _nextChunk == _oldChunk);
        
        instantiateChunk(_normalChunks[_nextChunk]);

        transitionProbability += _perIncrease;
    }
    
    private void instantiateChunk(Chunk p_chunk)
    {
        _oldChunk = _currentChunk;
        if (_currentChunk != -1)
            _lastPos += _lastChunk.lenght / 2;
        else
            _lastPos += 9;

        _lastPos += p_chunk.lenght / 2;
        _lastChunk = p_chunk;

        GameObject chunkCreated = Instantiate(p_chunk.prefab, new Vector3(_lastPos, 8*gc.player.floor), Quaternion.identity, transform);
        chunksQueue.Enqueue(chunkCreated);
        if(chunksQueue.Count >= 3)
            GameObject.Destroy(chunksQueue.Dequeue());

        _currentChunk = _nextChunk;
    }

}
