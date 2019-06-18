using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Box))]
public class BoxEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Box myBox = (Box)target;

        if (GUILayout.Button("Break down!"))
            Destroy(myBox.gameObject);



    }

}
