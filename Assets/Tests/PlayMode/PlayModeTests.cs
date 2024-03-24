using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using TMPro;

public class PlayModeTests
{
    private RockPaperScissorsManager testRockPaperScissorsManager;

    [SetUp]
    public void TestSetup()
    {
        var testRockPaperScissorsManagerGameObject = new GameObject();
        testRockPaperScissorsManager = testRockPaperScissorsManagerGameObject.AddComponent<RockPaperScissorsManager>();

        string[] searchResults = AssetDatabase.FindAssets("AI Hand Prefab");
        string prefabPath = AssetDatabase.GUIDToAssetPath(searchResults[0]);
        var aiHandPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject aiHandGameObject = GameObject.Instantiate(aiHandPrefab);

        foreach (var textUI in aiHandGameObject.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            Debug.Log(textUI.name);

            switch(textUI.name)
            {
                case "Game Start Text":
                    testRockPaperScissorsManager.gameStartText = textUI;
                    break;
                case "Game Result Text":
                    testRockPaperScissorsManager.gameResultText = textUI;
                    break;
            }
        }
    }

    [UnityTest]
    public IEnumerator SelectRockTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectRock();
        Assert.AreEqual("rock", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectPaperTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectPaper();
        Assert.AreEqual("paper", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectScissorsTest()
    {
        TestSetup();
        testRockPaperScissorsManager.SelectScissors();
        Assert.AreEqual("scissors", testRockPaperScissorsManager.selectedPlayerPose);

        yield return null;
    }
}
