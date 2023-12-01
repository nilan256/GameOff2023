using Game.GameAsset;
using UnityEngine;
using UnityEngine.Events;

namespace Game.CharacterControl
{

    public class ModelController : MonoBehaviour
    {

        public ModelSeries Models;
        public CharacterModel ModelInScene;
        public UnityEvent ModelChanged = new UnityEvent();
        public CharacterModel CurrentModel { get; private set; }

        private void Start()
        {
            InitializeModel();
        }

        public void ChangeModel(int evolution)
        {
            var modelPrefab = Models.GetModel(evolution);
            if (modelPrefab)
            {
                if (CurrentModel)
                {
                    Destroy(CurrentModel.gameObject);
                }
                
                var model = Instantiate(modelPrefab, transform);
                CurrentModel = model;
                ModelChanged.Invoke();
            }
            else
            {
                Debug.LogError($"Undefined model, evolution:'{evolution}'");
            }
        }

        private void InitializeModel()
        {
            if (ModelInScene)
            {
                CurrentModel = ModelInScene;
            }
            else
            {
                var model = Models.GetModel(1);
                CurrentModel = Instantiate(model, transform);
            }
        }

    }

}