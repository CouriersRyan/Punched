using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Maze _maze;

    private Vector3Int _gridPos;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _maze = FindObjectOfType<Maze>();
        if (_maze.grid != null)
        {
            _gridPos = new Vector3Int(4, 0);
            transform.position = _maze.grid.GetCellCenterWorld(_gridPos);
            _maze.ChangeTile(_gridPos, TileTypes.Filled);
            _spriteRenderer.color = Color.black;
        }
    }

    void OnMoveHorizontal(InputValue value)
    {
        //Debug.Log("Moving");
        int hMove = (int)value.Get<float>();
        if(hMove != 0) Move(new Vector3Int(_gridPos.x + hMove, _gridPos.y, _gridPos.z));
    }
    
    void OnMoveVertical(InputValue value)
    {
        //Debug.Log("Moving");
        int vMove = (int)value.Get<float>();
        if(vMove != 0)Move(new Vector3Int(_gridPos.x, _gridPos.y + vMove, _gridPos.z));
    }

    private void Move(Vector3Int newPos)
    {
        var tile = _maze.GetTile(newPos);
        switch (tile)
        {
            case TileTypes.None:
            case TileTypes.Wall:
                break;
            case TileTypes.Filled:
                break;
            
            case TileTypes.Unfilled:
                _maze.ChangeTile(newPos, TileTypes.Filled);
                _spriteRenderer.color = Color.black;
                _gridPos = newPos;
                transform.position = _maze.grid.GetCellCenterWorld(_gridPos);
                var mazeObject = _maze.GetMazeObject(newPos.x, newPos.y);
                if (mazeObject != null)
                {
                    var result = mazeObject.Interact();
                    switch (result)
                    {
                        case -1:
                        case 0:
                            Reset();
                            break;
                    }
                }
                break;
        }
    }

    public void Reset()
    {
        _maze.Reset();
        _gridPos = new Vector3Int(4, 0);
        transform.position = _maze.grid.GetCellCenterWorld(_gridPos);
        _maze.ChangeTile(_gridPos, TileTypes.Filled);
        _spriteRenderer.color = Color.black;
        GameManager.Instance.ResetSentence();
    }

    private void OnRestart(InputValue value)
    {
        Reset();
    }
}
