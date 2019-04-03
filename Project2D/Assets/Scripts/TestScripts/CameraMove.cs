using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _offset;
    
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x+_offset, 0, -10);
    }
}
