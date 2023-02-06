using System.Collections.Generic;
using AnimationManager;
using GameManager;
using UnityEngine;
using UnityEngine.AI;

namespace AIManager
{
    public class AIController : MonoBehaviour
    {
        public List<GameObject> targets;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private bool _canReturn;

        void Start()
        {
            targets = new List<GameObject>(AITargetList.Instance.aıTargetList);
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            if (targets.Contains(transform.gameObject))
            {
                targets.Remove(transform.gameObject);
            }
        }

        void Update()
        {
            FindClosestTarget();
            SetCharacterAnimation();
            DisqualifiedPlayer();



        }

        private void FindClosestTarget()
        {
            float minDistance = Mathf.Infinity;
            GameObject closestTarget = null;
    
            foreach (GameObject target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target;
                    
                }
            }

            if (closestTarget == null || !_agent.enabled) return;
            if (!targets.Contains(closestTarget)) return;
            _agent.SetDestination(closestTarget.transform.position);
           
        }

        private void SetCharacterAnimation()
        {
            float speed = _agent.velocity.magnitude;
            if (speed > 0) {
                AnimationController.Instance.AnimationChange(_animator, AnimationTypes.AnimationType.Run);
            } else {
                AnimationController.Instance.AnimationChange(_animator, AnimationTypes.AnimationType.Idle);
            }
        }

        private void DisqualifiedPlayer()
        {
            if (_rigidbody.velocity.y < -2f && !_canReturn)
            {
                _canReturn = true;
                _rigidbody.mass = 50f;
                _agent.enabled = false;
                DisqualifiedController.Instance.DisqualifyPlayer(gameObject);
            }
        }
       
    }
}