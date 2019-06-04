using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: AController
{
    

    [HideInInspector] private float _offset = 4;

    [HideInInspector] private int myFloor = 0;


    void FixedUpdate()
    {
        if (!gc.IsGameRunning() || gc.player.isDead)
            return;

        if (gc.player.transform.position.x < transform.position.x - _offset - .1f)
            return;

        if(gc.GetFloor() != myFloor && gc.player.isGrounded && !gc.player.isTrampoline)
            myFloor = gc.GetFloor();
        else if (gc.GetFloor() != myFloor)
        {
            if(gc.GetFloor() - myFloor > 0)//Subo
            {
                if(gc.player.transform.position.y < myFloor*8)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (8 * myFloor) + 2, transform.position.z), .5f);
                else
                    transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (gc.player.transform.position.y) + 2, transform.position.z), .5f);
            }
            else //Bajo
            {
                if (gc.player.transform.position.y > myFloor*8)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (8 * myFloor) + 2, transform.position.z), .5f);
                else
                    transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (gc.player.transform.position.y) + 2, transform.position.z), .5f);
            }
        }
            
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (8 * gc.GetFloor())+2, transform.position.z), .5f);
    }

    private void Update()
    {
        //indicator.SetActive(myFloor != 0);
    }

}
