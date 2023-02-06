using System.Collections;
using System.Collections.Generic;
using AIManager;
using CollisionManager;
using DesignPattern;
using PlayerManager;
using ScoreManager;
using UnityEngine;

namespace GameManager
{
    public class DisqualifiedController : Singleton<DisqualifiedController>
    {
        public List<GameObject> siftedObjects;
        public Score score;

        public void DisqualifyPlayer(GameObject currentObject)
        {
            siftedObjects.Add(currentObject);
            RemoveFromTargetLists(currentObject);
            GiveOpponentScore(currentObject);
            LastPlayerControl();
        }

        private void RemoveFromTargetLists(GameObject playerToBeDisqualified)
        {
            foreach (var currentObject in GeneralPlayerList.Instance.players)
            {
              var aıController =  currentObject.GetComponent<AIController>();
              if (aıController == null) continue;
              var contains = aıController.targets.Contains(playerToBeDisqualified);
              if (contains)
              {
                  aıController.targets.Remove(playerToBeDisqualified);
                  StartCoroutine("DestroyPlayer",playerToBeDisqualified);
              }

            }
        }

        private void GiveOpponentScore(GameObject currentObject)
        {
          var collisionController = currentObject.GetComponent<CollisionController>();
          if (collisionController.lastPlayerCollided != null)
          {
              collisionController.lastPlayerCollided.GetComponent<PlayerController>().score += score.score;
          }

         
          
        }
        private IEnumerator DestroyPlayer(GameObject currentObject)
        {
            yield return new WaitForSeconds(.5f);
            currentObject.SetActive(false);
            
        }

        private void LastPlayerControl()
        {
            if (siftedObjects.Count+1 == GeneralPlayerList.Instance.players.Count)
            {
                GameOverController.Instance.OnGameOver();
            }
        }
    }
}
