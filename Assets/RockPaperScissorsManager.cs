using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class RockPaperScissorsManager : MonoBehaviour
{
    private string selectedPose = null;
    
    public void SelectPose(string selectedPose)
    {
        this.selectedPose = selectedPose;
        Console.WriteLine("Selected: " + selectedPose);
    }
}
