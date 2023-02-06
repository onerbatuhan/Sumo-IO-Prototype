using UnityEngine;

namespace InteractableObjects
{
    public class ObjectTypes : MonoBehaviour
    {
      public enum ObjectType
      {
          Player,
          Item
      }

      public ObjectType type;
    }
}
