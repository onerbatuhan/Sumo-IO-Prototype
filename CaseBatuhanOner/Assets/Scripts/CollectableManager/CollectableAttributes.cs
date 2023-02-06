using UnityEngine;

namespace CollectableManager
{
    [CreateAssetMenu(fileName = "CollectableAttribute", menuName = "ScriptableObjects/Item/Attribute", order = 2)]
    public class CollectableAttributes : ScriptableObject
    {
        public int transferPower;
        public Vector3 transferSize;
        public float transferSpeed;

    }
}
