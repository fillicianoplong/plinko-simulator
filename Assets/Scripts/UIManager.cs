using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    // Game Settings
    private GameManager m_gameManager;

    // Input
    [SerializeField] private TMP_Text balanceTextField;
    [SerializeField] private TMP_InputField betInputField;

    // Buttons
    [SerializeField] private Button playButton;
    [SerializeField] private Button halfButton;
    [SerializeField] private Button doubleButton;
    [SerializeField] private Button lowRiskButton;
    [SerializeField] private Button mediumRiskButton;
    [SerializeField] private Button highRiskButton;
    [SerializeField] private Button turboButton;
    [SerializeField] private Button autoButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button defaultButton;
    [SerializeField] private Button resetButton;

    // Sliders
    [SerializeField] private Slider rowSlider;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider autoSlider;
    [SerializeField] private Slider turboSlider;
    [SerializeField] private Slider gravitySlider;
    [SerializeField] private Slider bounceSlider;
    [SerializeField] private Slider frictionSlider;

    // Text
    [SerializeField] private TMP_Text rowText;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private TMP_Text autoText;
    [SerializeField] private TMP_Text turboText;
    [SerializeField] private TMP_Text gravityText;
    [SerializeField] private TMP_Text bounceText;
    [SerializeField] private TMP_Text frictionText;

    // Panels
    [SerializeField] private GameObject settingsPanel;

    // Colors
    private Color lightGrey;
    private Color mediumGrey;
    private Color darkGrey;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize game manager
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Set on click listeners
        playButton.onClick.AddListener(Play);
        autoButton.onClick.AddListener(ToggleAutoplay);
        turboButton.onClick.AddListener(ToggleTurbo);
        settingsButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
        defaultButton.onClick.AddListener(ResetSettings);
        resetButton.onClick.AddListener(m_gameManager.Restart);

        // Set on click listeners with variables
        lowRiskButton.onClick.AddListener(delegate { SetRisk(lowRiskButton); });
        mediumRiskButton.onClick.AddListener(delegate { SetRisk(mediumRiskButton); });
        highRiskButton.onClick.AddListener(delegate { SetRisk(highRiskButton); });
        halfButton.onClick.AddListener(delegate { HalfBet(); });
        doubleButton.onClick.AddListener(delegate { DoubleBet(); });

        // On deselect listeners
        betInputField.onDeselect.AddListener(delegate { Format(); });

        // On value changed listeners
        betInputField.onValueChanged.AddListener(delegate { UpdateBet(); });
        rowSlider.onValueChanged.AddListener(delegate { UpdateRow(); });
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        autoSlider.onValueChanged.AddListener(delegate { UpdateAuto(); });
        turboSlider.onValueChanged.AddListener(delegate { UpdateTurbo(); });
        gravitySlider.onValueChanged.AddListener(delegate { UpdateGravityScale(); });
        bounceSlider.onValueChanged.AddListener(delegate { UpdateBounce(); });
        frictionSlider.onValueChanged.AddListener(delegate { UpdateFriction(); });

        // Initialize row slider
        m_gameManager.row = rowSlider.value.ToString();
        rowText.text = m_gameManager.row;

        // Initialize volume slider
        volumeSlider.value = (float)Math.Round(m_gameManager.GetVolume(), 2);
        volumeText.text = Math.Round(volumeSlider.value, 2).ToString("0.00");

        // Initialize auto slider
        autoSlider.value = (float)Math.Round(m_gameManager.GetAuto(), 2);
        autoText.text = Math.Round(autoSlider.value, 2).ToString("0.00");

        // Initialize turbo slider
        turboSlider.value = (float)Math.Round(m_gameManager.GetTurbo(), 2);
        turboText.text = Math.Round(turboSlider.value, 2).ToString("0.00");

        // Initialize gravity slider
        gravitySlider.value = (float)Math.Round(m_gameManager.GetGravityScale(), 2);
        gravityText.text = Math.Round(gravitySlider.value, 2).ToString("0.00");

        // Initialize bounce slider
        bounceSlider.value = (float)Math.Round(m_gameManager.GetBounce(), 2);
        bounceText.text = Math.Round(bounceSlider.value, 2).ToString("0.00");

        // Initialize friction slider
        frictionSlider.value = (float)Math.Round(m_gameManager.GetFriction(), 2);
        frictionText.text = Math.Round(frictionSlider.value, 2).ToString("0.00");

        // Initialize colors
        lightGrey = new Color(0.231f, 0.231f, 0.231f);
        mediumGrey = new Color(0.2f, 0.2f, 0.2f);
        darkGrey = new Color(0.157f, 0.157f, 0.157f);

        // Initialize buttons
        SetRisk(lowRiskButton);

        // Format text
        Format();
    }

    void Update()
    {
        // While game is running
        if (!m_gameManager.isGameOver)
        {
            // Update balance text every frame
            balanceTextField.text = m_gameManager.GetBalance().ToString("C", CultureInfo.CurrentCulture);

            // Check autoplay requirements every frame
            LimitAutoplay();

            // Enable play button if bets are valid
            ListenButtonInteractable();
        }
    }

    public void UpdateBet()
    {
        // Format bet amount
        float bet = (float)Math.Round(float.Parse(betInputField.text), 2);
        
        // Limit bet range
        if (bet < 0)
        {
            bet = m_gameManager.GetMinBet();
        }
        else if (bet > m_gameManager.GetMaxBet())
        {
            bet = m_gameManager.GetMaxBet();
        }

        // Set bet amount
        m_gameManager.SetBet(bet);
    }

    public void UpdateRow()
    {
        // Set row value from slider value
        m_gameManager.row = rowSlider.value.ToString();

        // Update text to new row value
        rowText.text = m_gameManager.row;

        // Enable selected board size and disable others
        for (int i = 0; i < m_gameManager.stages.Length; i++)
        {
            if (m_gameManager.stages[i].name == m_gameManager.row)
            {
                m_gameManager.stages[i].SetActive(true);
            }
            else
            {
                m_gameManager.stages[i].SetActive(false);
            }
        }
    }

    public void UpdateVolume()
    {
        // Set volume value from slider value
        m_gameManager.SetVolume(volumeSlider.value);

        // Update text to new volume value
        volumeText.text = Math.Round(m_gameManager.GetVolume(), 2).ToString("0.00");
    }

    public void UpdateAuto()
    {
        // Set auto value from slider value
        m_gameManager.SetAuto(autoSlider.value);

        // Update text to new auto value
        autoText.text = Math.Round(m_gameManager.GetAuto(), 2).ToString("0.00");
    }

    public void UpdateTurbo()
    {
        // Set turbo value from slider value
        m_gameManager.SetTurbo(turboSlider.value);

        // Update text to new turbo value
        turboText.text = Math.Round(m_gameManager.GetTurbo(), 2).ToString("0.00");
    }

    public void UpdateGravityScale()
    {
        // Set gravity value from slider value
        m_gameManager.SetGravityScale(gravitySlider.value);

        // Update text to new gravity value
        gravityText.text = Math.Round(m_gameManager.GetGravityScale(), 2).ToString("0.00");
    }

    public void UpdateBounce()
    {
        // Set bounce value from slider value
        m_gameManager.SetBounce(bounceSlider.value);

        // Update text to new bounce value
        bounceText.text = Math.Round(m_gameManager.GetBounce(), 2).ToString("0.00");
    }

    public void UpdateFriction()
    {
        // Set friction value from slider value
        m_gameManager.SetFriction(frictionSlider.value);

        // Update text to new friction value
        frictionText.text = Math.Round(m_gameManager.GetFriction(), 2).ToString("0.00");
    }

    public void SetRisk(Button selectedButton)
    {
        // Set risk value from selected risk button
        m_gameManager.risk = selectedButton.name;

        // Update button colors based on selected button
        if(selectedButton.name == "Low Button")
        {
            lowRiskButton.GetComponent<Image>().color = lightGrey;
            mediumRiskButton.GetComponent<Image>().color = darkGrey;
            highRiskButton.GetComponent<Image>().color = darkGrey;
        }
        else if(selectedButton.name == "Medium Button")
        {
            lowRiskButton.GetComponent<Image>().color = darkGrey;
            mediumRiskButton.GetComponent<Image>().color = lightGrey;
            highRiskButton.GetComponent<Image>().color = darkGrey;
        }
        else if (selectedButton.name == "High Button")
        {
            lowRiskButton.GetComponent<Image>().color = darkGrey;
            mediumRiskButton.GetComponent<Image>().color = darkGrey;
            highRiskButton.GetComponent<Image>().color = lightGrey;
        }
    }

    private void Play()
    {
        // Subtract bet amount from balance
        m_gameManager.Withdraw();

        // Drop ball
        m_gameManager.ActivateBall();
    }

    IEnumerator AutoPlay()
    {
        // Drop ball while conditions are met
        while (m_gameManager.isAuto == true && m_gameManager.GetBet() <= m_gameManager.GetBalance())
        {
            Play();
            yield return new WaitForSeconds(1/m_gameManager.GetAuto());
        }
    }

    public void ToggleAutoplay()
    {
        // Toggle auto value
        m_gameManager.isAuto = !m_gameManager.isAuto;

        // Start/stop autoplay and update button color
        if (m_gameManager.isAuto == true)
        {
            autoButton.GetComponent<Image>().color = mediumGrey;
            StartCoroutine(AutoPlay());
        }
        else
        {
            autoButton.GetComponent<Image>().color = darkGrey;
            StopCoroutine(AutoPlay());
        }
    }

    public void LimitAutoplay()
    {
        // Disable autoplay if conditions are not met
        if (m_gameManager.GetBalance() < m_gameManager.GetBet())
        {
            m_gameManager.isAuto = false;
            autoButton.GetComponent<Image>().color = darkGrey;
            StopCoroutine(AutoPlay());
        }
    }

    public void ToggleTurbo()
    {
        // Toggle turbo
        m_gameManager.isTurbo = !m_gameManager.isTurbo;

        // Start/stop turbo and update button color
        if (m_gameManager.isTurbo == true)
        {
            m_gameManager.EnableTurbo();
            turboButton.GetComponent<Image>().color = mediumGrey;
        }
        else
        {
            m_gameManager.DisableTurbo();
            turboButton.GetComponent<Image>().color = darkGrey;
        }
    }

    public void OpenSettings()
    {
        // Activate settings panel
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // Deactivate settings panel
        settingsPanel.SetActive(false);
    }

    public void ResetSettings()
    {
        // Reset volume
        m_gameManager.SetVolume(50.0f);
        volumeSlider.value = (float)Math.Round(m_gameManager.GetVolume(), 2);
        volumeText.text = Math.Round(volumeSlider.value, 2).ToString("0.00");

        // Reset auto speed
        m_gameManager.SetAuto(4.0f);
        autoSlider.value = (float)Math.Round(m_gameManager.GetAuto(), 2);
        autoText.text = Math.Round(autoSlider.value, 2).ToString("0.00");

        // Reset turbo speed
        m_gameManager.SetTurbo(5.0f);
        turboSlider.value = (float)Math.Round(m_gameManager.GetTurbo(), 2);
        turboText.text = Math.Round(turboSlider.value, 2).ToString("0.00");

        // Reset gravity multiplier
        m_gameManager.SetGravityScale(1.0f);
        gravitySlider.value = (float)Math.Round(m_gameManager.GetGravityScale(), 2);
        gravityText.text = Math.Round(gravitySlider.value, 2).ToString("0.00");

        // Reset ball bounciness
        m_gameManager.SetBounce(0.5f);
        bounceSlider.value = (float)Math.Round(m_gameManager.GetBounce(), 2);
        bounceText.text = Math.Round(bounceSlider.value, 2).ToString("0.00");

        // Reset ball friction
        m_gameManager.SetFriction(0.2f);
        frictionSlider.value = (float)Math.Round(m_gameManager.GetFriction(), 2);
        frictionText.text = Math.Round(frictionSlider.value, 2).ToString("0.00");
    }

    public void HalfBet()
    {
        // Half bet if possible or set bet to minimum bet limit
        if (m_gameManager.GetBet() * 0.5f > m_gameManager.GetMinBet())
        {
            m_gameManager.SetBet((float)Math.Round(m_gameManager.GetBet() * 0.5f, 2));
        }
        else
        {
            m_gameManager.SetBet((float)Math.Round(m_gameManager.GetMinBet(), 2));
        }

        // Format bet text as currency
        Format();
    }

    public void DoubleBet()
    {
        // Double bet if possible or set bet to maximum bet limit
        if (m_gameManager.GetBet() * 2.0f < m_gameManager.GetMaxBet())
        {
            m_gameManager.SetBet((float)Math.Round(m_gameManager.GetBet() * 2.0f, 2));
        }
        else
        {
            m_gameManager.SetBet((float)Math.Round(m_gameManager.GetMaxBet(), 2));
        }

        // Format bet text as currency
        Format();
    }

    private void Format()
    {
        // Format bet text as currency
        betInputField.text = m_gameManager.GetBet().ToString("0.00");
    }

    private void ListenButtonInteractable()
    {
        // Enable play button if bet is valid
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
