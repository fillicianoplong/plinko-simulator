using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Stage variables
    [SerializeField] public GameObject[] stages;

    // Spawn setting variables
    [SerializeField] private float m_spawnHeight;

    // Currency variables
    [SerializeField] private float m_balance;
    [SerializeField] private float m_minBet;
    [SerializeField] private float m_maxBet;
    [SerializeField] private float m_bet;

    // Physics variables
    [SerializeField] private PhysicsMaterial2D m_bounceMaterial;
    [SerializeField] private float m_gravity;
    [SerializeField] private float m_gravityScale;
    [SerializeField] private float m_bounce;
    [SerializeField] private float m_friction;

    // System settings
    [SerializeField] private float m_volume;
    [SerializeField] private float m_auto;
    [SerializeField] private float m_turbo;
    [SerializeField] private int m_row;
    [SerializeField] private int m_risk;

    // Bet status variable
    public bool isBetting = false;
    public bool isGameOver = false;
    public bool isAuto = false;
    public bool isTurbo = false;

    // History variable
    private BetHistory m_betHistory;

    void Awake()
    {
        // History variable
        m_betHistory = gameObject.GetComponent<BetHistory>();

        // Initialize system variables
        m_volume = 50.0f;
        m_auto = 4.0f;
        m_turbo = 3.0f;
        m_spawnHeight = 3.0f;

        // Initialize currency variables
        m_balance = 10000.0f;
        m_minBet = 0.01f;
        m_maxBet = 1000.0f;
        m_bet = 100.0f;

        m_row = 8;
        m_risk = 0;

        // Initialize physics variables
        m_gravity = 9.81f;
        m_gravityScale = 1.0f;
        m_bounce = 0.5f;
        m_friction = 0.1f;

        // Initialize volume
        AudioListener.volume = m_volume;

        // Initialize physics
        Physics2D.gravity = new Vector2(0, -m_gravity * m_gravityScale);
        m_bounceMaterial.bounciness = m_bounce;
        m_bounceMaterial.friction = m_friction;
    }

    void Update()
    {
        if (!isGameOver)
        {
            
        }
        else
        {
            GameOver();
        }
    }

    public void SetBalance(float balance)
    {
        // Format and set balance
        m_balance = (float)Math.Round(balance, 2);
    }

    public void SetBet(float bet)
    {
        // Format and set bet
        m_bet = (float)Math.Round(bet, 2);
    }

    public void SetMinBet(float minBet)
    {
        // Format and set minimum bet
        m_minBet = (float)Math.Round(minBet, 2);
    }

    public void SetMaxBet(float maxBet)
    {
        // Format and set maximum bet
        m_maxBet = (float)Math.Round(maxBet, 2);
    }

    public void SetRow(int row)
    {
        m_row = row;

    }

    public void SetRisk(int risk)
    {
        m_risk = risk;
    }

    public void SetGravityScale(float gravityScale)
    {
        // Format and set gravity
        m_gravityScale = (float)Math.Round(gravityScale, 2);
        Physics2D.gravity = new Vector2(0, -m_gravity * gravityScale);
    }

    public void SetBounce(float bounce)
    {
        // Format and set ball bounciness
        m_bounce = (float)Math.Round(bounce, 2);
        m_bounceMaterial.bounciness = m_bounce;
    }

    public void SetFriction(float friction)
    {
        // Format and set ball friction
        m_friction = (float)Math.Round(friction, 2);
        m_bounceMaterial.friction = m_friction;
    }

    public void SetVolume(float volume)
    {
        // Format and set volume
        m_volume = (float)Math.Round(volume, 2); ;
        AudioListener.volume = volume;
    }

    public void SetAuto(float auto)
    {
        // Set auto status
        m_auto = auto;
    }

    public void SetTurbo(float turbo)
    {
        // Set turbo status
        m_turbo = turbo;

        // Update turbo speed if turbo button is enabled
        if (isTurbo)
        {
            EnableTurbo();
        }
    }

    // Getters
    public int GetRow() { return m_row; }
    public int GetRisk() { return m_risk; }
    public float GetBalance() { return m_balance; }
    public float GetBet() { return m_bet; }
    public float GetMinBet() { return m_minBet; }
    public float GetMaxBet() { return m_maxBet; }
    public float GetGravityScale() { return m_gravityScale; }
    public float GetBounce() { return m_bounce; }
    public float GetFriction() { return m_friction; }
    public float GetVolume() { return m_volume; }
    public float GetAuto() { return m_auto; }
    public float GetTurbo() { return m_turbo; }

    public bool IsValidBet()
    {
        // Check if a bet is within possible limits
        if (m_bet <= m_balance && m_bet != 0)
        {
            return true;
        }

        return false;
    }

    public void Withdraw(float amount)
    {
        // Format and subtract bet amount from balance
        m_balance = (float)Math.Round(m_balance - amount, 2);
    }

    public void Deposit(float amount)
    {
        // Format and add winning amount to balance
        m_balance = (float)Math.Round(m_balance + amount, 2);
    }

    public void EnableTurbo()
    {
        // Set time scale to turbo speed
        Time.timeScale = m_turbo;
    }

    public void DisableTurbo()
    {
        // Set time scale to normal speed
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        // Display game over screen
    }

    public void Restart()
    {
        // Reload default scene
        SceneManager.LoadScene(0);
    }

    public void ActivateBall()
    {
        // Get single object from object pool
        GameObject pooledObj = ObjectPooler.SharedInstance.GetPooledObject();

        if (pooledObj != null)
        {
            // Activate object
            pooledObj.SetActive(true);

            // Set ball cost
            pooledObj.GetComponent<Ball>().SetCost(m_bet);

            // Withdraw ball cost from balance
            Withdraw(pooledObj.GetComponent<Ball>().GetCost());

            // Randomly select a spawn point
            int spawnPoint = (int)UnityEngine.Random.Range(0, 3);

            // Set object position based on randomly selected spawn point
            switch (spawnPoint)
            {
                case 0:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), m_spawnHeight, 0);
                    break;
                case 1:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(-3.0f, -2.9f), m_spawnHeight, 0);
                    break;
                case 2:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(2.9f, 3.0f), m_spawnHeight, 0);
                    break;
            }
        }
    }

    public void AddRecord(float cost, float multiplier, float payout, Color color)
    {
        m_betHistory.Add(cost, multiplier, payout, color);
    }

    public int GetRecordSize()
    {
        return m_betHistory.Size();
    }

    public BetRecord GetLastRecord()
    {
        return m_betHistory.GetLastRecord();
    }

    public void CreateHistoryElement()
    {
        //GameObject childObj = (GameObject)Instantiate(multiplierIcon);
        //childObj.transform.SetParent(historyWidget.transform);
    }
}