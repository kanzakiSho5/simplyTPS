using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(SpawnField))]
public class SpawnFieldDrawer :  PropertyDrawer{

    private SpawnField spawnField;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using(new EditorGUI.PropertyScope(position, label, property))
        {
            // ラベルの大きさを小さくする
            //EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;

            var halfWidth = position.width * 0.5f;

            // それぞれのRectを求める
            var centerRect = new Rect(position)
            {
                width = position.width,
                height = 64,
            };

            var sizeRect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight,
                y = centerRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            // 各プロパティーのGUIを求める
            var centerProperty = property.FindPropertyRelative("center");
            var sizeProperty = property.FindPropertyRelative("size");

            // 各プロパティーを描画
            EditorGUI.Vector3Field(centerRect, centerProperty.displayName, centerProperty.vector3Value);
            EditorGUI.Vector3Field(sizeRect, sizeProperty.displayName, sizeProperty.vector3Value);

        }
    }
    

}
