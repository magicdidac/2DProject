using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    #region Variables

    [SerializeField] private BoxGraphics[] boxes = null;
    [SerializeField] private int boxIndex = -1;

    #endregion


    #region Initializers

    private void Start()
    {
        if (boxIndex < 0)
            boxIndex = Random.Range(0, boxes.Length);

        GetComponent<SpriteRenderer>().sprite = boxes[boxIndex].box_sprite;
    }

    #endregion

    public void DestroyBox()
    {
        Instantiate(boxes[boxIndex].box_destroy_prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
