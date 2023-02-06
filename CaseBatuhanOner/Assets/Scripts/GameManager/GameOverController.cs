using System.Collections;
using AIManager;
using CollisionManager;
using DesignPattern;
using MotionManager;
using ScoreManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameOverController : Singleton<GameOverController>
    {

        public GameObject successText;
        public GameObject failText;
        public void OnGameOver()
        {
            ScoreController.Instance.CalculateGameScore();
            AllPlayerNotMove();
            ScoreController.Instance.ShowPlayersScoreTable();
            Success();
        }
        
        private static void AllPlayerNotMove()
        {
            foreach (var currentObject in GeneralPlayerList.Instance.players)
            {
                var rigidbody = currentObject.GetComponent<Rigidbody>();
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                rigidbody.isKinematic = true;

                var collider = currentObject.GetComponent<Collider>();
                collider.enabled = false;
                
                NavMeshAgent navMeshAgent = currentObject.GetComponent<NavMeshAgent>();
                if (navMeshAgent == null) continue;
                currentObject.GetComponent<AIController>().enabled = false;
                navMeshAgent.velocity = Vector3.zero;
            }
        }

        public void Fail()
        {
            failText.SetActive(true);
            StartCoroutine("GameRestartDuration",2);
        }
        
        public void Success()
        {
            successText.SetActive(true);
            StartCoroutine("GameRestartDuration",4);
        }
        private IEnumerator GameRestartDuration(float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
