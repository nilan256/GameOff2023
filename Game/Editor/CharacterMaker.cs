using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.CharacterControl;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


namespace GameOff2023.Editor
{

    public class CharacterMaker : OdinEditorWindow
    {

        public GameObject RuntimePrefab;
        
        [FolderPath, OnValueChanged(nameof(OnCharacterFolderChanged))]
        public string CharacterFolder;

        public AnimatorController BaseAnimatorController;

        public int MaxModelCount = 15;

        public bool CloneAnimationClip = true;
        public bool OverwriteAnimationClip;

        public List<Sprite> sprites = new List<Sprite>();

        [MenuItem("Tools/Game/CharacterMaker")]
        public static void Open()
        {
            GetWindow<CharacterMaker>();
        }

        public void OnCharacterFolderChanged()
        {
            if (BaseAnimatorController == null)
            {
                var path = Path.Combine(CharacterFolder, "Base", "AnimatorController.controller");
                BaseAnimatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
            }

            if (sprites.Count == 0)
            {
                var path = Path.Combine(CharacterFolder, "Texture.psd");
                sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToList();
                sprites.Sort((a, b) => int.Parse(a.name).CompareTo(int.Parse(b.name)));
            }
        }

        [Button]
        public void Make()
        {
            // todo: refactoring
            // var characterEvolution = RuntimePrefab.GetComponent<CharacterEvolution>();
            // characterEvolution.CharacterModels = new GameObject[MaxModelCount];
            // for (int i = 0; i < MaxModelCount; i++)
            // {
            //     MakeModel(i);
            // }
            //
            // EditorUtility.SetDirty(RuntimePrefab);
            // AssetDatabase.SaveAssets();
        }

        private void MakeModel(int index)
        {
            var modelName = "Model" + (index + 1);
            var folder = Path.Combine(CharacterFolder, modelName);
            if (!AssetDatabase.IsValidFolder(folder))
            {
                AssetDatabase.CreateFolder(CharacterFolder, modelName);
            }
            var controllerPath = Path.Combine(folder, modelName + "Controller.overrideController");

            var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(controllerPath);
            var animatorExists = (bool)animatorController;
            if (!animatorExists)
            {
                animatorController = new AnimatorOverrideController();
            }
            
            animatorController.runtimeAnimatorController = BaseAnimatorController;

            if (CloneAnimationClip)
            {
                var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                foreach (var sourceClip in BaseAnimatorController.animationClips)
                {
                    var sourcePath = AssetDatabase.GetAssetPath(sourceClip);
                    var targetPath = Path.Combine(folder, Path.GetFileName(sourcePath));
                    var targetClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(targetPath);
                    if (!targetClip)
                    {
                        AssetDatabase.CopyAsset(sourcePath, targetPath);
                        targetClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(targetPath);
                    }
                    else if (OverwriteAnimationClip)
                    {
                        AssetDatabase.DeleteAsset(targetPath);
                        AssetDatabase.CopyAsset(sourcePath, targetPath);
                        targetClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(targetPath);
                    }
                    overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(sourceClip, targetClip));
                }

                animatorController.ApplyOverrides(overrides);
            }

            if (!animatorExists)
            {
                AssetDatabase.CreateAsset(animatorController, controllerPath);
            }

            if (!TryCreateChild(RuntimePrefab, modelName, out var runtimeModel))
            {
                return;
            }
            
            // todo: refactoring
            // var characterEvolution = RuntimePrefab.GetComponent<CharacterEvolution>();
            // characterEvolution.CharacterModels[index] = runtimeModel.gameObject;
            // TryCreateChild(runtimeModel, "Body", out var body);
            // var spriteRenderer = body.AddComponent<SpriteRenderer>();
            // spriteRenderer.sprite = sprites[index];
            //
            // var animator = runtimeModel.AddComponent<Animator>();
            // animator.runtimeAnimatorController = animatorController;
            //
            // if (index == 0)
            // {
            //     var character = RuntimePrefab.GetComponent<GameCharacter>();
            //     character.CharacterModel = runtimeModel;
            //     character.CharacterAnimator = animator;
            // }
            // else
            // {
            //     runtimeModel.SetActive(false);
            // }
        }

        private bool TryCreateChild(GameObject parent, string name, out GameObject child)
        {
            if (parent.transform.Find(name))
            {
                child = null;
                return false;
            }

            child = new GameObject(name);
            child.transform.SetParent(parent.transform);
            return true;
        }

    }

}