using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMultiplier : MonoBehaviour
{
    private decimal m_multiplierAmount;
    private TextMeshProUGUI m_multiplierText;
    private GameManager m_gameManager;
    private UIManager m_uiManager;


    decimal[,] m_multipliers8 = new decimal[3, 5]
    {
    {0.5m, 1.0m, 1.1m, 2.1m, 5.6m},
    {0.4m, 0.7m, 1.3m, 3.0m, 13.0m},
    {0.2m, 0.3m, 1.5m, 4.0m, 29.0m}
    };

    decimal[,] m_multipliers9 = new decimal[3, 5]
    {
    {0.7m, 1.0m, 1.6m, 2.0m, 5.6m},
    {0.5m, 0.9m, 1.7m, 4.0m, 18.0m},
    {0.2m, 0.6m, 2.0m, 7.0m, 43.0m}
    };

    decimal[,] m_multipliers10 = new decimal[3, 6]
    {
    {0.5m, 1.0m, 1.1m, 1.4m, 3.0m, 8.9m},
    {0.4m, 0.6m, 1.4m, 2.0m, 5.0m, 22.0m},
    {0.2m, 0.3m, 0.9m, 3.0m, 10.0m, 76.0m}
    };

    decimal[,] m_multipliers11 = new decimal[3, 6]
    {
    {0.7m, 1.0m, 1.3m, 1.9m, 3.0m, 8.4m},
    {0.5m, 0.7m, 1.8m, 3.0m, 6.0m, 24.0m},
    {0.2m, 0.4m, 1.4m, 5.2m, 14.0m, 120.0m}
    };

    decimal[,] m_multipliers12 = new decimal[3, 7]
    {
    {0.5m, 1.0m, 1.1m, 1.4m, 1.6m, 3.0m, 10.0m},
    {0.3m, 0.6m, 1.1m, 2.0m, 4.0m, 11.0m, 33.0m},
    {0.2m, 0.2m, 0.7m, 2.0m, 8.1m, 24.0m, 170.0m}
    };

    decimal[,] m_multipliers13 = new decimal[3, 7]
    {
    {0.7m, 0.9m, 1.2m, 1.9m, 3.0m, 4.0m, 8.1m},
    {0.4m, 0.7m, 1.3m, 3.0m, 6.0m, 13.0m, 43.0m},
    {0.2m, 0.2m, 1.0m, 4.0m, 11.0m, 37.0m, 260.0m}
    };

    decimal[,] m_multipliers14 = new decimal[3, 8]
    {
    {0.5m, 1.0m, 1.1m, 1.3m, 1.4m, 1.9m, 4.0m, 7.1m},
    {0.2m, 0.5m, 1.0m, 1.9m, 4.0m, 7.0m, 15.0m, 58.0m},
    {0.2m, 0.2m, 0.3m, 1.9m, 5.0m, 18.0m, 56.0m, 420.0m}
    };

    decimal[,] m_multipliers15 = new decimal[3, 8]
    {
    {0.7m, 1.0m, 1.1m, 1.5m, 2.0m, 3.0m, 8.0m, 15.0m},
    {0.3m, 0.5m, 1.3m, 3.0m, 5.0m, 11.0m, 18.0m, 88.0m},
    {0.2m, 0.2m, 0.5m, 3.0m, 8.0m, 27.0m, 83.0m, 620.0m}
    };

    decimal[,] m_multipliers16 = new decimal[3, 9]
    {
    {0.5m, 1.0m, 1.1m, 1.2m, 1.4m, 1.4m, 2.0m, 9.0m, 16.0m},
    {0.3m, 0.5m, 1.0m, 1.5m, 3.0m, 5.0m, 10.0m, 41.0m, 110.0m},
    {0.2m, 0.2m, 0.2m, 2.0m, 4.0m, 9.0m, 26.0m, 130.0m, 1000.0m}
    };

    void Start()
    {
        // Get components
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        m_uiManager = GameObject.Find("Game Manager").GetComponent<UIManager>();
        m_multiplierText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        SelectMultiplier();
    }

    private void Payout()
    {
        // Multiply bet with block multiplier
        decimal amount = m_gameManager.bet * m_multiplierAmount;

        // Update balance with multiplied bet amount
        m_gameManager.GetBet(amount);
    }

    private void SelectMultiplier()
    {
        string selectedStage = m_uiManager.selectedStageText;

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

    private void SetMultiplier(decimal[,] multiplier)
    {
        string selectedRisk = m_uiManager.selectedRiskText;

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
            // Deactivate other object on impact
            collision.gameObject.SetActive(false);
            m_gameManager.isBetting = false;

            // Update balance with multiplied bet amount
            Payout();
        }
    }
}
