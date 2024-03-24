using System;
using System.Collections;
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

    public TextMeshProUGUI gameStartText;

    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI roundsPlayedText;
    public TextMeshProUGUI userScoreText;

    public Animator aiAnimator;

    public GameObject particleGameObject = null;

    public CountdownTimer roundCountDownTimer = null;

    [HideInInspector]
    public string selectedPlayerPose = null;

    [HideInInspector]
    public string selectedComputerPose = null;

    [HideInInspector]
    public string gameResult = null;

    [HideInInspector]
    public int roundsPlayed = 0;

    [HideInInspector]
    public int userScore = 0;

    [HideInInspector]
    public bool roundStarted = false;

    public void NewRound()
    {
        gameResultText.text = "";

        // Disable Particle Effects from poses until we are ready
        particleGameObject.SetActive(false);
        roundCountDownTimer.StartCountDown();
    }

    public void RoundStart()
    {
        roundStarted = true;

        // Enable Particle Effects from poses
        particleGameObject.SetActive(false);
        StartCoroutine(WaitForPlayerInput(1));
    }

    IEnumerator WaitForPlayerInput(float waitTime)
    {
        if (selectedPlayerPose != null)
        {
            gameStartText.text = null;

            
            gameStartText.text = "";
            SetComputerPose();
            DetermineGameResult();
            UpdateScoreAndRounds();
            ResetPoses();
            UpdateUI();

            roundStarted = false;
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
            // Wait a second before checking user input
            gameStartText.text = "Waiting...";
            StartCoroutine(WaitForPlayerInput(0.5f));
        }
    }

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
        // Only allow player to select a pose once the game has started
        if (roundStarted)
        {
            SetPlayerPose(selectedPose.ToString());
        }
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

        // Reset AI Pose
        StartCoroutine(ResetPoseAfterDelay(2f));
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

    private void ResetPoses()
    {
        selectedPlayerPose = null;
        selectedComputerPose = null;
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
