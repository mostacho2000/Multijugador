using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundDisplay : MonoBehaviour
{
    public static RoundDisplay Instance { get; private set; }

    [Header("Configuración UI")]
    [SerializeField] private TextMeshProUGUI roundText; // Usa Text si no tienes TMP
    [SerializeField] private Animator roundAnimator;
    [SerializeField] private string roundUpdateTrigger = "Update";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateRoundDisplay(int currentRound)
    {
        if (roundText != null)
        {
            roundText.text = $"Round: {currentRound}";
        }

        if (roundAnimator != null)
        {
            roundAnimator.SetTrigger(roundUpdateTrigger);
        }
    }
}