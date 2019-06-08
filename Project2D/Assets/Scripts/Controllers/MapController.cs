using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : AController
{
    /*
     LAWS:
        - All variables will be private if you need access to it, plese do it by Getter and/or Setter
        - All the functions should be used somewhere, except controllers (only on GameController class)
        - Just put the regions that the class will use
        - All variables, regardless of whether they are public or private, you should put [HideInInspector] or [SerializeField]
     */

    #region Variables

    [SerializeField] private ChunkStore store = null;
    [Range(0, .9f)]
    [SerializeField] private float increaseAmmountProbability = .1f;
    [SerializeField] private GameObject background = null;

    [HideInInspector] private int challengeCounter = 3;
    [HideInInspector] private int chunksCounter = 0;
    [HideInInspector] private float transitionChunkProbability = 0;
    [HideInInspector] private float xOffset = 0;
    [HideInInspector] private float nextSpawnPosition = 9;
    [HideInInspector] private float lastChunkLenght = 0;
    [HideInInspector] private List<Chunk> chunksSpawned = null;
    [HideInInspector] private Queue<GameObject> chunksQueue = null;
    [HideInInspector] private Queue<GameObject> backgroundQueue = null;

    [Header("Rocks")]
    [SerializeField] private GameObject rocks = null;

    #endregion


    #region Initializers

    //Start
    private void Start()
    {
        chunksSpawned = new List<Chunk>();
        chunksQueue = new Queue<GameObject>();
        backgroundQueue = new Queue<GameObject>();

        NextChunk();
        //NewBackground();
    }

    public void StartGame()
    {
        rocks.SetActive(true);
    }


    #endregion


    #region Getters

    public int GetChunksCounter() { return chunksCounter; }

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
        return GetChunkByArrayWithoutRepeating(store.easy);
    }

    private Chunk GetNormalChunk()
    {
        return GetChunkByArrayWithoutRepeating(store.normal);
    }

    private Chunk GetHardChunk()
    {
        return GetChunkByArrayWithoutRepeating(store.hard);
    }

    private Chunk GetChunkByArrayWithoutRepeating(Chunk[] array)
    {
        List<Chunk> allChunks = new List<Chunk>(array);
        Chunk chunk;
        do
        {

            if (allChunks.Count == 0)
            {
                chunksSpawned.Clear();
                allChunks = new List<Chunk>(array);
            }

            chunk = allChunks[Random.Range(0, allChunks.Count)];

            if (chunksSpawned.Contains(chunk))
                allChunks.Remove(chunk);

        } while (chunksSpawned.Contains(chunk));

        chunksSpawned.Add(chunk);
        return chunk;
    }

    private Chunk GetChunkByArray(Chunk[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private Chunk GetTransitionChunk()
    {
        switch (gc.GetFloor())
        {
            case 1:
                return GetChunkByArray(store.top);
            case 0:
                return GetChunkByArray(store.middle);
            case -1:
                return GetChunkByArray(store.bottom);
        }

        throw new System.Exception("Floor isn't valid");
    }

    private Chunk GetRewardChunk()
    {
        return GetChunkByArray(store.reward);
    }

    #endregion
    

    //Update
    private void Update()
    {
        if (!gc.IsGameRunning() || gc.player.isDead)
            return;

        /*if (gc.player.transform.position.x >= xOffset - 19.2f)
            NewBackground();*/

        if (gc.player.transform.position.x > nextSpawnPosition)
            NextChunk();

    }


    #region Other

    private void NewBackground()
    {
        xOffset += 19.2f;
        backgroundQueue.Enqueue(Instantiate(background, new Vector3(xOffset, 0), Quaternion.identity));
        if (backgroundQueue.Count > 3)
            GameObject.Destroy(backgroundQueue.Dequeue());
    }

    private void NextChunk()
    {
        Chunk newChunk = null;


        if (challengeCounter > 0)
        {
            if (Random.value < transitionChunkProbability)
            {
                newChunk = GetTransitionChunk();
                transitionChunkProbability = 0;
            }
            else
            {
                newChunk = GetChallengeChunk();
                challengeCounter--;
                transitionChunkProbability += increaseAmmountProbability;
                chunksCounter++;
                gc.IncreasePlayerSkill();
            }
        }
        else
        {
            newChunk = GetRewardChunk();

            gc.IncreaseVelocityMultiplier();

            challengeCounter = Random.Range(3, 6);
        }

        if (chunksQueue.Count > 1)
            Destroy(chunksQueue.Dequeue());

        GameObject chunkObject = SpawnChunk(newChunk);
        lastChunkLenght = newChunk.lenght;
        nextSpawnPosition = chunkObject.transform.position.x;
        chunksQueue.Enqueue(chunkObject);
    }



    private GameObject SpawnChunk(Chunk c)
    {
        return Instantiate(c.prefab, new Vector3(nextSpawnPosition + (lastChunkLenght / 2) + (c.lenght / 2), gc.GetFloor() * 8, 0), Quaternion.identity);
    }

    #endregion


}
