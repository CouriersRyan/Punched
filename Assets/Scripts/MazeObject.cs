using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObject : MonoBehaviour
{
    private Maze _maze;

    public Vector3Int gridPos;

    private SpriteRenderer _spriteRenderer;

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _maze = FindObjectOfType<Maze>();
        if (_maze.grid != null)
        {
            transform.position = _maze.grid.GetCellCenterWorld(gridPos);
            _maze.SetMazeObject(this, new Vector2Int(gridPos.x, gridPos.y));
        }
    }

    public virtual int Interact()
    {
        return -2;
    }

    public void Reset()
    {
        //_spriteRenderer.color = Color.white;
    }
}
