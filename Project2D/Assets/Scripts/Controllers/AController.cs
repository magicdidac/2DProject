using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AController : MonoBehaviour
{
    [HideInInspector] protected GameController gc;

    private void OnEnable()
    {
        GetGameController();
    }

    private void GetGameController()
    {
        try
        {
            gc = GameController.instance;
            gc.AddController(this);
        }
        catch (System.Exception)
        {
            Invoke("GetGameController", .1f);
        }
    }
}
