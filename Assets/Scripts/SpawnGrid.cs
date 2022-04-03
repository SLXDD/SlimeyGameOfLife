using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    public GameObject alivePrefab;
    public GameObject deadPrefab;

    public int gridSize;
    public int startTrueSum;

    [HideInInspector]
    public bool[,] grid;
    private bool[,] nextGrid;
    public GameObject[,] deleating;

    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(gridSize/2, 20, gridSize/2);
        grid = new bool[gridSize+1, gridSize+1];
        nextGrid = grid;
        deleating = new GameObject[gridSize + 1, gridSize + 1];
        //Randonly insert alive cells into the matrix
        for (int l = 0; l < startTrueSum; l += 1)
        {
            grid[Random.Range(1, gridSize + 1), Random.Range(0, gridSize + 1)] = true;
        }

        generateNewGen();
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & start == false)
        {
            InvokeRepeating("generateNewGen", 1, 0.5f);
            start = true;
            Debug.Log("started");
        }
    }


    void generateNewGen()
    {
        
        foreach (GameObject i in deleating)
        {
            Destroy(i);
        }

        for (int i = 1; i < gridSize; i += 1)//x
        {
            //Debug.Log("i for runned");
            for (int a = 1; a < gridSize; a += 1)//y
            {
                //Debug.Log("a for runned");
                //确定周围活细胞数量
                int sum = 0;

                for(int g = -1; g < 2; g += 1)
                {
                    //Debug.Log("g for runned");
                    for (int h = -1; h < 2; h += 1)
                    {
                        //Debug.Log("h for runned");
                        if (grid[i+g,a+h] == true)
                        {
                            //Debug.Log(i + g);
                            //Debug.Log(a + h);
                            //Debug.Log("if runned");
                            sum += 1;
                        }
                    }
                }

                if (grid[i, a] == true)
                {
                    //Debug.Log("grid 1");
                    //生成alive
                    GameObject inter = Instantiate(alivePrefab, new Vector3(i, 0, a), Quaternion.identity);

                    deleating[i, a] = inter;

                    sum -= 1;
                    //根据周围活细胞数量应用以下规则
                    //If alive == true
                    //Any live cell with fewer than two live neighbours dies, as if by underpopulation.(nearbyCell<2, dead = true)
                    //Any live cell with two or three live neighbours lives on to the next generation.(1<nearbyCell<4, alive = true)
                    //Any live cell with more than three live neighbours dies, as if by overpopulation.(nearbyCell>3, dead = true)
                    if (sum < 2 || sum > 3)
                    {
                        nextGrid[i, a] = false;
                        //Debug.Log("nextgrid 1");
                    }
                }
                else
                {
                    //Debug.Log("grid 1(1)");
                    //生成dead
                    GameObject inter = Instantiate(deadPrefab, new Vector3(i, 0, a), Quaternion.identity);
                    deleating[i, a] = inter;
                    //根据周围活细胞数量应用以下规则
                    //If dead == true
                    //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.(nearbyCell == 3, alive = true)
                    if (sum == 3)
                    {
                        nextGrid[i, a] = true;
                        //Debug.Log("nextgrid 2");
                    }
                }
            }
        }
        
        grid = nextGrid;
    }
}
