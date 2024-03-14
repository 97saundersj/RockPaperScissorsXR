using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using TMPro;

public class RockPaperScissorsManager : MonoBehaviour
{
    private string selectedPlayerPose = null;
    private string selectedComputerPose = null;
    private string gameResult = null;
    private int roundsPlayed = 0;
    private int userScore = 0;

    public TMPro.TextMeshProUGUI selectedPlayerPoseText; // Assign in inspector
    public TMPro.TextMeshProUGUI selectedComputerPoseText; // Assign in inspector
    public TMPro.TextMeshProUGUI gameResultText; // Assign in inspector
    public TMPro.TextMeshProUGUI roundsPlayedText; // Assign in inspector
    public TMPro.TextMeshProUGUI userScoreText; // Assign in inspector

    public void SelectPose(string selectedPose)
    {
        Debug.Log("Selected: " + selectedPose);
        this.selectedPlayerPose = selectedPose;

        // Generate a random number between 0 and 2
        int randomIndex = new System.Random().Next(0, 3);

        // Array of possible poses
        string[] poses = { "Rock", "Paper", "Scissors" };

        // Output the randomly selected pose
        Debug.Log("Selected: " + poses[randomIndex]);

        // Determine the result of the game
        selectedComputerPose = poses[randomIndex];
        gameResult = DetermineWinner(selectedPlayerPose, selectedComputerPose);
        gameResultText.text = "Result: " + gameResult;
        Debug.Log("Result: " + gameResult);

        // Update rounds played
        roundsPlayed++;

        // Update user score based on game result
        if (gameResult == "Player wins!")
        {
            userScore++;
        }
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (selectedPlayerPose != null)
            selectedPlayerPoseText.text = "You Selected: " + selectedPlayerPose;
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
        if ((playerPose == "Rock" && computerPose == "Scissors") ||
            (playerPose == "Paper" && computerPose == "Rock") ||
            (playerPose == "Scissors" && computerPose == "Paper"))
        {
            return "Player wins!";
        }
        else if (playerPose == computerPose)
        {
            return "It's a tie!";
        }
        else
        {
            return "Computer wins!";
        }
    }
}
