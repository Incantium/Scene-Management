using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Incantium.SceneManagement
{
    /// <summary>
    /// Class representing a reference field for scenes.
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Incantium-Core/blob/main/Documentation~/SceneReference.md">
    /// SceneReference</seealso>
    [PublicAPI]
    [Serializable]
    public sealed partial class SceneReference : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Reference to a scene that is engine-safe.
        /// </summary>
        [SerializeField] 
        private Object scene;

        /// <summary>
        /// The name of the scene.
        /// </summary>
        [field: SerializeField] 
        public string name { get; private set; }
        
        /// <inheritdoc cref="Scene.buildIndex"/>
        public int index => SceneManager.GetSceneByName(name).buildIndex;

        /// <inheritdoc cref="Scene.isLoaded"/>
        public bool isLoaded => SceneManager.GetSceneByName(name).isLoaded;
        
        /// <summary>
        /// Loads the scene asynchronously in the background.
        /// </summary>
        /// <param name="mode">If <see cref="LoadSceneMode.Single"/>, then all current scenes will be unloaded before
        /// loading.</param>
        /// <returns>Determines if the operation has completed.</returns>
        public AsyncOperation LoadAsync(LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadSceneAsync(name, mode);
        
        /// <summary>
        /// Unloads the scene asynchronously in the background.
        /// </summary>
        /// <returns>Determines if the operation has completed.</returns>
        public AsyncOperation UnLoadAsync() => SceneManager.UnloadSceneAsync(name);
        
        /// <summary>
        /// Method to serialize this scene reference. Will update the reference if the scene has changed name.
        /// </summary>
        [Obsolete("This method is only meant for running on build")]
        public void OnBeforeSerialize() => name = scene ? scene.name : null;

        /// <summary>
        /// Method to deserialize this scene reference.
        /// </summary>
        /// <remarks>This method is meant only for running on build.</remarks>
        [Obsolete("This method is only meant for running on build")]
        public void OnAfterDeserialize() {}
        
        /// <summary>
        /// Implicit conversion of a scene reference to its scene name.
        /// </summary>
        /// <param name="scene">The scene reference.</param>
        /// <returns>The scene name.</returns>
        public static implicit operator string(SceneReference scene) => scene.name;
        
        /// <summary>
        /// Implicit conversion of a scene reference to its scene index.
        /// </summary>
        /// <param name="scene">The scene reference.</param>
        /// <returns>The scene build index.</returns>
        public static implicit operator int(SceneReference scene) => scene.index;
    }
}