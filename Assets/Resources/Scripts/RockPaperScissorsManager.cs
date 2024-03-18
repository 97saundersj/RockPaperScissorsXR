using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using TMPro;

public class RockPaperScissorsManager : MonoBehaviour
{
    public string selectedPlayerPose = null;
    private string selectedComputerPose = null;
    private string gameResult = null;
    private int roundsPlayed = 0;
    private int userScore = 0;

    public TextMeshProUGUI selectedPlayerPoseText; // Assign in inspector
    public TextMeshProUGUI selectedComputerPoseText; // Assign in inspector
    public TextMeshProUGUI gameResultText; // Assign in inspector
    public TextMeshProUGUI roundsPlayedText; // Assign in inspector
    public TextMeshProUGUI userScoreText; // Assign in inspector

    public Animator aiAnimator; // Assign in inspector

    public void SelectPose(string selectedPose)
    {
        SetPlayerPose(selectedPose);
        SetComputerPose();
        DetermineGameResult();
        UpdateScoreAndRounds();
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
        // Generate a random number between 0 and 2
        int randomIndex = new System.Random().Next(0, 3);

        // Array of possible poses
        string[] poses = { "rock", "paper", "scissors" };

        // Output the randomly selected pose
        Debug.Log("AI Selected: " + poses[randomIndex]);

        selectedComputerPose = poses[randomIndex];
        // Set animation parameter based on AI's pose

        if (aiAnimator != null)
        {
            switch (selectedComputerPose)
            {
                case "rock":
                    aiAnimator.SetInteger("selectedPose", 1);
                    break;
                case "paper":
                    aiAnimator.SetInteger("selectedPose", 2);
                    break;
                case "scissors":
                    aiAnimator.SetInteger("selectedPose", 3);
                    break;
            }
        }
    }

    private void DetermineGameResult()
    {
        // Determine the result of the game
        gameResult = DetermineWinner(selectedPlayerPose, selectedComputerPose);

        Debug.Log("Result: " + gameResult);
    }

    private void UpdateScoreAndRounds()
    {
        // Update rounds played
        roundsPlayed++;

        // Update user score based on game result
        if (gameResult == "You win!")
        {
            userScore++;
        }
    }

    IEnumerator ResetPoseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        aiAnimator.SetInteger("selectedPose", 0); // reset pose
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (selectedPlayerPose != null && selectedPlayerPose != null && gameResult != null)
            this.selectedPlayerPoseText.text = "";//"You Selected: " + selectedPlayerPose;
        if (selectedComputerPose != null)
            selectedComputerPoseText.text = "AI Selected: " + selectedComputerPose;
        if (gameResult != null)
            gameResultText.text = gameResult;
        //roundsPlayedText.text = "Rounds Played: " + roundsPlayed;
        //userScoreText.text = "User Score: " + userScore;
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
            Console.WriteLine("Selected: Scissors");
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
