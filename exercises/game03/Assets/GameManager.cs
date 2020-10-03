using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject cellPrefab;

    public CellScript[,] grid;
	
	int gridWidth = 100;
	int gridHeight = 100;

    float cellDimension = 0.8f;
    float padding = 0.3f;

    int time = 0;
    float timer = 0;
    float timerRate = 0.5f;

	bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        grid = new CellScript[gridWidth, gridHeight];
        for(int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Vector3 pos = new Vector3(x * cellDimension + padding, 0, y * cellDimension + padding);
                GameObject cellObj = Instantiate(cellPrefab, pos, Quaternion.identity);
                cellObj.transform.localScale = new Vector3(cellDimension, cellDimension, cellDimension);
                CellScript cs = cellObj.GetComponent<CellScript>();
                grid[x,y] = cs;
            }
        }
        
    }

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0 && running)
		{
			generateNextState();

			timer = timerRate;
		}
	}

	void generateNextState()
	{
		time++;

		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				List<CellScript> liveNeighbors = gatherLiveNeighbors(x, y);
				//Apply the 4 rules from Conway's Gaem of Life (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
				//1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
				if (grid[x, y].Alive && liveNeighbors.Count < 2)
				{
					grid[x, y].nextAlive = false;
				}
				//2. Any live cell with two or three live neighbours lives on to the next generation.
				else if (grid[x, y].Alive && (liveNeighbors.Count == 2 || liveNeighbors.Count == 3))
				{
					grid[x, y].nextAlive = true;
				}
				//3. Any live cell with more than three live neighbours dies, as if by overpopulation.
				else if (grid[x, y].Alive && liveNeighbors.Count > 3)
				{
					grid[x, y].nextAlive = false;
				}
				//4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
				else if (!grid[x, y].Alive && liveNeighbors.Count == 3)
				{
					grid[x, y].nextAlive = true;
				}
			}
		}

		//Now that we have looped through aonll of the cells in the grid, and stored what their alive status should
		//	be in each cell's nextAlivevariable, update each cell's alive state to be that value.
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				grid[x, y].Alive = grid[x, y].nextAlive;
			}
		}
	}

	List<CellScript> gatherLiveNeighbors(int x, int y)
	{
		List<CellScript> liveNeighbors = new List<CellScript>();
		//Spend some time thinking about how this considers all surrounding cells in grid[x,y]
		//why now indexing bad values of grid.
		for (int i = Mathf.Max(0, x - 1); i <= Mathf.Min(gridWidth - 1, x + 1); i++)
		{
			for (int j = Mathf.Max(0, y - 1); j <= Mathf.Min(gridHeight - 1, y + 1); j++)
			{
				//Add all live neighbors of (x, y) excluding itself
				if (!(x == i && y == j))
				{
					if (grid[i, j].Alive)
					{
						liveNeighbors.Add(grid[i, j]);
					}
				}
			}
		}

		return liveNeighbors;
	}

	public void SimulateRunning(bool checkValue)
	{
		running = !running;
	}

}
