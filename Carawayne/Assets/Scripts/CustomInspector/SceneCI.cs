using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SceneHandler))]
public class SceneCI : Editor
{
    public override void OnInspectorGUI()
    {
        SceneHandler scene = (SceneHandler)target;

        if (GUILayout.Button("End turn"))
        {
            scene.endTurn();
        }

        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
