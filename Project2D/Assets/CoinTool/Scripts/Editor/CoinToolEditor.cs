using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoinTool))]
public class CoinToolEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        CoinTool myTool = (CoinTool)target;

        if (GUILayout.Button("Generate!"))
            myTool.Generate();



    }

}
