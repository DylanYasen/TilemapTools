using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class TileMapBuilder : MonoBehaviour
{
    public enum MapTheme
    {
        GlowingMushroom,
        UnderWater,
        Forest
    }

    public string LevelName;
    public MapTheme mapTheme;
    public string m_mapData { get; private set; }

    public void BuildMap()
    {
        m_mapData = "";

        int index = 0;

        string str = "{\"MapName\":\"test\",\"MapTheme\":\"test\",\"Tiles\":[,{\"TileName\":\"value\",\"TileXPosition\":\"value\",\"TileYPosition\":\"value\",\"TileSortingLayer\":\"value\",\"TileSortingOrder\":\"value\",\"TileRotation\":\"value\"}]}";

        var TileData = JSONNode.Parse(str);

        TileData["MapName"] = LevelName;
        TileData["MapTheme"] = mapTheme.ToString();

        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.name == gameObject.name)
                continue;

            Transform trans = obj.transform;
            Vector2 pos = trans.position;
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

            TileData["Tiles"][index]["TileName"] = obj.name;
            TileData["Tiles"][index]["TileXPosition"] = (pos.x).ToString();
            TileData["Tiles"][index]["TileYPosition"] = (pos.y).ToString();
            TileData["Tiles"][index]["TileSortingLayer"] = sr.sortingLayerName;
            TileData["Tiles"][index]["TileSortingOrder"] = sr.sortingOrder.ToString();
            TileData["Tiles"][index]["TileRotation"] = trans.localEulerAngles.z.ToString();
            index++;
        }

        Parse(TileData.ToString(""));
        Debug.Log(m_mapData);

        SaveMapData(TileData);
    }

    void Parse(string text)
    {
        m_mapData += text + "\n";
    }


    void SaveMapData(JSONNode data)
    {

#if !UNITY_WEBPLAYER
        if (File.Exists("Assets/Resources/Maps/" + LevelName + ".inheritanceMap"))
        {
            Debug.Log(LevelName + " already exist.");
            return;
        }

        // encode later
        var file = File.CreateText("Assets/Resources/Maps/" + LevelName + ".inheritanceMap");
        file.Write(data.SaveToBase64());
        file.Close();
#endif
    }

}
