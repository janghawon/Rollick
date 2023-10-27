using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int _score;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warnning!! ScoreManager has Multiplying Rinning!!");
            return;
        }
        Instance = this;
    }

    public void AddScore(int value)
    {
        _score += value;
        UIManager.Instance.SetScore(_score);
    }
}
