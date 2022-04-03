using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basicunit : MonoBehaviour
{
    private Basicunit basicunit;

    public Material aliveMatter;
    public Material deadMatter;

    public Collider[] nearbyCells;

    public int nearbyaliveCellCount;
    public float generationTime = 1;

    public bool alive;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Check", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Check()
    {
        nearbyCells = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity);
        Debug.Log(transform.name +nearbyCells.Length);

        for (int i = 0; i < nearbyCells.Length; i += 1)
        {
            basicunit = nearbyCells[i].GetComponent<Basicunit>();
            if (basicunit.alive)
            {
                nearbyaliveCellCount += 1;
            }
        }

        if (alive)
        {
            nearbyaliveCellCount -= 1;
        }

            //If alive == true
            //Any live cell with fewer than two live neighbours dies, as if by underpopulation.(nearbyCell<2, dead = true)
            //Any live cell with two or three live neighbours lives on to the next generation.(1<nearbyCell<4, alive = true)
            //Any live cell with more than three live neighbours dies, as if by overpopulation.(nearbyCell>3, dead = true)
        if (alive)
        {
            if (nearbyaliveCellCount < 2)
            {
                dead = true;
                alive = false;
                transform.GetComponent<Renderer>().material = deadMatter;
                nearbyaliveCellCount = 0;
                
            }
            else if (nearbyaliveCellCount > 3)
            {
                dead = true;
                alive = false;
                transform.GetComponent<Renderer>().material = deadMatter;
                nearbyaliveCellCount = 0;
                
            }
        }
        //If dead == true
        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.(nearbyCell == 3, alive = true)
        else
        {
            if (nearbyaliveCellCount == 3)
            {
                alive = true;
                dead = false;
                transform.GetComponent<Renderer>().material = aliveMatter;
                nearbyaliveCellCount = 0;
                
            }
        }
    }

}
