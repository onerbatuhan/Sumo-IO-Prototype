using System;
using InteractableObjects;
using MotionManager;
using UnityEngine;

namespace CollisionManager
{
    public class CollisionController : MonoBehaviour, ICollidable
    {
        public CollisionTypes.CollisionType _ownCollisionType;
        public CollisionTypes.CollisionType _collisionObjectType;
        private Vector3 reverseForceDirection;
        public GameObject lastPlayerCollided;
        public AttackEvent attackEvent;

        private void Start()
        {
            attackEvent = transform.GetComponent<AttackEvent>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            FilterByType(collision);
          
        }
        
        public void FilterByType(Collision collision)
        {
            
            GameObject currentCollisionObject = collision.transform.gameObject;
            if (currentCollisionObject.GetComponent<ObjectTypes>()==null) return;
           
         
            ObjectTypes objectType = currentCollisionObject.GetComponent<ObjectTypes>();
            
            switch (objectType.type)
            {
                case ObjectTypes.ObjectType.Player:
                    attackEvent.InitiateAttack(this.gameObject,currentCollisionObject,CalculateOwnDirection(collision),CalculateCollisionObjectDirection(collision), reverseForceDirection);
                    LastHitObject(collision);
                    break;
                case ObjectTypes.ObjectType.Item:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }
        }

        public CollisionTypes.CollisionType CalculateOwnDirection(Collision collision)
        {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 worldNormal = contactPoint.normal;
            Vector3 worldDirection = contactPoint.point - transform.position;
            Vector3 localNormal = transform.InverseTransformDirection(worldNormal);
            Vector3 localDirection = transform.InverseTransformDirection(worldDirection);
            
            if (Mathf.Abs(localNormal.x) > Mathf.Abs(localNormal.y) && Mathf.Abs(localNormal.x) > Mathf.Abs(localNormal.z))
            {
                if (localDirection.x > 0)
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Right;
                }
                else
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Left;
                }
            }
            else if (Mathf.Abs(localNormal.y) > Mathf.Abs(localNormal.x) && Mathf.Abs(localNormal.y) > Mathf.Abs(localNormal.z))
            {
                if (localDirection.y > 0)
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Top;
                }
                else
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Bottom;
                }
            }
            else
            {
                if (localDirection.z > 0)
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Front;
                }
                else
                {
                    _ownCollisionType = CollisionTypes.CollisionType.Back;
                }
            }

            return _ownCollisionType;
        }
        
        public CollisionTypes.CollisionType CalculateCollisionObjectDirection(Collision collision)
        {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3  _localNormal = collision.transform.InverseTransformDirection(contactPoint.normal);
            
            Vector3 collisionNormal = contactPoint.normal;
             reverseForceDirection = -collisionNormal;
             
            if (Mathf.Abs(_localNormal.x) > Mathf.Abs(_localNormal.y) && Mathf.Abs(_localNormal.x) > Mathf.Abs(_localNormal.z))
            {
                _collisionObjectType = _localNormal.x > 0 ? CollisionTypes.CollisionType.Right : CollisionTypes.CollisionType.Left;
            }
            else if (Mathf.Abs(_localNormal.y) > Mathf.Abs(_localNormal.x) && Mathf.Abs(_localNormal.y) > Mathf.Abs(_localNormal.z))
            {
                _collisionObjectType = _localNormal.y > 0 ? CollisionTypes.CollisionType.Top : CollisionTypes.CollisionType.Bottom;
            }
            else
            {
                _collisionObjectType = _localNormal.z > 0 ? CollisionTypes.CollisionType.Front : CollisionTypes.CollisionType.Back;
            }
            
            return _collisionObjectType;
        }

        private void LastHitObject(Collision collision)
        {
            lastPlayerCollided = collision.gameObject;
        }
        
    }
}
