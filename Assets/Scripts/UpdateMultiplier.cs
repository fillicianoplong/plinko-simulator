using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateMultiplier : MonoBehaviour
{
    private float m_multiplierAmount;
    private TextMeshProUGUI m_multiplierText;
    private GameManager m_gameManager;

    float[,] m_multipliers8 = new float[3, 5]
    {
    {0.5f, 1.0f, 1.1f, 2.1f, 5.6f},
    {0.4f, 0.7f, 1.3f, 3.0f, 13.0f},
    {0.2f, 0.3f, 1.5f, 4.0f, 29.0f}
    };

    float[,] m_multipliers9 = new float[3, 5]
    {
    {0.7f, 1.0f, 1.6f, 2.0f, 5.6f},
    {0.5f, 0.9f, 1.7f, 4.0f, 18.0f},
    {0.2f, 0.6f, 2.0f, 7.0f, 43.0f}
    };

    float[,] m_multipliers10 = new float[3, 6]
    {
    {0.5f, 1.0f, 1.1f, 1.4f, 3.0f, 8.9f},
    {0.4f, 0.6f, 1.4f, 2.0f, 5.0f, 22.0f},
    {0.2f, 0.3f, 0.9f, 3.0f, 10.0f, 76.0f}
    };

    float[,] m_multipliers11 = new float[3, 6]
    {
    {0.7f, 1.0f, 1.3f, 1.9f, 3.0f, 8.4f},
    {0.5f, 0.7f, 1.8f, 3.0f, 6.0f, 24.0f},
    {0.2f, 0.4f, 1.4f, 5.2f, 14.0f, 120.0f}
    };

    float[,] m_multipliers12 = new float[3, 7]
    {
    {0.5f, 1.0f, 1.1f, 1.4f, 1.6f, 3.0f, 10.0f},
    {0.3f, 0.6f, 1.1f, 2.0f, 4.0f, 11.0f, 33.0f},
    {0.2f, 0.2f, 0.7f, 2.0f, 8.1f, 24.0f, 170.0f}
    };

    float[,] m_multipliers13 = new float[3, 7]
    {
    {0.7f, 0.9f, 1.2f, 1.9f, 3.0f, 4.0f, 8.1f},
    {0.4f, 0.7f, 1.3f, 3.0f, 6.0f, 13.0f, 43.0f},
    {0.2f, 0.2f, 1.0f, 4.0f, 11.0f, 37.0f, 260.0f}
    };

    float[,] m_multipliers14 = new float[3, 8]
    {
    {0.5f, 1.0f, 1.1f, 1.3f, 1.4f, 1.9f, 4.0f, 7.1f},
    {0.2f, 0.5f, 1.0f, 1.9f, 4.0f, 7.0f, 15.0f, 58.0f},
    {0.2f, 0.2f, 0.3f, 1.9f, 5.0f, 18.0f, 56.0f, 420.0f}
    };

    float[,] m_multipliers15 = new float[3, 8]
    {
    {0.7f, 1.0f, 1.1f, 1.5f, 2.0f, 3.0f, 8.0f, 15.0f},
    {0.3f, 0.5f, 1.3f, 3.0f, 5.0f, 11.0f, 18.0f, 88.0f},
    {0.2f, 0.2f, 0.5f, 3.0f, 8.0f, 27.0f, 83.0f, 620.0f}
    };

    float[,] m_multipliers16 = new float[3, 9]
    {
    {0.5f, 1.0f, 1.1f, 1.2f, 1.4f, 1.4f, 2.0f, 9.0f, 16.0f},
    {0.3f, 0.5f, 1.0f, 1.5f, 3.0f, 5.0f, 10.0f, 41.0f, 110.0f},
    {0.2f, 0.2f, 0.2f, 2.0f, 4.0f, 9.0f, 26.0f, 130.0f, 1000.0f}
    };

    void Start()
    {
        // Get components
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        m_multiplierText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        SelectMultiplier();
    }

    private void Payout(float cost)
    {
        // Multiply bet with block multiplier
        float amount = (float)Math.Round((cost * m_multiplierAmount), 2);

        // Update balance with multiplied bet amount
        m_gameManager.Deposit(amount);
    }

    private void SelectMultiplier()
    {
        string selectedStage = m_gameManager.row;

        switch (selectedStage)
        {
            case "8":
                SetMultiplier(m_multipliers8);
                break;
            case "9":
                SetMultiplier(m_multipliers9);
                break;
            case "10":
                SetMultiplier(m_multipliers10);
                break;
            case "11":
                SetMultiplier(m_multipliers11);
                break;
            case "12":
                SetMultiplier(m_multipliers12);
                break;
            case "13":
                SetMultiplier(m_multipliers13);
                break;
            case "14":
                SetMultiplier(m_multipliers14);
                break;
            case "15":
                SetMultiplier(m_multipliers15);
                break;
            case "16":
                SetMultiplier(m_multipliers16);
                break;
        }

        m_multiplierText.text = m_multiplierAmount.ToString() + "x";
    }

    private void SetMultiplier(float[,] multiplier)
    {
        string selectedRisk = m_gameManager.risk;

        switch (selectedRisk)
        {
            case "Low Button":
                switch (gameObject.name)
                {
                    case "Center":
                        m_multiplierAmount = multiplier[0,0];
                        break;
                    case "1st":
                        m_multiplierAmount = multiplier[0,1];
                        break;
                    case "2nd":
                        m_multiplierAmount = multiplier[0,2];
                        break;
                    case "3rd":
                        m_multiplierAmount = multiplier[0,3];
                        break;
                    case "4th":
                        m_multiplierAmount = multiplier[0,4];
                        break;
                    case "5th":
                        m_multiplierAmount = multiplier[0,5];
                        break;
                    case "6th":
                        m_multiplierAmount = multiplier[0,6];
                        break;
                    case "7th":
                        m_multiplierAmount = multiplier[0,7];
                        break;
                    case "8th":
                        m_multiplierAmount = multiplier[0,8];
                        break;
                }
                break;
            case "Medium Button":
                switch (gameObject.name)
                {
                    case "Center":
                        m_multiplierAmount = multiplier[1,0];
                        break;
                    case "1st":
                        m_multiplierAmount = multiplier[1,1];
                        break;
                    case "2nd":
                        m_multiplierAmount = multiplier[1,2];
                        break;
                    case "3rd":
                        m_multiplierAmount = multiplier[1,3];
                        break;
                    case "4th":
                        m_multiplierAmount = multiplier[1,4];
                        break;
                    case "5th":
                        m_multiplierAmount = multiplier[1,5];
                        break;
                    case "6th":
                        m_multiplierAmount = multiplier[1,6];
                        break;
                    case "7th":
                        m_multiplierAmount = multiplier[1,7];
                        break;
                    case "8th":
                        m_multiplierAmount = multiplier[1,8];
                        break;
                }
                break;
            case "High Button":
                switch (gameObject.name)
                {
                    case "Center":
                        m_multiplierAmount = multiplier[2,0];
                        break;
                    case "1st":
                        m_multiplierAmount = multiplier[2,1];
                        break;
                    case "2nd":
                        m_multiplierAmount = multiplier[2,2];
                        break;
                    case "3rd":
                        m_multiplierAmount = multiplier[2,3];
                        break;
                    case "4th":
                        m_multiplierAmount = multiplier[2,4];
                        break;
                    case "5th":
                        m_multiplierAmount = multiplier[2,5];
                        break;
                    case "6th":
                        m_multiplierAmount = multiplier[2,6];
                        break;
                    case "7th":
                        m_multiplierAmount = multiplier[2,7];
                        break;
                    case "8th":
                        m_multiplierAmount = multiplier[2,8];
                        break;
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Retrieve ball cost
            float cost = (float)Math.Round(collision.gameObject.GetComponent<Ball>().GetCost(), 2);

            // Update balance with multiplied ball cost amount
            Payout(cost);

            // Deactivate other object on impact
            collision.gameObject.SetActive(false);
            m_gameManager.isBetting = false;
        }
    }
}
