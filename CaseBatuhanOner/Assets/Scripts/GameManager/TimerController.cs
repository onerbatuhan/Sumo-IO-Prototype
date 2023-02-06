using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager
{
    public class TimerController : MonoBehaviour
    {
        public float gameDuration = 60.0f;
        private float startTime;
        private bool _isGameOver;
        public TextMeshProUGUI timeLeftText;



        private void Start()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - startTime >= gameDuration)
            {
                if (!_isGameOver)
                {
                    _isGameOver = true;
                    GameOverController.Instance.OnGameOver();
                }

                if (timeLeftText != null)
                {
                    timeLeftText.text = "Finish!";
                }
            }
            else if (timeLeftText != null)
            {
                timeLeftText.text = "Time left: " + Mathf.RoundToInt(gameDuration - (Time.time - startTime)).ToString();
            }
        }
    }
}