using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public LayerMask targetlayer;

    private SpawnGrid spawnGrid;

    public GameObject spawn_grid_obj;
    public GameObject alivePrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnGrid = spawn_grid_obj.GetComponent<SpawnGrid>();

        InvokeRepeating("Click", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetlayer))
            {
                GameObject inter = Instantiate(alivePrefab, hit.transform.position, Quaternion.identity);

                spawnGrid.grid[Mathf.RoundToInt(hit.transform.position.x), Mathf.RoundToInt(hit.transform.position.z)] = true;
                spawnGrid.deleating[Mathf.RoundToInt(hit.transform.position.x), Mathf.RoundToInt(hit.transform.position.z)] = inter;

                Destroy(hit.transform.gameObject);
            }
        }
    }
}
