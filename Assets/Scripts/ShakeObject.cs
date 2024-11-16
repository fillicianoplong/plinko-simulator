using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField] private float m_distance;
    [SerializeField] private float m_time;
    private Vector2 m_startPos;
    private Vector2 m_endPos;

    // Start is called before the first frame update
    void Start()
    {
        m_startPos = new Vector2(transform.position.x, transform.position.y);
        m_endPos = new Vector2(transform.position.x, transform.position.y + m_distance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Animate block on impact
        StartCoroutine(MovingBlock(m_time, m_startPos, m_endPos));
    }

    IEnumerator MovingBlock(float time, Vector2 startPos, Vector2 endPos)
    {
        // Reset time
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(endPos, startPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (transform.position.y != startPos.y)
        {
            transform.position = startPos;
        }
    }
}
