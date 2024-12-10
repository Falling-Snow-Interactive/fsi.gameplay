using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.SceneManagement
{
    [Serializable]
    public class FsiSceneEntry : ISerializationCallbackReceiver
    {
        [SerializeField]
        private string name;
        public string Name => name;
        
        #if UNITY_EDITOR
        [SerializeField]
        private SceneAsset scene;
        #endif
        
        public void OnBeforeSerialize()
        {
            #if UNITY_EDITOR
            if (scene != null)
            {
                name = scene.name;
            }
            #endif
        }

        public void OnAfterDeserialize() { }
    }
}