using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TilleMapEditor))]
public class TileMapCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilleMapEditor editor = (TilleMapEditor)target;



        GUILayout.BeginArea(new Rect(0, 0, 100, 100));
        if (GUILayout.Button("test"))
        {

        }
        GUILayout.EndArea();
    }
}
