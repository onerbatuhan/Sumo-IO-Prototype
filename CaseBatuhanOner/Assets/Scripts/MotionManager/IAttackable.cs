
using AnimationManager;
using CollisionManager;
using PlayerManager;
using UnityEngine;

namespace MotionManager
{
    interface IAttackable
    {
        void InitiateAttack(GameObject ownCollision,GameObject objectCollision,CollisionTypes.CollisionType ownCollisionType,CollisionTypes.CollisionType objectCollisionType,Vector3 objectNegativeLocalDirection);
        
        void ApplyImpulseForce(GameObject objectCollision, float forceAmount,Vector3 forceDirection);

        void PlayAnimation(GameObject ownCollision,AnimationTypes.AnimationType animationType);

        float PlayerAttributesChange(PlayerController playerController);
    }
}
