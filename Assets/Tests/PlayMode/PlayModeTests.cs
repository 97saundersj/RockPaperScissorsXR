using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class PlayModeTests
{
    private RockPaperScissorsManager testRockPaperScissorsManager;

    [SetUp]
    public void TestSetup()
    {
        var testRockPaperScissorsManagerGameObject = new GameObject();
        testRockPaperScissorsManager = testRockPaperScissorsManagerGameObject.AddComponent<RockPaperScissorsManager>();

        // Create new GameObjects with TextMeshProUGUI components
        var playerPoseTextObject = new GameObject();
        testRockPaperScissorsManager.selectedPlayerPoseText = playerPoseTextObject.AddComponent<TextMeshProUGUI>();

        var computerPoseTextObject = new GameObject();
        testRockPaperScissorsManager.selectedComputerPoseText = computerPoseTextObject.AddComponent<TextMeshProUGUI>();

        var gameResultTextObject = new GameObject();
        testRockPaperScissorsManager.gameResultText = gameResultTextObject.AddComponent<TextMeshProUGUI>();
    }

    [UnityTest]
    public IEnumerator SelectRockTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectPose("rock");
        Assert.AreEqual("rock", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectPaperTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectPose("paper");
        Assert.AreEqual("paper", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectScissorsTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectPose("scissors");
        Assert.AreEqual("scissors", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }
}
