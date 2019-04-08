using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow: MonoBehaviour
{
    
    private GameController gc;
    
    private float _offset = 0;

    private int myFloor = 0;

    private void Start()
    {
        gc = GameController.instance;
        _offset = Mathf.Abs(gc.player.transform.position.x);
    }

    void FixedUpdate()
    {
        if(gc.floor != myFloor && gc.player.isGrounded)
            myFloor = gc.floor;
        else if (gc.floor != myFloor)
            transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (gc.player.transform.position.y)+2, transform.position.z), .5f);
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (8 * gc.floor)+2, transform.position.z), .5f);
    }
}
