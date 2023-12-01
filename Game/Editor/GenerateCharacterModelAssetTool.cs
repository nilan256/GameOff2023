using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameOff2023.Editor
{

    public class GenerateCharacterModelAssetTool : OdinEditorWindow
    {

        [FolderPath]
        public string Root;

        public AnimatorController BaseAnimatorController;

        public int MaxModelCount = 15;

        public List<AnimationClip> Animations = new List<AnimationClip>();

        [MenuItem("Tools/GameOff2023/GenCharacterModelAsset")]
        public static void Open()
        {
            GetWindow<GenerateCharacterModelAssetTool>(true);
        }
        
        [Button]
        public void GenerateModels()
        {
            for (int i = 0; i < MaxModelCount; i++)
            {
                var folderName = "Model" + i;
                var folderPath = Path.Combine(Root, folderName);
                if (!AssetDatabase.IsValidFolder(folderPath))
                {
                    AssetDatabase.CreateFolder(Root, folderName);
                }

                var animator = new AnimatorOverrideController();
                animator.runtimeAnimatorController = BaseAnimatorController;

                var list = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                foreach (var clip in Animations)
                {
                    var path = AssetDatabase.GetAssetPath(clip);
                    var toPath = Path.Combine(folderPath, Path.GetFileName(path));
                    DeleteIfExists(toPath);
                    AssetDatabase.CopyAsset(path, toPath);
                    var newClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(toPath);
                    list.Add(new KeyValuePair<AnimationClip, AnimationClip>(clip, newClip));
                }

                animator.ApplyOverrides(list);

                var controllerPath = Path.Combine(folderPath, folderName + "Controller.overrideController");
                DeleteIfExists(controllerPath);
                AssetDatabase.CreateAsset(animator, controllerPath);
            }
        }

        private void DeleteIfExists(string path)
        {
            if (AssetDatabase.LoadAssetAtPath<Object>(path))
            {
                AssetDatabase.DeleteAsset(path);
            }
        }

    }

}