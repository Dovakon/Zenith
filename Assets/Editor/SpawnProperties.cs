//using UnityEditor;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(EnemyProperities))]
//public class  SpawnProperties: PropertyDrawer
//{

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return Screen.width < 333 ? (16f + 18f) : 16f;
//    }

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        label = EditorGUI.BeginProperty(position, label, property);
//        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
//        if (position.height > 16f)
//        {
//            position.height = 16f;
//            EditorGUI.indentLevel += 1;
//            contentPosition = EditorGUI.IndentedRect(position);
//            contentPosition.y += 18f;
//        }
//        contentPosition.width *= .35f;
//        EditorGUI.indentLevel = 0;
//        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Enemy"), GUIContent.none);
//        contentPosition.x += contentPosition.width;
//       // contentPosition.width /= 2f;
//        EditorGUIUtility.labelWidth = 50f;
//        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("NumberOfSwpan"), new GUIContent("Times"));
//        contentPosition.x += contentPosition.width;
//        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("ratio"), new GUIContent("Ratio"));
        
//        EditorGUI.EndProperty();
        
//    }
    
//}