using UnityEngine;

public class DebugUIManager : MonoBehaviour
{
    public RockPaperScissorsManager rockPaperScissorsManager;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Rock"))
        {
            rockPaperScissorsManager.SelectRock();
        }

        if (GUI.Button(new Rect(10, 70, 100, 50), "Paper"))
        {
            rockPaperScissorsManager.SelectPaper();
        }

        if (GUI.Button(new Rect(10, 130, 100, 50), "Scissors"))
        {
            rockPaperScissorsManager.SelectScissors();
        }

        GUI.Label(new Rect(10, 190, 200, 50), "AI Selected: " + rockPaperScissorsManager.selectedComputerPose);

        if (rockPaperScissorsManager.gameResult != null)
        {
            GUI.Label(new Rect(10, 220, 200, 50), "Rounds Played: " + rockPaperScissorsManager.roundsPlayed);

            GUI.Label(new Rect(10, 240, 200, 50), "User Score: " + rockPaperScissorsManager.userScore);
            GUI.Label(new Rect(10, 260, 200, 50), rockPaperScissorsManager.gameResult);
        }
    }
}