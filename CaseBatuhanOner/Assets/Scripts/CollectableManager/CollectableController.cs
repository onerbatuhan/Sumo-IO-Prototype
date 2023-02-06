using System;
using InteractableObjects;
using PlayerManager;
using UnityEngine;

namespace CollectableManager
{
    public class CollectableController : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = transform.GetComponent<PlayerController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            ObjectTypes objectTypes = collision.transform.GetComponent<ObjectTypes>();
            CollectableItem collectableItem = collision.transform.GetComponent<CollectableItem>();
            if(objectTypes== null && collectableItem == null) return;
            CollectableFilterType(objectTypes.type, collectableItem,collision.gameObject);
        }


        private void CollectableFilterType(ObjectTypes.ObjectType objectType,CollectableItem collectableItem,GameObject collectableObject)
        {
            switch (objectType)
            {
                case ObjectTypes.ObjectType.Player:
                    break;
                case ObjectTypes.ObjectType.Item:
                    PlayerAttributeUpdate(collectableItem.collectableAttributes);
                    Destroy(collectableObject);
                    break;
               
            }
        }

        private void PlayerAttributeUpdate(CollectableAttributes collectableAttributes)
        {
            _playerController.EnlargeSize(collectableAttributes.transferSize);
            _playerController.IncreasePower(collectableAttributes.transferPower);
            _playerController.IncreaseDirectionStrength(collectableAttributes.transferPower);
            _playerController.PlayerSpeedUpper(collectableAttributes.transferSpeed);
        }
    }
}
