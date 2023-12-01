using Irisheep.Runtime;
using UnityEngine;

namespace Game.UI
{

    [RequireComponent(typeof(Canvas))]
    public class CanvasCameraAssigner : MonoBehaviour
    {

        public bool AssignOnStart = true;
        public TagName CameraTag;

        private void Start()
        {
            if (AssignOnStart)
            {
                AssignCameraToCanvas();
            }
        }

        public void AssignCameraToCanvas()
        {
            if (!CameraTag.HasValue)
            {
                Debug.LogWarning("Tag undefined", this);
                return;
            }

            GetComponent<Canvas>().worldCamera = FindCamera();
        }

        private Camera FindCamera()
        {
            var go = GameObject.FindWithTag(CameraTag.Name);
            if (!go)
            {
                Debug.LogWarning($"Camera not found with tag '{CameraTag.Name}'");
                return null;
            }

            var cam = go.GetComponent<Camera>();
            if (!cam)
            {
                Debug.LogWarning($"Found GameObject with tag '{CameraTag.Name}', but no camera attached on it.", go);
                return null;
            }

            return cam;
        }

    }

}