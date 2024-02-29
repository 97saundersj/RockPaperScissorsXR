using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class RockPaperScissorsManager : MonoBehaviour
{
    private string selectedPose = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SelectPose(string selectedPose)
    {
        this.selectedPose = selectedPose;
        Console.WriteLine("Selected: " + selectedPose);
    }
}
