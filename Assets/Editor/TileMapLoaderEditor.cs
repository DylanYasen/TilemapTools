using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileMapLoader))]
public class TileMapLoaderEditor : Editor
{
    private bool mapLoaded;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileMapLoader mapLoader = (TileMapLoader)target;

        if (GUILayout.Button("Load Map"))
        {
            if (mapLoader.LoadMap())
            {
                mapLoaded = true;
                Debug.Log("map loaded");
            }
        }

        if (GUILayout.Button("Create Tiles"))
        {
            if (mapLoaded)
                mapLoader.CreateMap();
            else
                Debug.Log("load map first");
        }

        if(GUILayout.Button("Load & Create All Levels")){
            mapLoader.LoadAllCreate();
        }
    }
}
