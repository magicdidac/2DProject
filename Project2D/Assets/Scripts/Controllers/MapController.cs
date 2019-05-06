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
    private float offset = 0;
    private float transitionProbability = .0f;
    private Queue<GameObject> chunksQueue = new Queue<GameObject>();

    [SerializeField] private float _perIncrease = .05f;

    [SerializeField] private ChunkStore _chunkStore = null;

    [HideInInspector] public bool enemyNeedShoot = false;

    private void Start()
    {
        gc = GameController.instance;
        offset = Mathf.Abs(gc.player.transform.position.x) / 2;
    }

    private void Update()
    {

        if (gc.player.transform.position.x > _lastPos - offset)
            changeChunk();
        
    }

    private void changeChunk()
    {

        if (enemyNeedShoot)
        {
            _nextChunk = -1;
            instantiateChunk(_chunkStore.enemyChunk);
            gc.enemy.shootPosition = _lastPos;
            gc.enemy.canCharge = true;
            enemyNeedShoot = false;
            return;
        }

        if (Random.value < transitionProbability)
        {
            switch (gc.getFloor())
            {
                case 1: //Top Transitions
                    _nextChunk = Random.Range(0, _chunkStore.topTChunks.Length);
                    instantiateChunk(_chunkStore.topTChunks[_nextChunk]);
                    break;
                case 0: //Mid Transitions
                    _nextChunk = Random.Range(0, _chunkStore.midTChunks.Length);
                    instantiateChunk(_chunkStore.midTChunks[_nextChunk]);
                    break;
                case -1: //Bot Transitions
                    _nextChunk = Random.Range(0, _chunkStore.botTChunks.Length);
                    instantiateChunk(_chunkStore.botTChunks[_nextChunk]);
                    break;
            }
            transitionProbability = .0f;
            _nextChunk = -1;
            return;
        }

        do
        {
            _nextChunk = Random.Range(0, _chunkStore.continueChunks.Length);
        } while (_nextChunk == _currentChunk || _nextChunk == _oldChunk);
        
        instantiateChunk(_chunkStore.continueChunks[_nextChunk]);

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

        GameObject chunkCreated = Instantiate(p_chunk.prefab, new Vector3(_lastPos, 8*gc.getFloor()), Quaternion.identity, transform);
        chunksQueue.Enqueue(chunkCreated);
        if (chunksQueue.Count >= 3)
        {
            GameObject.Destroy(chunksQueue.Dequeue());
        }

        _currentChunk = _nextChunk;
    }

}
