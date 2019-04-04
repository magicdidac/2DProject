using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow: MonoBehaviour
{
    
    private PlayerController _player;

    [SerializeField]
    private float _offset;

    private Animator anim;

    private int myFloor = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        anim = Camera.main.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
        if(_player.floor != myFloor && _player.isGrounded)
            myFloor = _player.floor;
        else if (_player.floor != myFloor)
            transform.position = Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x + _offset, _player.transform.position.y, transform.position.z), .5f);
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x + _offset, 8 * _player.floor, transform.position.z), .5f);
    }
}
