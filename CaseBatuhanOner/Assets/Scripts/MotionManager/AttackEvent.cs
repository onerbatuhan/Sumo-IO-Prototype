using System;
using System.Collections;
using AnimationManager;
using CollisionManager;
using PlayerManager;
using UnityEngine;
using UnityEngine.AI;

namespace MotionManager
{
    public class AttackEvent : MonoBehaviour, IAttackable
    {
       
        public float forceAmount;
        private bool _notReturn;
        private PlayerController _playerController;
        public void InitiateAttack(GameObject ownCollision, GameObject objectCollision, CollisionTypes.CollisionType ownCollisionType, CollisionTypes.CollisionType objectCollisionType, Vector3 negativeLocalDirection)
        {
           _playerController = ownCollision.GetComponent<PlayerController>();
            forceAmount = 0;
            switch (ownCollisionType)
            {
                case CollisionTypes.CollisionType.Left:
                case CollisionTypes.CollisionType.Right:
                    forceAmount += _playerController._sideDirectionStrength;
                    break;
                case CollisionTypes.CollisionType.Front:
                    forceAmount += _playerController._frontDirectionStrength;
                    PlayAnimation(ownCollision, AnimationTypes.AnimationType.Attack);
                    break;
            }
            switch (objectCollisionType)
            {
                case CollisionTypes.CollisionType.Left:
                case CollisionTypes.CollisionType.Right:
                    forceAmount += _playerController._sideDirectionStrength;
                    PlayAnimation(objectCollision, AnimationTypes.AnimationType.Attacked);
                    break;
                case CollisionTypes.CollisionType.Back:
                    PlayAnimation(objectCollision, AnimationTypes.AnimationType.Attacked);
                    forceAmount += _playerController._backDirectionStrength;
                    break;
            }

            ApplyImpulseForce(objectCollision, forceAmount, negativeLocalDirection);
        }
        public void ApplyImpulseForce(GameObject objectCollision, float forceAmount, Vector3 forceDirection)
        {
            
            Rigidbody rb = objectCollision.GetComponent<Rigidbody>();
            DisableNavMeshAgent(gameObject);
            rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
        }

        public void PlayAnimation(GameObject obj, AnimationTypes.AnimationType animation)
        {
            var animator = obj.GetComponent<Animator>();
            AnimationController.Instance.AnimationChange(animator, animation);
        }

        public float PlayerAttributesChange(PlayerController playerController)
        {
          return  playerController.GetTotalPower();
        }

        private void DisableNavMeshAgent(GameObject obj)
        {
            NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
            if (agent == null || _notReturn) return;
            _notReturn = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            StartCoroutine("WaitAndReactivateNavMeshAgent", agent);

        }
        

        private IEnumerator WaitAndReactivateNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            var currentObjectRigidbody = navMeshAgent.transform.gameObject.GetComponent<Rigidbody>();
            yield return new WaitForSeconds(2);
            _notReturn = false;
            currentObjectRigidbody.isKinematic = true;
            yield return new WaitForSeconds(.1f);
            currentObjectRigidbody.isKinematic = false;
            // navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.enabled = true;

        }
    }
}
