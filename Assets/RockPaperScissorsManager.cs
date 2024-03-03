using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class RockPaperScissorsManager : MonoBehaviour
{
    private string selectedPlayerPose = null;
    private string selectedComputerPose = null;
    private string gameResult = null;
    private int roundsPlayed = 0;
    private int userScore = 0;

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
        Debug.Log("Result: " + gameResult);

        // Update rounds played
        roundsPlayed++;

        // Update user score based on game result
        if (gameResult == "Player wins!")
        {
            userScore++;
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
            Console.WriteLine("Selected: Scissors");
            SelectPose("Scissors");
        }

        GUI.Label(new Rect(10, 190, 200, 50), "AI Selected: " + selectedComputerPose);

        if(gameResult != null)
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
