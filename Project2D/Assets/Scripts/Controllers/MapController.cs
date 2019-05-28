using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    //GameController instance
   [HideInInspector] private GameController gc;

    [HideInInspector] private Chunk _lastChunk;
    [HideInInspector] private int _oldChunk = -1;
    [HideInInspector] private int _currentChunk = -1;
    [HideInInspector] private int _nextChunk = -1;
    [HideInInspector] private float _lastPos = 0;
    [HideInInspector] private float offset = 0;
    [HideInInspector] private float transitionProbability = .0f;
    [HideInInspector] private Queue<GameObject> chunksQueue = new Queue<GameObject>();
    [HideInInspector] private Queue<GameObject> backgroundQueue = new Queue<GameObject>();

    [SerializeField] private GameObject background = null;
    [HideInInspector] private float xOffset = 0;

    [SerializeField] private float _perIncrease = .05f;

    [SerializeField] private ChunkStore _chunkStore = null;

    private void Start()
    {
        gc = GameController.instance;
        gc.mapController = this;

        offset = Mathf.Abs(gc.player.transform.position.x) / 2;
        NewBackground();
    }

    private void Update()
    {
        if (gc.player.transform.position.x >= xOffset-19.2f)
            NewBackground();

        //Si el Jugador ha sobrepasado la distancia maxima con respecto al ultimo chunk
            //NextChunk();
        
    }

    /*private void changeChunk()
    {

        if (Random.value < transitionProbability)
        {
            switch (gc.GetFloor())
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

        GameObject chunkCreated = Instantiate(p_chunk.prefab, new Vector3(_lastPos, 8*gc.GetFloor()), Quaternion.identity, transform);
        chunksQueue.Enqueue(chunkCreated);
        if (chunksQueue.Count >= 3)
        {
            GameObject.Destroy(chunksQueue.Dequeue());
        }

        _currentChunk = _nextChunk;
    }*/


    private void NewBackground()
    {
        xOffset += 19.2f;
        backgroundQueue.Enqueue(Instantiate(background,new Vector3(xOffset,0),Quaternion.identity));
        if (backgroundQueue.Count > 3)
            GameObject.Destroy(backgroundQueue.Dequeue());
    }




    private void NextChunk()
    {
        //Si el challenge counter es menor a 3
            //Si Random.value es menor que la probabilidad de transicion
                //GetTransitionChunk();
                //probabilidad = 0
            //Si no
                //GetChallengeChunk();
                //Se incrementará el counter challenge en 1

        //Si no
            //GetRewardChunk()
            //Subir el nivel del Jugador +10
            //si la velocidad no es mayor a la velocidad inicial de la run *1.75
                //sumar a la velocidad actual .1
            //el counter de chunks settearlo a 0
        
        //Eliminar ultimo chunk
        //Transform chunkTransform = SpawnChunk(c);
        //Setear la distancia de spawneo a = chunkObject.position.x+(c.lenght/2);
    }

    private Chunk GetChallengeChunk()
    {
        //Depende del nivel del Jugador se elegirá un chunk que no haya salido previamente
        //Si no queda ningun chunk que no haya salido
            //El store con los chunks anteriormente spawneados se limpiará
            //return GetChallengeChunk();
        

        //añadir chunk a la lista de elegidos
        //return chunk elegido
        return null;
    }

    private Chunk GetTransitionChunk()
    {
        //Depende del piso donde se encuentre el jugador elegir chunk de transicion

        return null;
    }

    private Chunk GetRewardChunk()
    {
        //Elegir un chunk de Reward

        return null;
    }

    private GameObject SpawnChunk(Chunk c)
    {
        //Spawnear el Chunk en la posición correcta
        //return gameObject chunk
        return null;
    }

}
