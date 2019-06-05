using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoinStructure))]
public class CoinEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        CoinStructure myStructure = (CoinStructure)target;

        EditorGUILayout.LabelField("Coins Count: ", myStructure.GetCoinsCounter()+"");

        if (GUILayout.Button("Generate"))
        {
            myStructure.GenerateStructure();
        }



    }
}
