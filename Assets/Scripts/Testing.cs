using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<Checker> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<Checker>(3, 3, 3f, new Vector3(0, 0, 0), (Grid<Checker> g, int x, int y) => new Checker());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
