using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapBuilder))]
public class TileMapBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileMapBuilder mapBuilder = (TileMapBuilder)target;

        if (GUILayout.Button("Build Map"))
        {
            mapBuilder.BuildMap();
        }
    }
}
