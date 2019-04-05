using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayerModel playerModel;
    private bool onTop;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTop = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTop = false;
        playerModel = other.gameObject.GetComponent<PlayerModel>();
    }

    void Jump()
    {
        
    }
}
