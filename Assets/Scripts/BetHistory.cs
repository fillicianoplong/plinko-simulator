using System.Collections.Generic;
using UnityEngine;

public class BetHistory : MonoBehaviour
{
    [SerializeField] private List<BetRecord> records;

    // Start is called before the first frame update
    void Awake()
    {
        records = new List<BetRecord>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(float b, float m, float p, Color c)
    {
        records.Add(new BetRecord {
            id = records.Count,
            bet = b,
            multiplier = m,
            payout = p,
            color = c
        });
    }


    public BetRecord GetLastRecord()
    {
        return records[records.Count - 1];
    }

    public int Size()
    {
        return records.Count;
    }

    public void PrintLastBet()
    {
        int i = records.Count - 1;
        Debug.Log(+ i + ": " + (float)records[i].bet + " x " + (float)records[i].multiplier + " = " + (float)records[i].payout);
    }
}
