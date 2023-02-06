using System;
using AnimationManager;
using GameManager;
using UnityEngine;

namespace MotionManager
{
    public class PlayerMoveController : MonoBehaviour
    {
       
         public DynamicJoystick dynamicJoystick;
         public float speed;
         public float directionSpeed;
         private Vector3 _newPosition;
         private Vector3 _newRotation;
         private Rigidbody _rigidbody;
         private Animator _animator;
         private bool _canReturn;

         private void Start()
         {
             _rigidbody = GetComponent<Rigidbody>();
             _animator = GetComponent<Animator>();
             
            
         }

         private void FixedUpdate()
        {
            SetAnimation();
            MainPlayerPositionControl();
            if (Input.GetMouseButton(0))
            {
                PlayerMovement();
            }
        }

        private void PlayerMovement()
        {
            _newPosition = new Vector3( dynamicJoystick.Horizontal* speed * Time.deltaTime, 0, dynamicJoystick.Vertical * speed * Time.deltaTime);
            _rigidbody.position += _newPosition;
            
            _newRotation = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_newRotation),directionSpeed * Time.deltaTime);
            
        }


        private void SetAnimation()
        {
            if (dynamicJoystick.Horizontal != 0 || dynamicJoystick.Vertical != 0)
            {
                AnimationController.Instance.AnimationChange(_animator,AnimationTypes.AnimationType.Run);
            }
            else
            {
                AnimationController.Instance.AnimationChange(_animator,AnimationTypes.AnimationType.Idle);
            }
        }

        private void MainPlayerPositionControl()
        {
            if (_rigidbody.velocity.y < -2f && !_canReturn)
            {
                _canReturn = true;
                _rigidbody.mass = 50f;
                DisqualifiedController.Instance.DisqualifyPlayer(gameObject);
                GameOverController.Instance.Fail();
            }
        }
    }
}
