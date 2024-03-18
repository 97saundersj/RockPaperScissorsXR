using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using TMPro;

// Refactored RockPaperScissorsManager class
public class RockPaperScissorsManager : MonoBehaviour
{
    private string selectedPlayerPose = null;
    private string selectedComputerPose = null;
    private string gameResult = null;
    private int roundsPlayed = 0;
    private int userScore = 0;

    public TextMeshProUGUI gameStartText;

    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI roundsPlayedText;
    public TextMeshProUGUI userScoreText;

    public Animator aiAnimator;

    public void SelectPose(string selectedPose)
    {
        gameStartText.text = "";
        SetPlayerPose(selectedPose);
        SetComputerPose();
        DetermineGameResult();
        UpdateScoreAndRounds();
        UpdateUI();

        StartCoroutine(ResetPoseAfterDelay(2f));
    }

    private void SetPlayerPose(string selectedPose)
    {
        selectedPose = selectedPose.ToLower();
        Debug.Log("Selected: " + selectedPose);
        this.selectedPlayerPose = selectedPose;
    }

    private void SetComputerPose()
    {
        int randomIndex = new System.Random().Next(0, 3);
        string[] poses = { "rock", "paper", "scissors" };
        Debug.Log("AI Selected: " + poses[randomIndex]);
        selectedComputerPose = poses[randomIndex];

        if (aiAnimator != null)
        {
            aiAnimator.SetInteger("selectedPose", Array.IndexOf(poses, selectedComputerPose) + 1);
        }
    }

    private void DetermineGameResult()
    {
        gameResult = DetermineWinner(selectedPlayerPose, selectedComputerPose);
        Debug.Log("Result: " + gameResult);
    }

    private void UpdateScoreAndRounds()
    {
        roundsPlayed++;
        if (gameResult == "You win!")
        {
            userScore++;
        }
    }

    IEnumerator ResetPoseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        aiAnimator.SetInteger("selectedPose", 0);
    }

    void UpdateUI()
    {
        if (gameResult != null)
        {
            gameResultText.text = gameResult;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Rock"))
        {
            SelectPose("Rock");
        }

        if (GUI.Button(new Rect(10, 70, 100, 50), "Paper"))
        {
            SelectPose("Paper");
        }

        if (GUI.Button(new Rect(10, 130, 100, 50), "Scissors"))
        {
            SelectPose("Scissors");
        }

        GUI.Label(new Rect(10, 190, 200, 50), "AI Selected: " + selectedComputerPose);

        if (gameResult != null)
        {
            GUI.Label(new Rect(10, 290, 200, 50), "Rounds Played: " + roundsPlayed);
            GUI.Label(new Rect(10, 330, 200, 50), "User Score: " + userScore);
            GUI.Label(new Rect(10, 370, 200, 50), gameResult);
        }
    }

    private string DetermineWinner(string playerPose, string computerPose)
    {
        if ((playerPose == "rock" && computerPose == "scissors") ||
            (playerPose == "paper" && computerPose == "rock") ||
            (playerPose == "scissors" && computerPose == "paper"))
        {
            return "You win!";
        }
        else if (playerPose == computerPose)
        {
            return "It's a tie!";
        }
        else
        {
            return "You Lose!";
        }
    }
}
