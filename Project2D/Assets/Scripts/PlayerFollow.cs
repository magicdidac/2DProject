using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow: MonoBehaviour
{
    
    private GameController gc;
    [HideInInspector] public GameObject indicator;
    
    private float _offset = 0;

    private int myFloor = 0;

    private void Start()
    {
        gc = GameController.instance;
        indicator = transform.GetChild(0).gameObject;
        _offset = Mathf.Abs(gc.player.transform.position.x);
    }

    void FixedUpdate()
    {
        if(gc.getFloor() != myFloor && gc.player.isGrounded && !gc.player.isTrampoline)
            myFloor = gc.getFloor();
        else if (gc.getFloor() != myFloor)
        {
            if(gc.getFloor() - myFloor > 0)//Subo
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(gc.player.transform.position.x + _offset, (8 * gc.getFloor())+2, transform.position.z), .5f);
    }

    private void Update()
    {
        indicator.SetActive(myFloor != 0);
    }

}
