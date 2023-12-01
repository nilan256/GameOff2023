using System.Collections;
using Game.Events;
using Irisheep.Runtime;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(menuName = "Game/Animation/WorldScale")]
    public class WorldScaleAnimation : ScriptableObject
    {

        public float DefaultCameraSize = 135f;
        public LayerMask InvisibleInAnimation;
        public float ZoomInDuration = 2f;
        public float FlashInTime = 0.1f;
        public float FlashOutTime = 0.3f;
        public float ZoomOutDuration = 3f;

        public IEnumerator Play()
        {
            var gameplay = GameplayController.Current;
            var player = gameplay.Player;
            var camera = gameplay.MainCamera;

            // set culling mask
            var cullingMaskBefore = camera.GameCamera.cullingMask;
            camera.GameCamera.RemoveCullingMask(InvisibleInAnimation);

            // clear projectiles and enemies
            InstantKillAllEvent.Send();
            ClearProjectilesEvent.Send();
            
            // do overlay flash effect
            OverlayGraphic.Current.Flash(Color.white, FlashInTime, FlashOutTime);
            yield return new WaitForSecondsRealtime(FlashInTime);
            
            camera.UpdateScreenSize(DefaultCameraSize / 2);

            // change player model
            player.Evolve();
            player.transform.position /= 2;
            gameplay.MainCamera.MoveCameraInstantlyToPosition(player.transform.position);

            // change world scale level
            gameplay.MapBuilder.ChangeScaleLevel(gameplay.WorldScale);
            
            yield return new WaitForSecondsRealtime(FlashOutTime);

            // restore camera size
            gameplay.MainCamera.UpdateScreenSize(DefaultCameraSize, ZoomOutDuration);
            yield return new WaitForSecondsRealtime(ZoomOutDuration);

            // restore camera culling mask
            camera.GameCamera.cullingMask = cullingMaskBefore;
        }

    }

}