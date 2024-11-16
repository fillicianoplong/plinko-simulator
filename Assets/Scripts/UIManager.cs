using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    private GameManager m_gameManager;

    // UI variables
    [SerializeField] private TMP_InputField balanceTextField;
    [SerializeField] private TMP_InputField betInputField;
    [SerializeField] private Button playButton;
    [SerializeField] private Button halfButton;
    [SerializeField] private Button doubleButton;
    [SerializeField] private Button lowRiskButton;
    [SerializeField] private Button mediumRiskButton;
    [SerializeField] private Button highRiskButton;
    [SerializeField] private Button turboButton;
    [SerializeField] private Button autoButton;
    [SerializeField] private Slider stageSlider;

    public string selectedStageText;
    public string selectedRiskText;
    public bool isAuto;
    public bool isTurbo;

    private Color selectedColor;
    private Color unselectedColor;

    private void Awake()
    {
        selectedColor = new Color(0.192f, 0.204f, 0.231f);
        unselectedColor = new Color(0.133f, 0.137f, 0.157f);

        selectedStageText = stageSlider.value.ToString();
        SetRisk(lowRiskButton);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Set on click listeners
        playButton.onClick.AddListener(Play);
        autoButton.onClick.AddListener(ToggleAutoplay);
        turboButton.onClick.AddListener(ToggleTurbo);

        // Set on click listeners with variables
        lowRiskButton.onClick.AddListener(delegate { SetRisk(lowRiskButton); });
        mediumRiskButton.onClick.AddListener(delegate { SetRisk(mediumRiskButton); });
        highRiskButton.onClick.AddListener(delegate { SetRisk(highRiskButton); });
        halfButton.onClick.AddListener(delegate { HalfBet(); });
        doubleButton.onClick.AddListener(delegate { DoubleBet(); });

        // On deselect listeners
        betInputField.onDeselect.AddListener(delegate { FormatBet(); });

        // On value changed listeners
        stageSlider.onValueChanged.AddListener(delegate { SetStage(); });

        FormatBalance();
        FormatBet();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameManager.GameOver())
        {
            FormatBalance();
            LimitAutoplay();

            try
            {
                m_gameManager.bet = decimal.Parse(betInputField.text);
                if (m_gameManager.bet > m_gameManager.maxBet)
                {
                    m_gameManager.bet = m_gameManager.maxBet;
                    FormatBet();
                }

            }
            catch (System.FormatException e) { print(e); }

            ListenButtonInteractable();
        }
        else
        {
            Restart();
        }
    }

    public void SetStage()
    {
        // Get row dropdown value
        selectedStageText = stageSlider.value.ToString();

        // Activate selected game object
        for (int i = 0; i < m_gameManager.stages.Length; i++)
        {
            // Set all stages inactive besides the matching stage
            if (m_gameManager.stages[i].name == selectedStageText)
            {
                m_gameManager.stages[i].SetActive(true);
            }
            else
            {
                m_gameManager.stages[i].SetActive(false);
            }
        }
    }

    public void SetRisk(Button selectedButton)
    {
        selectedRiskText = selectedButton.name;

        if(selectedButton.name == "Low Button")
        {
            lowRiskButton.GetComponent<Image>().color = selectedColor;
            mediumRiskButton.GetComponent<Image>().color = unselectedColor;
            highRiskButton.GetComponent<Image>().color = unselectedColor;
        }
        else if(selectedButton.name == "Medium Button")
        {
            lowRiskButton.GetComponent<Image>().color = unselectedColor;
            mediumRiskButton.GetComponent<Image>().color = selectedColor;
            highRiskButton.GetComponent<Image>().color = unselectedColor;
        }
        else if (selectedButton.name == "High Button")
        {
            lowRiskButton.GetComponent<Image>().color = unselectedColor;
            mediumRiskButton.GetComponent<Image>().color = unselectedColor;
            highRiskButton.GetComponent<Image>().color = selectedColor;
        }
    }

    private void Play()
    {
        m_gameManager.SetBet();
        m_gameManager.ActivateBall();

        FormatBalance();
        FormatBet();
    }

    IEnumerator AutoPlay()
    {
        while (isAuto == true && (decimal)m_gameManager.bet <= m_gameManager.balance)
        {
            Play();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ToggleAutoplay()
    {
        // Toggle auto
        isAuto = !isAuto;

        if (isAuto == true)
        {
            autoButton.GetComponent<Image>().color = unselectedColor;
            StartCoroutine(AutoPlay());
        }
        else if (isAuto == false)
        {
            autoButton.GetComponent<Image>().color = new Color(0.169f, 0.18f, 0.2f);
            StopCoroutine(AutoPlay());
        }
    }

    public void LimitAutoplay()
    {
        if (m_gameManager.balance < (decimal)m_gameManager.bet)
        {
            isAuto = false;
            autoButton.GetComponent<Image>().color = new Color(0.169f, 0.18f, 0.2f);
            StopCoroutine(AutoPlay());
        }
    }

    public void ToggleTurbo()
    {
        // Toggle turbo
        isTurbo = !isTurbo;

        if (isTurbo == true)
        {
            turboButton.GetComponent<Image>().color = unselectedColor;
            Time.timeScale = 3.0f;
        }
        else
        {
            turboButton.GetComponent<Image>().color = new Color(0.169f, 0.18f, 0.2f);
            Time.timeScale = 1.0f;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void HalfBet()
    {
        m_gameManager.bet *= 0.5m;
        FormatBet();
    }

    public void DoubleBet()
    {
        m_gameManager.bet *= 2.0m;
        FormatBet();
    }

    private void FormatBet()
    {
        betInputField.text = m_gameManager.bet.ToString("C", CultureInfo.CurrentCulture);
    }

    private void FormatBalance()
    {
        balanceTextField.text = m_gameManager.balance.ToString("C", CultureInfo.CurrentCulture);
    }

    private void ListenButtonInteractable()
    {
        if (m_gameManager.IsValidBet())
        {
            playButton.interactable = true;
        }

        else
        {
            playButton.interactable = false;
        }
    }
}
