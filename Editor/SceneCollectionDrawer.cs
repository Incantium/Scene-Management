using UnityEditor;
using UnityEngine;

namespace Incantium.SceneManagement.Editor
{
    /// <summary>
    /// Class representing the custom inspector for a scene collection.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneCollection))]
    internal sealed class SceneCollectionDrawer : PropertyDrawer
    {
        /// <inheritdoc cref="PropertyDrawer.OnGUI"/>
        /// <summary>
        /// Method to draw the list of scenes in place of the whole object.
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var scenes = property.FindPropertyRelative("scenes");
            EditorGUI.PropertyField(position, scenes, label);
            
            EditorGUI.EndProperty();
        }
        
        /// <inheritdoc cref="PropertyDrawer.GetPropertyHeight"/>
        /// <summary>
        /// Method to dynamically resize the property based upon the sizing of the scene list.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var scenes = property.FindPropertyRelative("scenes");
            
            return EditorGUI.GetPropertyHeight(scenes, label, true);
        }
    }
}