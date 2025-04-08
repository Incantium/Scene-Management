using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Incantium.SceneManagement
{
    /// <summary>
    /// Class representing a collection of multiple reference field for scenes.
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Incantium-Core/blob/main/Documentation~/SceneCollection.md">
    /// SceneCollection</seealso>
    [PublicAPI]
    [Serializable]
    public sealed class SceneCollection
    {
        /// <summary>
        /// The list of scenes within this collection.
        /// </summary>
        [SerializeField]
        public List<SceneReference> scenes = new();
        
        /// <summary>
        /// Method to load in all scenes of this collection asynchronously in the background.
        /// </summary>
        /// <param name="mode">If <see cref="LoadSceneMode.Single"/>, then all current scenes will be unloaded before
        /// loading.</param>
        /// <returns>The <see cref="AsyncOperation"/> of the last loading scene, or null if there were no scene to be
        /// loaded.</returns>
        public AsyncOperation LoadAllAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (scenes is not { Count: > 0 }) return null;
            if (scenes.Count == 1) return scenes[0].LoadAsync();
            
            scenes[0].LoadAsync(mode);
            
            for (var i = 1; i < scenes.Count - 1; i++)
            {
                scenes[i].LoadAsync(LoadSceneMode.Additive);
            }
            
            return scenes[^1].LoadAsync(LoadSceneMode.Additive);
        }

        /// <summary>
        /// Method to unload all scenes asynchronously in the background.
        /// </summary>
        /// <returns>The <see cref="AsyncOperation"/> of the last unloading scene, or null if there were no scenes to be
        /// unloaded.</returns>
        public AsyncOperation UnloadAllAsync()
        {
            if (scenes is not { Count: > 0 }) return null;
            
            for (var i = 0; i < scenes.Count - 1; i++)
            {
                scenes[i].UnLoadAsync();
            }
            
            return scenes[^1].UnLoadAsync();
        }
    }
}