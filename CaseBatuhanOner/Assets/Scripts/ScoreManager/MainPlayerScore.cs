using System;
using PlayerManager;
using TMPro;
using UnityEngine;

namespace ScoreManager
{
    public class MainPlayerScore : MonoBehaviour
    {
        public PlayerController mainPlayerController;
        public TextMeshProUGUI mainPlayerScoreText;

        private void LateUpdate()
        {
            mainPlayerScoreText.text ="Score: "+ mainPlayerController.score.ToString();
        }
    }
}
