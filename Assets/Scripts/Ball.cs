using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float cost;
    private float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCost(float cost)
    {
        this.cost = cost;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public float GetCost()
    {
        return cost;
    }

    public float GetValue()
    {
        return value;
    }
}
