using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using TMPro;

// Refactored RockPaperScissorsManager class
public class RockPaperScissorsManager : MonoBehaviour
{
    public enum Pose
    {
        Rock,
        Paper,
        Scissors
    }

    public string selectedPlayerPose = null;
    public string selectedComputerPose = null;
    public string gameResult = null;
    public int roundsPlayed = 0;
    public int userScore = 0;

    public TextMeshProUGUI gameStartText;

    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI roundsPlayedText;
    public TextMeshProUGUI userScoreText;

    public Animator aiAnimator;

    public void SelectRock()
    {
        SelectPose(Pose.Rock);
    }

    public void SelectPaper()
    {
        SelectPose(Pose.Paper);
    }

    public void SelectScissors()
    {
        SelectPose(Pose.Scissors);
    }

    private void SelectPose(Pose selectedPose)
    {
        gameStartText.text = "";
        SetPlayerPose(selectedPose.ToString());
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
