using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow: MonoBehaviour
{
    
    private PlayerController _player;

    [SerializeField]
    private float _offset;

    private Animator anim;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
        anim = Camera.main.GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x+_offset, transform.position.y, transform.position.z);
        anim.SetInteger("playerFloor", _player.floor);
    }
}
