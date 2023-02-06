using System;
using MotionManager;
using UnityEngine;

namespace PlayerManager
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerAttributes playerAttributes;
        private PlayerMoveController playerMoveController;
        public float _power;
        public float _sideDirectionStrength;
        public float _frontDirectionStrength;
        public float _backDirectionStrength;
        public int score;
        private void Start()
        {
            playerMoveController = transform.GetComponent<PlayerMoveController>();
            _power = playerAttributes.power;
            _sideDirectionStrength = playerAttributes.sideDirectionStrength;
            _frontDirectionStrength = playerAttributes.frontDirectionStrength;
            _backDirectionStrength = playerAttributes.backDirectionStrength;
        }

        public void IncreasePower(float addPowerCount)
        {
            _power += addPowerCount;
        }

        public void EnlargeSize(Vector3 addSizeValues)
        {
            transform.localScale += addSizeValues;
        }

        public void IncreaseDirectionStrength(float addStrengthCount)
        {
            _sideDirectionStrength += addStrengthCount;
            _frontDirectionStrength += addStrengthCount;
            _backDirectionStrength += addStrengthCount;
        }

        public float GetTotalPower()
        {
            return _power * _sideDirectionStrength * _frontDirectionStrength * _backDirectionStrength /2 *.5f;
        }

        public void PlayerSpeedUpper(float speed)
        {
            if(playerMoveController == null) return;
            playerMoveController.speed += speed;
            playerMoveController.directionSpeed += speed;
        }
    }
}
