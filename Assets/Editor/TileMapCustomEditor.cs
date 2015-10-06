using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilleMapEditor))]
public class TileMapCustomEditor : Editor
{
    private TilleMapEditor editor;
    private int selectedTileIndex;

    void OnEnable()
    {
        editor = (TilleMapEditor)target;
        selectedTileIndex = -1;
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        if (GUILayout.Button("Refresh Tile Prefabs"))
        {
            editor.LoadTilePrefabs();
        }

        // tile selection platte
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Tiles");
        selectedTileIndex = GUILayout.SelectionGrid(selectedTileIndex, editor.tileTextures, 5);
        GUILayout.EndVertical();

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;

        // change tile layer
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("-"))
            editor.layer--;
        GUILayout.Label("Layer: " + editor.layer, style);
        if (GUILayout.Button("+"))
            editor.layer++;

        GUILayout.EndHorizontal();


        if (GUI.changed)
            Repaint();
    }

    void OnSceneGUI()
    {
        int controlled = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = ray.origin;

        // left mouse button to create tile
        if (e.isMouse && e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag))
        {
            // keep in side of grid canvas
            if (mousePos.x < 0 || mousePos.y < 0)
                return;

            if (Mathf.Floor(mousePos.x / editor.TileSize) >= editor.width - 1 || Mathf.Floor(mousePos.y / editor.TileSize) >= editor.height - 1)
                return;

            // register control
            GUIUtility.hotControl = controlled;
            e.Use();

            GameObject tileObj;
            GameObject prefab = editor.tiles[selectedTileIndex];

            if (prefab != null)
            {
                Undo.IncrementCurrentGroup();

                Vector2 allignPos = new Vector2(Mathf.Floor(mousePos.x / editor.TileSize) * editor.TileSize + editor.TileSize / 2, Mathf.Floor(mousePos.y / editor.TileSize) * editor.TileSize + editor.TileSize / 2);

                // create tile
                tileObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                tileObj.GetComponent<SpriteRenderer>().sortingOrder = editor.layer;

                // there is a tile already
                GameObject obj = GetObjFromPosition(allignPos);
                if (obj != null)
                {
                    // if same layer
                    if (obj.GetComponent<SpriteRenderer>().sortingOrder == tileObj.GetComponent<SpriteRenderer>().sortingOrder)
                        DestroyImmediate(obj);
                }

                tileObj.transform.position = allignPos;

                // regsiter ctrl + z
                Undo.RegisterCreatedObjectUndo(tileObj, "Create" + tileObj.name);
            }
        }

        // right mouse button to delete
        else if (e.isMouse && e.button == 1 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag))
        {
            GUIUtility.hotControl = controlled;
            e.Use();

            Vector2 pos = new Vector2(Mathf.Floor(mousePos.x / editor.TileSize) * editor.TileSize + editor.TileSize / 2, Mathf.Floor(mousePos.y / editor.TileSize) * editor.TileSize + editor.TileSize / 2);

            GameObject obj = GetObjFromPosition(pos);
            if (obj != null)
                DestroyImmediate(obj);

        }
    }

    GameObject GetObjFromPosition(Vector2 pos)
    {
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.name == "TileMapTool")
                continue;

            if (obj.transform.position == (Vector3)pos)
                return obj;
        }

        return null;
    }
}
