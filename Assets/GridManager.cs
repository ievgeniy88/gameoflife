using UnityEngine;

public class GridManager : MonoBehaviour
{
    private      Cell[,]    _grid;

    public const int        Rows    = 20;
    public const int        Columns = 20;
    public       GameObject CellPrefab;

    public void Start()
    {
        CreateGrid();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateGrid();
        }
    }

    private void CreateGrid()
    {
        _grid = new Cell[Rows, Columns];
        for (var x = 0; x < Rows; x++)
        {
            for (var y = 0; y < Columns; y++)
            {
                var newCell = Instantiate(CellPrefab, new Vector2(x - Columns / 2f + 0.5f, y -  Rows / 2f + 0.5f), Quaternion.identity);
                _grid[x, y] = newCell.GetComponent<Cell>();
                _grid[x, y].SetAlive(Random.value > 0.5f);
            }
        }
    }

    private void UpdateGrid()
    {
        var newStates = new bool[Rows, Columns];

        for (var x = 0; x < Rows; x++)
        {
            for (var y = 0; y < Columns; y++)
            {
                var liveNeighbors = CountLiveNeighbors(x, y);

                if (_grid[x, y].IsAlive)
                {
                    newStates[x, y] = liveNeighbors is 2 or 3;
                }
                else
                {
                    newStates[x, y] = liveNeighbors == 3;
                }
            }
        }

        for (var x = 0; x < Rows; x++)
        {
            for (var y = 0; y < Columns; y++)
            {
                _grid[x, y].SetAlive(newStates[x, y]);
            }
        }
    }

    private int CountLiveNeighbors(int x, int y)
    {
        var count = 0;

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                var nx = x + i;
                var ny = y + j;

                if (nx is >= 0 and < Rows && ny is >= 0 and < Columns && _grid[nx, ny].IsAlive)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
