using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Assertions.Must;

public class GameManager : MonoBehaviour
{
    // Public game variables
    public GameObject[] stages;
    public decimal balance;
    public decimal bet;
    public decimal minBet;
    public decimal maxBet;

    // Game setting variables
    [SerializeField] private float spawnWidth;
    [SerializeField] private float spawnHeight;
    [SerializeField] private float maxSpeed;

    // Bet status variable
    public bool isBetting;

    void Awake()
    {
        // Initialize physics settings
        Physics2D.maxTranslationSpeed = maxSpeed;

        // Initialize misc
        isBetting = false;

        balance = 100.0m;
        bet = 0.01m;
        minBet = 0.01m;
        maxBet = 100.0m;
    }

    void Update()
    {
        
    }

    public bool GameOver()
    {
        if (balance > 0.0m || isBetting == true)
        {
            return false;
        }

        return true;
    }

    public void SetBet()
    {
        balance -= bet;
    }

    public void GetBet(decimal amount)
    {
        balance += amount;
    }

    
    public bool IsValidBet()
    {
        if (balance >= bet && bet != 0)
        {
            return true;
        }

        return false;
    }

    

    public void ActivateBall()
    {
        // Get single object from object pool
        GameObject pooledObj = ObjectPooler.SharedInstance.GetPooledObject();

        if (pooledObj != null)
        {
            pooledObj.SetActive(true);
            pooledObj.transform.position = GenerateRandomPosition();
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-spawnWidth, spawnWidth), spawnHeight, 0);
    }
}