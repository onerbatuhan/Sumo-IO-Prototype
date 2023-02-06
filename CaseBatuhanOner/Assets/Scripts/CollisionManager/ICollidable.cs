
using InteractableObjects;
using UnityEngine;

namespace CollisionManager
{
    internal interface ICollidable
    {
        void FilterByType(Collision collision);
        public CollisionTypes.CollisionType CalculateOwnDirection(Collision collision);
        public CollisionTypes.CollisionType CalculateCollisionObjectDirection(Collision collision);
        

    }
}
