using UnityEditor;
using UnityEngine;

public class TilleMapEditor : MonoBehaviour
{
    public GameObject[] tiles;
    public Texture2D[] tileTextures;

    public int width;
    public int height;
    public int TileSize;

    public float TileWidth
    {
        get
        {
            return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    public void LoadTilePrefabs()
    {
        tiles = Resources.LoadAll<GameObject>("TilePrefabs/");

        tileTextures = new Texture2D[tiles.Length];
        // get tile textures from tile prefabs
        for (int i = 0; i < tiles.Length; i++)
            tileTextures[i] = GetTextureFromTile(i);
    }

    public Texture2D GetTextureFromTile(int index)
    {
        Sprite sprite = tiles[index].GetComponent<SpriteRenderer>().sprite;

        Texture2D spTex = sprite.texture;
        Rect spRect = sprite.textureRect;

        Color[] pixels = spTex.GetPixels((int)spRect.x, (int)spRect.y, (int)spRect.width, (int)spRect.height);

        Texture2D tex = new Texture2D((int)spRect.width, (int)spRect.height);
        tex.SetPixels(pixels);

        return tex;
    }

    void OnDrawGizmos()
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawLine(new Vector2(0, TileSize * h), new Vector2(w * TileSize, TileSize * h));
                Gizmos.DrawLine(new Vector2(w * TileSize, 0), new Vector2(w * TileSize, TileSize * h));
            }
        }
    }
}

