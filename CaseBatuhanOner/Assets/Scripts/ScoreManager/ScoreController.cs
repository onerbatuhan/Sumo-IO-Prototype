using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DesignPattern;
using GameManager;
using PlayerManager;

namespace ScoreManager
{
public class ScoreController : Singleton<ScoreController>
{
public List<TextMeshProUGUI> topScoreTexts; 
public GameObject scoreTable; 

private List<string> playerNameScoreList; 
    private List<int> playerPointScoreList; 

    private void Start()
    {
        playerNameScoreList = new List<string>();
        playerPointScoreList = new List<int>();
    }

   
    public void CalculateGameScore()
    {
        playerNameScoreList.Clear();
        playerPointScoreList.Clear();

        
        foreach (var player in GeneralPlayerList.Instance.players)
        {
            var playerAttributes = player.GetComponent<PlayerController>().playerAttributes;
            playerNameScoreList.Add(playerAttributes.nickName);
            playerPointScoreList.Add(player.GetComponent<PlayerController>().score);
        }

        //Burası biraz karışık oldu :)
        for (int i = 0; i < playerPointScoreList.Count; i++)
        {
            for (int j = i + 1; j < playerPointScoreList.Count; j++)
            {
                if (playerPointScoreList[j] > playerPointScoreList[i])
                {
                    int tempScore = playerPointScoreList[i];
                    playerPointScoreList[i] = playerPointScoreList[j];
                    playerPointScoreList[j] = tempScore;

                    string tempName = playerNameScoreList[i];
                    playerNameScoreList[i] = playerNameScoreList[j];
                    playerNameScoreList[j] = tempName;
                }
            }
        }
    }

   
    private List<string> GetTopThreePlayerNames()
    {
        int count = Math.Min(playerPointScoreList.Count, 3);
        return playerNameScoreList.GetRange(0, count);
    }

   
    public void ShowPlayersScoreTable()
    {
        scoreTable.SetActive(true);
        var topThreePlayerNames = GetTopThreePlayerNames();

        for (int i = 0; i < 4; i++)
        {
            if (i < topThreePlayerNames.Count)
            {
                int playerScore = playerPointScoreList[playerNameScoreList.FindIndex(x => x == topThreePlayerNames[i])];
                topScoreTexts[i].text = topThreePlayerNames[i] + ": " + playerScore.ToString();
            }
            else
            {
               
            }
        }
    }
}

}
