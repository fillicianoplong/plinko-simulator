using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set ball cost
    public void SetCost(float cost)
    {
        this.cost = cost;
    }

    // Get ball cost
    public float GetCost()
    {
        return cost;
    }
}
