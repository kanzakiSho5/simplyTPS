using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{
    /* TODO: Vector3含む自作クラスのReorderableListの値の変更の適応
    ReorderableList spawnFieldList;
    

    private void OnEnable()
    {
         
        var spawnFieldProp = serializedObject.FindProperty("spawnFields");
        

        
        spawnFieldList = new ReorderableList(serializedObject, spawnFieldProp,true, true, true, true);
        spawnFieldList.elementHeight = 50;
        spawnFieldList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = spawnFieldProp.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };

        spawnFieldList.onCanRemoveCallback = (list) =>
        {
            return list.count > 1;
        };

        var defaultColor = GUI.backgroundColor;

        spawnFieldList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, spawnFieldProp.displayName);
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (spawnFieldList.index >= spawnFieldList.count)
            spawnFieldList.index = spawnFieldList.count - 1;
        
        /*
        SpawnPoint myTarget = (SpawnPoint)target;
        DrawPropertiesExcluding(serializedObject, new string[] { "spawnFields" });
        

        EditorGUI.BeginChangeCheck();
        spawnFieldList.DoLayoutList();
        if (EditorGUI.EndChangeCheck())
            Debug.Log(serializedObject.ApplyModifiedProperties());
        
        this.DrawDefaultInspector();
        
    }
    */
}


