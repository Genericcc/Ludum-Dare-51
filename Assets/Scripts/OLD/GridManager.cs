using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] GameObject tilePrefab;
    [SerializeField] int gridSize;
    [SerializeField] int unityGridSize = 10;

    private List<Tile> tileList = new List<Tile>();

    void Awake() 
    {
        if(Instance != null) 
        {
            Debug.LogError("There's more than one GridManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        CreateGrid();
    }

    void CreateGrid() 
    {
        for(int x = 0; x < gridSize; x++) 
        {
            for(int y = 0; y < gridSize; y++) 
            {
                GridPosition coordinates = new GridPosition(x,y);
                GameObject gameObjectTile = Instantiate(tilePrefab, GetPositionFromCoordinates(coordinates), Quaternion.identity);
                Tile newTile = gameObjectTile.GetComponentInChildren<Tile>();
                tileList.Add(newTile);
            }
        }
    }

    public GridPosition GetCoordinatesFromPosition(Vector3 position) 
    {
        GridPosition coordinates = new GridPosition();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(GridPosition coordinates) 
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    public void CheckForTens()
    {
        foreach(Tile tile in tileList)
        {
            if(tile.IsScoreATen())
            {
                tile.AddOneToScore();
            }
        }
    }

    public Tile GetTile(GridPosition gridPosition)
    {
        foreach(Tile tile in tileList)
        {
            if(tile.GetGridPosition() == gridPosition)
            {
                return tile;
            }
        }

        Debug.Log("Fakap");
        return null;
    }


}
