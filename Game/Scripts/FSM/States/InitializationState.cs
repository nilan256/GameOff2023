using System.Collections;
using Irisheep.Runtime;
using UnityEngine;

namespace Game.FSM.States
{

    public class InitializationState : CoroutineState
    {

        protected override IEnumerator Run()
        {
            yield return InitDataManager();
            yield return InitLevelManager();
            yield return InitCamera();
            yield return InitMap();
        }

        private IEnumerator InitDataManager()
        {
            AssertUtil.IsTrue(AssetManager.HasInstance);
            AssertUtil.NotNull(AssetManager.Current.Database);
            yield break;
        }

        private IEnumerator InitLevelManager()
        {
            AssertUtil.IsTrue(GameplayController.HasInstance, "No 'GameLevelManager' in scene");
            GameplayController.Current.Initialize();
            
            yield return new WaitUntil(() => GameplayController.Current.Initialized);
        }

        private IEnumerator InitCamera()
        {
            var camera = Gameplay.MainCamera;
            AssertUtil.NotNull(camera, "No main camera in scene.");

            var player = Gameplay.Player;
            camera.AddCameraTarget(player.transform, targetOffset: new Vector2(0,player.ModelController.CurrentModel.Height / 2));
            player.ModelController.ModelChanged.AddListener(OnModelChanged);
            
            yield break;
            
            void OnModelChanged()
            {
                camera.RemoveCameraTarget(player.transform);
                camera.AddCameraTarget(player.transform, targetOffset: new Vector2(0, player.ModelController.CurrentModel.Height / 2));
            }
            
        }

        private IEnumerator InitMap()
        {
            AssertUtil.NotNull(Gameplay.MapBuilder, "No map builder in scene.");

            Gameplay.MapBuilder.Initialize(Gameplay.WorldScale);
            
            yield break;
        }

    }

}