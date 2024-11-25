using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Xml;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    // Stage variables
    [SerializeField] public GameObject[] stages;

    // Spawn setting variables
    [SerializeField] private float spawnHeight;

    // Currency variables
    [SerializeField] private float balance;
    [SerializeField] private float minBet;
    [SerializeField] private float maxBet;
    [SerializeField] private float bet;

    // Physics variables
    [SerializeField] private PhysicsMaterial2D bounceMaterial;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityScale;
    [SerializeField] private float bounce;
    [SerializeField] private float friction;

    // System settings
    [SerializeField] private float volume;
    [SerializeField] private float auto;
    [SerializeField] private float turbo;
    [SerializeField] public string row;
    [SerializeField] public string risk;

    // Bet status variable
    public bool isBetting = false;
    public bool isGameOver = false;
    public bool isAuto = false;
    public bool isTurbo = false;

    void Awake()
    {
        // Initialize system variables
        volume = 50.0f;
        auto = 4.0f;
        turbo = 5.0f;
        spawnHeight = 3.0f;

        // Initialize currency variables
        balance = 10000.0f;
        minBet = 0.01f;
        maxBet = 1000.0f;
        bet = 1.0f;

        // Initialize physics variables
        gravity = 9.81f;
        gravityScale = 1.0f;
        bounce = 0.5f;
        friction = 0.2f;

        // Initialize volume
        AudioListener.volume = volume;

        // Initialize physics
        Physics2D.gravity = new Vector2(0, -gravity * gravityScale);
        bounceMaterial.bounciness = bounce;
        bounceMaterial.friction = friction;
    }

    void Update()
    {
        if (!this.isGameOver)
        {
            
        }
        else
        {
            Restart();
        }
    }

    public void LoadSettings()
    {
        StringBuilder sbData = new StringBuilder();
        XmlDocument xDoc = new XmlDocument();
        StringWriter swWriter = new StringWriter(sbData);

    }

    public void SetBalance(float balance)
    {
        // Format and set balance
        this.balance = (float)Math.Round(balance, 2);
    }

    public void SetBet(float bet)
    {
        // Format and set bet
        this.bet = (float)Math.Round(bet, 2);
    }

    public void SetMinBet(float minBet)
    {
        // Format and set minimum bet
        this.minBet = (float)Math.Round(minBet, 2);
    }

    public void SetMaxBet(float maxBet)
    {
        // Format and set maximum bet
        this.maxBet = (float)Math.Round(maxBet, 2);
    }

    public void SetGravityScale(float gravityScale)
    {
        // Format and set gravity
        this.gravityScale = (float)Math.Round(gravityScale, 2);
        Physics2D.gravity = new Vector2(0, -this.gravity * gravityScale);
    }

    public void SetBounce(float bounce)
    {
        // Format and set ball bounciness
        this.bounce = (float)Math.Round(bounce, 2);
        this.bounceMaterial.bounciness = this.bounce;
    }

    public void SetFriction(float friction)
    {
        // Format and set ball friction
        this.friction = (float)Math.Round(friction, 2);
        this.bounceMaterial.friction = this.friction;
    }

    public void SetVolume(float volume)
    {
        // Format and set volume
        this.volume = (float)Math.Round(volume, 2); ;
        AudioListener.volume = volume;
    }

    public void SetAuto(float auto)
    {
        // Set auto status
        this.auto = auto;
    }

    public void SetTurbo(float turbo)
    {
        // Set turbo status
        this.turbo = turbo;

        // Update turbo speed if turbo button is enabled
        if (this.isTurbo)
        {
            EnableTurbo();
        }
    }

    // Getters
    public float GetBalance() { return this.balance; }
    public float GetBet() { return this.bet; }
    public float GetMinBet() { return this.minBet; }
    public float GetMaxBet() { return this.maxBet; }
    public float GetGravityScale() { return this.gravityScale; }
    public float GetBounce() { return this.bounce; }
    public float GetFriction() { return this.friction; }
    public float GetVolume() { return this.volume; }
    public float GetAuto() { return this.auto; }
    public float GetTurbo() { return this.turbo; }

    public void ActivateBall()
    {
        // Get single object from object pool
        GameObject pooledObj = ObjectPooler.SharedInstance.GetPooledObject();

        if (pooledObj != null)
        {
            // Activate object
            pooledObj.SetActive(true);

            // Randomly selected a spawn point
            int spawnPoint = (int)UnityEngine.Random.Range(0, 3);

            // Set object position based on randomly selected spawn point
            switch(spawnPoint)
            {
                case 0:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), spawnHeight, 0);
                    break;
                case 1:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(-3.0f, -2.9f), spawnHeight, 0);
                    break;
                case 2:
                    pooledObj.transform.position = new Vector3(UnityEngine.Random.Range(2.9f, 3.0f), spawnHeight, 0);
                    break;
            }
        }
    }

    public bool IsValidBet()
    {
        // Check if a bet is within possible limits
        if (bet <= balance && bet != 0)
        {
            return true;
        }

        return false;
    }

    public void Withdraw()
    {
        // Format and subtract bet amount from balance
        this.balance = (float)Math.Round(this.balance - bet, 2);
    }

    public void Deposit(float amount)
    {
        // Format and add winning amount to balance
        this.balance = (float)Math.Round(this.balance + amount, 2);
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

    public void EnableTurbo()
    {
        // Set time scale to turbo speed
        Time.timeScale = this.turbo;
    }

    public void DisableTurbo()
    {
        // Set time scale to normal speed
        Time.timeScale = 1.0f;
    }
}