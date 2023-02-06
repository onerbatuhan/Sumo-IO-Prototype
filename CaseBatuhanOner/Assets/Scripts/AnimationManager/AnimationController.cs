using System;
using DesignPattern;
using UnityEngine;

namespace AnimationManager
{
    public class AnimationController : Singleton<AnimationController>
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int IsAttacked = Animator.StringToHash("IsAttacked");

        public void AnimationChange(Animator animator,AnimationTypes.AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationTypes.AnimationType.Idle:
                    animator.SetBool(IsMoving,false);
                    break;
                case AnimationTypes.AnimationType.Run:
                    animator.SetBool(IsMoving,true);
                    break;
                case AnimationTypes.AnimationType.Attack:
                    animator.SetTrigger(IsAttacking);
                    break;
                case AnimationTypes.AnimationType.Attacked:
                    animator.SetTrigger(IsAttacked);
                    break;
                case AnimationTypes.AnimationType.Fall:
                    break;
                case AnimationTypes.AnimationType.Win:
                    break;
                case AnimationTypes.AnimationType.Lose:
                    break;
                case AnimationTypes.AnimationType.Promotion:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}
