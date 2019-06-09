using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGranade : Granade
{

    #region Initializers

    //OnEnable
    private void OnEnable()
    {
        rb2d.AddForce(-Vector2.one, ForceMode2D.Impulse);
    }

    #endregion

}
