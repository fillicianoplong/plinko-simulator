using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Multiplier : MonoBehaviour
{
    // Game manager
    private GameManager m_gameManager;
    private UIManager m_uiManager;

    // Multiplier variables
    [SerializeField] private float m_multiplier;
    [SerializeField] private float m_payout;
    private TextMeshProUGUI m_multiplierText;

    // Movement variables
    [SerializeField] private Vector2 m_startPos;
    [SerializeField] private Vector2 m_endPos;
    [SerializeField] private float m_distance;
    [SerializeField] private float m_time;

    // Audio variables
    [SerializeField] private AudioClip m_clip;
    private AudioSource m_source;

    // Color variables
    private Color m_color;
    private int m_minHue = 0;
    private int m_maxHue = 45;

    // Possible multiplier values
    private Dictionary<string, float[]> values = new Dictionary<string, float[]>
    {
        {"8 Low", new float[] { 0.5f, 1.0f, 1.1f, 2.1f, 5.6f, 0.0f, 0.0f, 0.0f, 0.0f} },
        {"8 Medium", new float[] { 0.4f, 0.7f, 1.3f, 3.0f, 13.0f, 0.0f, 0.0f, 0.0f, 0.0f } },
        {"8 High", new float[] { 0.2f, 0.3f, 1.5f, 4.0f, 29.0f, 0.0f, 0.0f, 0.0f, 0.0f } },
        {"9 Low", new float[] { 0.7f, 1.0f, 1.6f, 2.0f, 5.6f, 0.0f, 0.0f, 0.0f, 0.0f } },
        {"9 Medium", new float[] { 0.5f, 0.9f, 1.7f, 4.0f, 18.0f, 0.0f, 0.0f, 0.0f, 0.0f } },
        {"9 High", new float[] { 0.2f, 0.6f, 2.0f, 7.0f, 43.0f, 0.0f, 0.0f, 0.0f, 0.0f } },
        {"10 Low", new float[] { 0.5f, 1.0f, 1.1f, 1.4f, 3.0f, 8.9f, 0.0f, 0.0f, 0.0f } },
        {"10 Medium", new float[] { 0.4f, 0.6f, 1.4f, 2.0f, 5.0f, 22.0f, 0.0f, 0.0f, 0.0f } },
        {"10 High", new float[] { 0.2f, 0.3f, 0.9f, 3.0f, 10.0f, 76.0f, 0.0f, 0.0f, 0.0f } },
        {"11 Low", new float[] { 0.7f, 1.0f, 1.3f, 1.9f, 3.0f, 8.4f, 0.0f, 0.0f, 0.0f } },
        {"11 Medium", new float[] { 0.5f, 0.7f, 1.8f, 3.0f, 6.0f, 24.0f, 0.0f, 0.0f, 0.0f } },
        {"11 High", new float[] { 0.2f, 0.4f, 1.4f, 5.2f, 14.0f, 120.0f, 0.0f, 0.0f, 0.0f } },
        {"12 Low", new float[] { 0.5f, 1.0f, 1.1f, 1.4f, 1.6f, 3.0f, 10.0f, 0.0f, 0.0f } },
        {"12 Medium", new float[] { 0.3f, 0.6f, 1.1f, 2.0f, 4.0f, 11.0f, 33.0f, 0.0f, 0.0f } },
        {"12 High", new float[] { 0.2f, 0.2f, 0.7f, 2.0f, 8.1f, 24.0f, 170.0f, 0.0f, 0.0f } },
        {"13 Low", new float[] { 0.7f, 0.9f, 1.2f, 1.9f, 3.0f, 4.0f, 8.1f, 0.0f, 0.0f } },
        {"13 Medium", new float[] { 0.4f, 0.7f, 1.3f, 3.0f, 6.0f, 13.0f, 43.0f, 0.0f, 0.0f } },
        {"13 High", new float[] { 0.2f, 0.2f, 1.0f, 4.0f, 11.0f, 37.0f, 260.0f, 0.0f, 0.0f } },
        {"14 Low", new float[] { 0.5f, 1.0f, 1.1f, 1.3f, 1.4f, 1.9f, 4.0f, 7.1f, 0.0f } },
        {"14 Medium", new float[] { 0.2f, 0.5f, 1.0f, 1.9f, 4.0f, 7.0f, 15.0f, 58.0f, 0.0f } },
        {"14 High", new float[] { 0.2f, 0.2f, 0.3f, 1.9f, 5.0f, 18.0f, 56.0f, 420.0f, 0.0f } },
        {"15 Low", new float[] { 0.7f, 1.0f, 1.1f, 1.5f, 2.0f, 3.0f, 8.0f, 15.0f, 0.0f } },
        {"15 Medium", new float[] { 0.3f, 0.5f, 1.3f, 3.0f, 5.0f, 11.0f, 18.0f, 88.0f, 0.0f } },
        {"15 High", new float[] { 0.2f, 0.2f, 0.5f, 3.0f, 8.0f, 27.0f, 83.0f, 620.0f, 0.0f } },
        {"16 Low", new float[] { 0.5f, 1.0f, 1.1f, 1.2f, 1.4f, 1.4f, 2.0f, 9.0f, 16.0f} },
        {"16 Medium", new float[] { 0.3f, 0.5f, 1.0f, 1.5f, 3.0f, 5.0f, 10.0f, 41.0f, 110.0f} },
        {"16 High", new float[] { 0.2f, 0.2f, 0.2f, 2.0f, 4.0f, 9.0f, 26.0f, 130.0f, 1000.0f} }
    };

    void Start()
    {
        // Initialize game manager
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        m_uiManager = GameObject.Find("Game Manager").GetComponent<UIManager>();

        // Initialize multiplier variables
        m_multiplier = 0.0f;
        m_payout = 0.0f;

        // Initialize multiplier text
        m_multiplierText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        m_multiplierText.text = m_multiplier.ToString();

        // Initialize movement variables
        m_startPos = new Vector2(transform.position.x, transform.position.y);
        m_endPos = new Vector2(transform.position.x, transform.position.y + m_distance);

        // Initialize audio variables
        m_source = gameObject.GetComponent<AudioSource>();
        m_source.clip = m_clip;

        SetMultiplier();
        SetColor();
    }

    private void Update()
    {
        SetMultiplier();
    }

    public float GetMultiplier() { return m_multiplier; }
    public float GetPayout() { return m_payout; }
    public Color GetColor() { return m_color; }

    public void SetMultiplier()
    {
        // Initialize updated game values
        int row = m_gameManager.GetRow();
        int risk = m_gameManager.GetRisk();
        int index = GetIndex(gameObject.name);

        // Select corresponding multiplier value
        m_multiplier = SelectMultiplier(row, risk, index);

        // Initialize and set multiplier text
        m_multiplierText.text = Math.Round(m_multiplier, 1).ToString();
    }

    public void SetColor()
    {
        int row = m_gameManager.GetRow();
        int index = GetIndex(gameObject.name);

        m_color = CalculateColor(row, index, m_minHue, m_maxHue);
        GetComponent<SpriteRenderer>().color = m_color;
    }

    public void SetPayout(float cost)
    {
        // Multiply bet with block multiplier
        m_payout = (float)Math.Round((cost * m_multiplier), 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Play sound
            if (!m_source.isPlaying) { m_source.Play(); }

            // Animate block on impact
            StartCoroutine(MovingBlock(m_time, m_startPos, m_endPos));

            // Retrieve ball cost
            float cost = (float)Math.Round(collision.gameObject.GetComponent<Ball>().GetCost(), 2);

            // Calculate payout
            float payout = cost * m_multiplier;

            // Update balance with multiplied bet amount
            m_gameManager.Deposit(payout);

            // Add new bet record
            m_gameManager.AddRecord(cost, m_multiplier, payout, m_color);

            // Add new element to widget
            m_uiManager.UpdateWidget(m_gameManager.GetLastRecord());

            // Deactivate other object on impact
            collision.gameObject.SetActive(false);
            m_gameManager.isBetting = false;
        }
    }

    IEnumerator MovingBlock(float time, Vector2 startPos, Vector2 endPos)
    {
        // Reset time
        float elapsedTime = 0f;

        // Move object down
        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset time
        elapsedTime = 0f;

        // Move object up
        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(endPos, startPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Fix position due to float variation
        if (transform.position.y != startPos.y)
        {
            transform.position = startPos;
        }
    }

    private void CalculateMultiplier(int rows, int risk, int index)
    {
        // Initialize equation variables
        float baseMultiplier = 1.0f;
        float growthFactor = 0.0f;
        float minMultiplier = 0.2f;
        float maxMultiplier = 1000.0f;

        // Adjust growth according to risk
        if (risk == 0) { growthFactor = 2.0f; }
        else if (risk == 1) { growthFactor = 2.5f; }
        else if (risk == 2) { growthFactor = 5.0f; }

        // Exponential equation
        float calculatedMultiplier = baseMultiplier * (float)Math.Pow(growthFactor, index);

        // Clamp values between min and max 
        m_multiplier = Math.Min(Math.Max(calculatedMultiplier, minMultiplier), maxMultiplier);
    }

    private float SelectMultiplier(int rows, int risk, int index)
    {
        // Initialize multiplier variable
        float tempMultiplier = 0;

        // Select multiplier value in dictionary
        if (rows == 8 && risk == 0) tempMultiplier = values["8 Low"][index];
        else if (rows == 8 && risk == 1) tempMultiplier = values["8 Medium"][index];
        else if (rows == 8 && risk == 2) tempMultiplier = values["8 High"][index];
        else if (rows == 9 && risk == 0) tempMultiplier = values["9 Low"][index];
        else if (rows == 9 && risk == 1) tempMultiplier = values["9 Medium"][index];
        else if (rows == 9 && risk == 2) tempMultiplier = values["9 High"][index];
        else if (rows == 10 && risk == 0) tempMultiplier = values["10 Low"][index];
        else if (rows == 10 && risk == 1) tempMultiplier = values["10 Medium"][index];
        else if (rows == 10 && risk == 2) tempMultiplier = values["10 High"][index];
        else if (rows == 11 && risk == 0) tempMultiplier = values["11 Low"][index];
        else if (rows == 11 && risk == 1) tempMultiplier = values["11 Medium"][index];
        else if (rows == 11 && risk == 2) tempMultiplier = values["11 High"][index];
        else if (rows == 12 && risk == 0) tempMultiplier = values["12 Low"][index];
        else if (rows == 12 && risk == 1) tempMultiplier = values["12 Medium"][index];
        else if (rows == 12 && risk == 2) tempMultiplier = values["12 High"][index];
        else if (rows == 13 && risk == 0) tempMultiplier = values["13 Low"][index];
        else if (rows == 13 && risk == 1) tempMultiplier = values["13 Medium"][index];
        else if (rows == 13 && risk == 2) tempMultiplier = values["13 High"][index];
        else if (rows == 14 && risk == 0) tempMultiplier = values["14 Low"][index];
        else if (rows == 14 && risk == 1) tempMultiplier = values["14 Medium"][index];
        else if (rows == 14 && risk == 2) tempMultiplier = values["14 High"][index];
        else if (rows == 15 && risk == 0) tempMultiplier = values["15 Low"][index];
        else if (rows == 15 && risk == 1) tempMultiplier = values["15 Medium"][index];
        else if (rows == 15 && risk == 2) tempMultiplier = values["15 High"][index];
        else if (rows == 16 && risk == 0) tempMultiplier = values["16 Low"][index];
        else if (rows == 16 && risk == 1) tempMultiplier = values["16 Medium"][index];
        else if (rows == 16 && risk == 2) tempMultiplier = values["16 High"][index];

        return tempMultiplier;
    }

    private Color CalculateColor(int total, int index, int minHue, int maxHue)
    {
        // Calculate gradient according to number of multipliers
        float hueDifference = (maxHue - minHue) / ((total / 2));
        float hue = maxHue - hueDifference * index;

        // Create new color
        Color calculatedColor = Color.HSVToRGB(hue / 360, 1.0f, 1.0f);

        return calculatedColor;
    }

    private int GetIndex(string name)
    {
        // Initialize index variable
        int index = 0;

        // Select index value based on multiplier name
        switch (name)
        {
            case "Center":
                index = 0;
                break;
            case "1st":
                index = 1;
                break;
            case "2nd":
                index = 2;
                break;
            case "3rd":
                index = 3;
                break;
            case "4th":
                index = 4;
                break;
            case "5th":
                index = 5;
                break;
            case "6th":
                index = 6;
                break;
            case "7th":
                index = 7;
                break;
            case "8th":
                index = 8;
                break;
            default:
                index = 0;
                break;
        }

        return index;
    }

    
}
