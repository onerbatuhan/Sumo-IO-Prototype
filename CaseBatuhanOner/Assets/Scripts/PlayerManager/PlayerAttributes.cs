using UnityEditor;
using UnityEngine;

namespace PlayerManager
{
    
    [CreateAssetMenu(fileName = "PlayerAttribute", menuName = "ScriptableObjects/Player/Attribute", order = 1)]
    
    public class PlayerAttributes : ScriptableObject
    {
        public string nickName;
        [Range(0, 10)]
        public float power;
        [Range(0, 10)]
        public float sideDirectionStrength;
        [Range(0, 10)]
        public float frontDirectionStrength;
        [Range(0, 10)]
        public float backDirectionStrength;
         public  Color color;
         

        public void OnInspectorGUI()
        {
            color = EditorGUILayout.ColorField("Color", color);
        }

    }
}
