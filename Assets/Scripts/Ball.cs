using UnityEngine;

public class Ball : MonoBehaviour
{
    private float m_cost;

    // Set ball cost
    public void SetCost(float cost)
    {
        m_cost = cost;
    }

    // Get ball cost
    public float GetCost()
    {
        return m_cost;
    }
}
