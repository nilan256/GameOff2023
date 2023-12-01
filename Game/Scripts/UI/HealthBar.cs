using Game.CharacterControl;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    [RequireComponent(typeof(RectTransform))]
    public class HealthBar : MonoBehaviour
    {

        public Player Player;
        public Image Fill;

        private CharacterModel cachedModel;
        private RectTransform rt;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
        }

        private void Update()
        {
            UpdateHealthBarPosition();
            UpdateHealthProgress();
        }

        private void UpdateHealthBarPosition()
        {
            var currentModel = Player.ModelController.CurrentModel;
            if (!currentModel || currentModel == cachedModel) return;
            var height = currentModel.Height;

            var position = transform.localPosition;
            position.y = height + rt.rect.height / 2;
            transform.localPosition = position;

            cachedModel = currentModel;
        }

        private void UpdateHealthProgress()
        {
            Fill.fillAmount = Player.CurrentHealth / Player.MaximumHealth;
        }

    }

}