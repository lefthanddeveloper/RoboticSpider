using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text text_coinAmount;
    private int currentCoinAmount = 0;
    private Move mover;
    void Start()
    {
        mover = FindObjectOfType<Move>();
        if (mover == null)
        {
            Debug.LogError("No Player Found");
        }

        mover.onCoinGained += OnCoinGained;
    }

    private void OnCoinGained(int coinGained)
    {
        currentCoinAmount += coinGained;

        text_coinAmount.text = string.Format("COIN : ${0}", currentCoinAmount); 
    }
}
