using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SlidingPuzzleManager : MonoBehaviour
{
    public int gridSize = 3; // 3x3 grid
    public Vector2 tileSize = new Vector2(200, 200); // Size of each tile
    public RectTransform emptyTile; // The empty tile RectTransform
    private Vector2Int emptyTilePosition; // Position of the empty tile in grid

    public SlidingTile[,] grid;

    private bool puzzleSolved = false; // Flag to disable interaction when solved
    public GameObject activePuzzleUI;
    public Puzzle puzzle;

    void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        Debug.Log("Initialised");
        grid = new SlidingTile[gridSize, gridSize];
        emptyTilePosition = new Vector2Int(gridSize - 1, gridSize - 1); // Start with empty tile in bottom-right corner

        foreach (SlidingTile tile in GetComponentsInChildren<SlidingTile>())
        {
            Vector2Int pos = tile.gridPosition;
            grid[pos.x, pos.y] = tile;
            tile.puzzleManager = this;
        }
    }


    public void TryMoveTile(SlidingTile tile)
{
    Vector2Int direction = tile.gridPosition - emptyTilePosition;

    // Only allow movement if the tile is adjacent to the empty space
    if (Mathf.Abs(direction.x) + Mathf.Abs(direction.y) == 1)
    {
        // Swap grid positions
        Vector2Int tilePosition = tile.gridPosition;

        // Swap positions in the logical grid
        grid[emptyTilePosition.x, emptyTilePosition.y] = tile;
        grid[tilePosition.x, tilePosition.y] = null;

        // Update the tile's grid position
        tile.gridPosition = emptyTilePosition;

        // Update the empty tile's grid position
        emptyTilePosition = tilePosition;

        // Update UI positions
        RectTransform tileRect = tile.GetComponent<RectTransform>();
        Vector2 emptyTileWorldPosition = emptyTile.anchoredPosition;

        tileRect.anchoredPosition = emptyTileWorldPosition;
        emptyTile.anchoredPosition = GetAnchoredPosition(emptyTilePosition);

        // Check if the puzzle is solved after the move
        CheckIfSolved();
    }
}


    public void CheckIfSolved()
{
    int expectedNumber = 1; // Start with "1" for the first tile

    for (int y = 0; y < gridSize; y++)
    {
        for (int x = 0; x < gridSize; x++)
        {
            // Check if this is the empty tile position (last position in the grid)
            if (x == gridSize - 1 && y == gridSize - 1)
            {
                if (grid[x, y] != null)
                {
                    return; // Not solved
                }
                continue; // Skip further checks for the empty tile
            }

            // Get the tile at the current position
            SlidingTile tile = grid[x, y];

            if (tile == null)
            {
                return; // Not solved
            }

            // Get the TMP_Text component
            Button tileButton = tile.GetComponent<Button>();
            TMP_Text tileText = tileButton.GetComponentInChildren<TMP_Text>();

            if (tileText == null)
            {
                return; // Not solved
            }

            if (tileText.text != expectedNumber.ToString())
            {
                return; // Not solved
            }

            expectedNumber++; // Increment to the next expected number
        }
    }

    puzzleSolved = true;
    Debug.Log("Puzzle Solved!");
    foreach (SlidingTile tile in GetComponentsInChildren<SlidingTile>())
    {
        tile.GetComponent<Button>().interactable = false;
    }
    if (activePuzzleUI)
    {
        activePuzzleUI.SetActive(false);
        activePuzzleUI = null;
    }
    PuzzleManager.Instance.CompletePuzzle(puzzle);

}

    private Vector2 GetAnchoredPosition(Vector2Int gridPos)
    {
        // Calculate anchored position based on tile size and grid position
        float x = gridPos.x * tileSize.x + 100; // Adjust starting offset
        float y = -gridPos.y * tileSize.y - 100; // Adjust starting offset
        return new Vector2(x, y);
    }
}
