using UnityEngine;

namespace ScoreManager
{
    [CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score/Value", order = 3)]
    public class Score : ScriptableObject
    {
        public int score;
    }
}
