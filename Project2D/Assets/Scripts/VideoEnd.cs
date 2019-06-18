using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoEnd : MonoBehaviour
{
    public void DeleteVideo()
    {
        GameController.instance.uiController.killVideo();
    }
}
