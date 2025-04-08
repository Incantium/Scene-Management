using UnityEditor;
using UnityEngine;

namespace Incantium.SceneManagement.Editor
{
    /// <summary>
    /// Class that represents the custom inspector for a scene reference.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))]
    internal sealed class SceneReferenceDrawer : PropertyDrawer
    {
        /// <inheritdoc cref="PropertyDrawer.OnGUI"/>
        /// <summary>
        /// Method to draw an <see cref="SceneAsset"/> field in place of the whole object.
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var scene = property.FindPropertyRelative("scene");
            scene.objectReferenceValue = EditorGUI.ObjectField(position, property.displayName, scene.objectReferenceValue, typeof(SceneAsset), false);
            
            EditorGUI.EndProperty();
        }
    }
}