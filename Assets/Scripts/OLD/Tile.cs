using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private List<Tile> neighbourList = new List<Tile>();

    private TextMeshPro positionLabel;
    private GridPosition gridPosition;
    private int currentScore;

    private void Awake() 
    {
        positionLabel = GetComponentInChildren<TextMeshPro>();
    }

    private void Start() 
    {
        neighbourList = GetNeighbourList();

        gridPosition = GridManager.Instance.GetCoordinatesFromPosition(transform.position);
        currentScore = gridPosition.x * gridPosition.y;

        DisplayGridPosition();
    }

    private List<Tile> GetNeighbourList()
    {
        List<Tile> neighbours = new List<Tile>();

        //West
        if(gridPosition.x - 1 >= 0)
        {
            neighbours.Add(GridManager.Instance.GetTile(new GridPosition(gridPosition.x - 1, gridPosition.y)));
        }

        //East
        if(gridPosition.x + 1 <= 10)
        {
            neighbours.Add(GridManager.Instance.GetTile(new GridPosition(gridPosition.x + 1, gridPosition.y)));
        }

        //South
        if(gridPosition.y - 1 >= 0)
        {
            neighbours.Add(GridManager.Instance.GetTile(new GridPosition(gridPosition.x, gridPosition.y - 1)));
        }

        //North
        if(gridPosition.y + 1 <= 10)
        {
            neighbours.Add(GridManager.Instance.GetTile(new GridPosition(gridPosition.x, gridPosition.y + 1)));
        }

        return neighbours;
    }

    private void DisplayGridPosition()
    {
        positionLabel.text = currentScore.ToString();
    }

    public bool IsScoreATen()
    {
        if(currentScore % 10 == 0)
        {
            return true;
        } 
        else
        {
            return false;
        }       
    }

    public void AddOneToScore()
    {
        if(currentScore >= 10)
        {
            foreach(Tile tile in GetNeighbourList())
            {
                tile.AddOneToScore();
            }
        }

        currentScore++;

    }

    public int GetScore() => currentScore;

    public GridPosition GetGridPosition() => gridPosition;



    // public void Show()
    // {
    //     positionLabel.gameObject.SetActive(true);
    // }

    // public void Hide()
    // {
    //     positionLabel.gameObject.SetActive(false);
    // }
}
