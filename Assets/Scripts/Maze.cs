using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Maze : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public Tile wall;
    public Tile filled;
    public Tile unfilled;

    private MazeObject[,] _mazeObjects;
    
    // Start is called before the first frame update
    void Awake()
    {
        grid = GetComponent<Grid>();
        tilemap = GetComponentInChildren<Tilemap>();
        _mazeObjects = new MazeObject[9, 9];
    }

    public TileTypes GetTile(Vector3Int pos)
    {
        var sprite = tilemap.GetSprite(pos);
        if (sprite == wall.sprite)
        {
            return TileTypes.Wall;
        }

        if (sprite == unfilled.sprite)
        {
            return TileTypes.Unfilled;
        }

        if (sprite == filled.sprite)
        {
            return TileTypes.Filled;
        }

        return TileTypes.None;
    }

    public void ChangeTile(Vector3Int pos, TileTypes type)
    {
        TileBase newTile = unfilled;
        switch (type)
        {
            case TileTypes.Filled:
                newTile = filled;
                break;
            
            case TileTypes.Wall:
                newTile = wall;
                break;
        }
        tilemap.SetTile(pos, newTile);
    }

    public void SetMazeObject(MazeObject obj, Vector2Int pos)
    {
        _mazeObjects[pos.x, pos.y] = obj;
    }

    public MazeObject GetMazeObject(int x, int y)
    {
        //Debug.Log(x + ", " + y);
        MazeObject obj = _mazeObjects[x, y];
        if (_mazeObjects != null)
        {
            return obj;
        }

        return null;
    }

    public void Reset()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                var tile = GetTile(new Vector3Int(x, y, 0));
                switch (tile)
                {
                    case TileTypes.None:
                    case TileTypes.Wall:
                        break;
                    case TileTypes.Filled:
                        ChangeTile(new Vector3Int(x, y, 0), TileTypes.Unfilled);
                        var mazeObject = GetMazeObject(x, y);
                        if (mazeObject != null)
                        {
                            mazeObject.Reset();
                        }
                        break;
            
                    case TileTypes.Unfilled:
                        break;
                }
            }
        }
    }
}

public enum TileTypes
{
    Unfilled,
    Filled,
    Wall,
    None
}
