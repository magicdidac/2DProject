using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStructure : MonoBehaviour
{

    [SerializeField] private GameObject coin = null;
    [SerializeField] private Transform coinsStore = null;

    [SerializeField] private Texture2D map = null;

    public void GenerateStructure()
    {
        if(map == null)
            Debug.LogWarning("You need to add a map to generate the structure");

        if(coinsStore == null)
            Debug.LogWarning("You need to add a coinsStore to generate the structure");

        if(coin == null)
            Debug.LogWarning("You need to add a coin prefab to generate the structure");

        if (map == null || coinsStore == null || coin == null)
            return;

        DestroyOldCoins();

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }

        Debug.Log("Coins Structure Generated!");

    }

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            // The pixel is transparrent. Let's ignore it!
            return;
        }

        if (Color.black.Equals(pixelColor))
        {
            Vector2 position = new Vector2(transform.position.x-((map.width/2)*.5f) + x * .5f, transform.position.y-((map.height/2)*.5f) + y * .5f);
            Instantiate(coin, position, Quaternion.identity, coinsStore);
        }
    }

    private void DestroyOldCoins()
    {
        while(coinsStore.childCount != 0)
        {
            GameObject.DestroyImmediate(coinsStore.GetChild(0).gameObject);
        }
    }


    public int GetCoinsCounter()
    {
        if (coinsStore == null)
            return 0;

        return coinsStore.childCount;
    }

}
