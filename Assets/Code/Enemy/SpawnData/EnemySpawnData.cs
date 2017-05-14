//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//[CreateAssetMenu(menuName = "EnemyData/SpawnData")]
//public class EnemySpawnData : ScriptableObject {

//    public SpawnData[] enemyData;

   
//}

//[System.Serializable]
//public class SpawnData 
//{

//    public GameObject Enemy;
//    public enum Types
//    {
//        Wave,
//        TimedWave
//    }

//    public Types SpawnType = Types.Wave;

//    public float WaveTime;

   
//}


//[CustomEditor(typeof(EnemySpawnData))]
//public class SpawnEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        EnemySpawnData mb = (EnemySpawnData)target;

//        serializedObject.Update();
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("integers"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyData"));

//        foreach (EnemyData ed in mb.enemyData)
//        {
           
//            EditorGUILayout.ObjectField(ed.Enemy, typeof(GameObject), true);
//        }
//            serializedObject.ApplyModifiedProperties();
//    }
//}