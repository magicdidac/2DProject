using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateStart : MonoBehaviour
{
    void Start()
    {
        GameController.instance.restartVariables();
        Destroy(gameObject);
    }
}
