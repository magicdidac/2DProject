using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    private GameObject _player;

    [SerializeField]
    private float _offset;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x+_offset, 1.3f, -10);
    }
}
