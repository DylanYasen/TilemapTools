using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridSystem
{
    private int gridWidth = 9;
    private int gridHeight = 10;

    public GridCell[,] grid { get; private set; }

    public float cellSize { get; private set; }

    public float originX, originY;

    public GridSystem(int width, int height, float cellSize)
    {
        this.gridWidth = width;
        this.gridHeight = height;
        this.cellSize = cellSize;

        InitGrid();
    }

    private void InitGrid()
    {
        originX = -gridWidth / 2 * cellSize + cellSize / 2;
        originY = -gridHeight / 2 * cellSize + cellSize / 2;

        grid = new GridCell[gridWidth, gridHeight];
        Vector2 pos = Vector2.zero;
        for (int w = 0; w < gridWidth; w++)
        {
            for (int h = 0; h < gridHeight; h++)
            {
                pos.Set(originX + w * cellSize + cellSize / 2, originY + h * cellSize + cellSize / 2);
                grid[w, h] = new GridCell(pos, cellSize, w, h);
            }
        }
    }


    

    public GridCell GetCellFromPos(Vector2 pos)
    {
        pos.x -= originX;
        pos.y -= originY;

        int xIndex = Mathf.FloorToInt(pos.x / cellSize);
        int yIndex = Mathf.FloorToInt(pos.y / cellSize);

        //Debug.Log(xIndex + " ++ " + yIndex);

        return grid[xIndex, yIndex];
    }

    public GridCell GetCellFromIndex(int x, int y)
    {
        if (x < gridWidth && x >= 0 && y >= 0 && y < gridHeight)
        {
            return grid[x, y];
        }

        return null;
    }
}

public class GridCell
{
    public float size;
    public Vector2 position;
    public int xIndex, yIndex;

    public Color color = Color.green;

    public GridCell(Vector2 pos, float size, int xIndex, int yIndex)
    {
        this.size = size;
        this.position = pos;
        this.xIndex = xIndex;
        this.yIndex = yIndex;
    }
}
