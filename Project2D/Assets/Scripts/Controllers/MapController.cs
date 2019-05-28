using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    //GameController instance
    [HideInInspector] private GameController gc;
    


    //New Variables

    [SerializeField] private ChunkStore store = null;
    [Range(0,.9f)]
    [SerializeField] private float increaseAmmountProbability = .1f;
    [SerializeField] private GameObject background = null;

    [HideInInspector] private int challengeCounter = 0;
    [HideInInspector] private float transitionChunkProbability = 0;
    [HideInInspector] private float xOffset = 0;
    [HideInInspector] private float nextSpawnPosition = 0;
    [HideInInspector] private List<Chunk> chunksSpawned = null;
    [HideInInspector] private Queue<GameObject> chunksQueue = null;
    [HideInInspector] private Queue<GameObject> backgroundQueue = null;

    private void Start()
    {
        gc = GameController.instance;
        gc.mapController = this;

        chunksSpawned = new List<Chunk>();
        chunksQueue = new Queue<GameObject>();
        backgroundQueue = new Queue<GameObject>();

        NextChunk();
        NewBackground();
    }

    private void Update()
    {
        if (gc.player.transform.position.x >= xOffset-19.2f)
            NewBackground();
        
        if (gc.player.transform.position.x > nextSpawnPosition)
            NextChunk();
        
    }


    private void NewBackground()
    {
        xOffset += 19.2f;
        backgroundQueue.Enqueue(Instantiate(background,new Vector3(xOffset,0),Quaternion.identity));
        if (backgroundQueue.Count > 3)
            GameObject.Destroy(backgroundQueue.Dequeue());
    }




    private void NextChunk()
    {
        Chunk newChunk = null;


        if (challengeCounter < 3)
        {
            if (Random.value < transitionChunkProbability)
            {
                newChunk = GetTransitionChunk();
                transitionChunkProbability = 0;
            }
            else
            {
                newChunk = GetChallengeChunk();
                challengeCounter++;
                transitionChunkProbability += increaseAmmountProbability;
            }
        }
        else
        {
            newChunk = GetRewardChunk();

            //Subir el nivel del Jugador +10

            //si la velocidad no es mayor a la velocidad inicial de la run *1.75
                //sumar a la velocidad actual .1

            challengeCounter = 0;
        }

        GameObject.Destroy(chunksQueue.Dequeue());
        GameObject chunkObject = SpawnChunk(newChunk);
        nextSpawnPosition = chunkObject.transform.position.x - (newChunk.lenght / 2);
    }

    private Chunk GetChallengeChunk()
    {
        Chunk resultChunk = null;

        if (PlayerPrefs.GetInt("PlayerSkill", 30) <= 40)
            resultChunk = GetEasyChunk();
        else if (PlayerPrefs.GetInt("PlayerSkill", 30) <= 80)
            resultChunk = GetNormalChunk();
        else
            resultChunk = GetHardChunk();

        return resultChunk;
    }

    private Chunk GetEasyChunk()
    {
        List<Chunk> allChunks = new List<Chunk>(store.easy);
        Chunk chunk;
        do
        {
            chunk = allChunks[Random.Range(0, allChunks.Count)];

            if (chunksSpawned.Contains(chunk))
                allChunks.Remove(chunk);

        } while (chunksSpawned.Contains(chunk));

        chunksSpawned.Add(chunk);
        return chunk;
    }

    private Chunk GetNormalChunk()
    {
        List<Chunk> allChunks = new List<Chunk>(store.normal);
        Chunk chunk;
        do
        {
            chunk = allChunks[Random.Range(0, allChunks.Count)];

            if (chunksSpawned.Contains(chunk))
                allChunks.Remove(chunk);

        } while (chunksSpawned.Contains(chunk));

        chunksSpawned.Add(chunk);
        return chunk;
    }

    private Chunk GetHardChunk()
    {
        List<Chunk> allChunks = new List<Chunk>(store.hard);
        Chunk chunk;
        do
        {
            chunk = allChunks[Random.Range(0, allChunks.Count)];

            if (chunksSpawned.Contains(chunk))
                allChunks.Remove(chunk);

        } while (chunksSpawned.Contains(chunk));

        chunksSpawned.Add(chunk);
        return chunk;
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
