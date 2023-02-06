using DG.Tweening;
using InteractableObjects;
using UnityEngine;

namespace CollectableManager
{
    public class ItemSpawner : MonoBehaviour {

        public GameObject objectToClone;
        public GameObject cloneAreaObject;
        private float _planeWidth;
        private float _planeLength;
        public int cloneMaxCount;
        private int cloneCount;
        public int cloneRepeatRate;
        public CollectableAttributes collectableAttributes;
        void Start()
        {
            Mesh mesh = cloneAreaObject.transform.GetComponent<MeshFilter>().mesh;
            Vector3 planeSize = mesh.bounds.size;
            _planeWidth = planeSize.x;
            _planeLength = planeSize.z;
            InvokeRepeating("Clone", 0, cloneRepeatRate);
        }

        void Clone()
        {
                if (cloneCount >= cloneMaxCount) return;
                cloneCount++;
                float xPos = Random.Range(-_planeWidth / 2, _planeWidth / 2);
                float zPos = Random.Range(-_planeLength / 2, _planeLength / 2);
                Vector3 randomPos = new Vector3(xPos, 0, zPos);

                var cloneObject = Instantiate(objectToClone, randomPos+Vector3.up, objectToClone.transform.rotation,transform);
                cloneObject.AddComponent<ObjectTypes>().type = ObjectTypes.ObjectType.Item;
                cloneObject.AddComponent<CollectableItem>().collectableAttributes = collectableAttributes;
                CloneObjectAnimation(cloneObject.transform);
           
           
        }

        void CloneObjectAnimation(Transform currentObjectTransform)
        {
            currentObjectTransform.DOMoveY(currentObjectTransform.position.y + 1, 1, false)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
