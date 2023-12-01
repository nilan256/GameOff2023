using System.Collections.Generic;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.UI
{

    public class UISkillChoosePanel : MonoBehaviour
    {

        [Required]
        public UIView View;
        public StreamId ResponseId;
        
        public List<UISkillCard> Cards = new List<UISkillCard>();
        private string chosenSkillId;

        private void Start()
        {
            View.OnShowCallback.Event.AddListener(OnShowing);
            View.OnHiddenCallback.Event.AddListener(OnHidden);
        }

        private void OnShowing()
        {
            var player = GameplayController.Current.Player;
            foreach (var card in Cards)
            {
                var skill = GameplayController.Current.SkillPool.Get(player);
                card.Initialize(skill);
            }
        }

        private void OnHidden()
        {
            Cards.ForEach(card=>card.ResetDefault());
            
            var stream = SignalStream.Get(ResponseId.Category, ResponseId.Name);
            stream.SendSignal(chosenSkillId, string.Empty);
        }

        public void OnConfirmed()
        {
            var chosen = GetChosen();
            chosenSkillId = chosen.Skill.Id;
            View.Hide();
        }

        private UISkillCard GetChosen()
        {
            foreach (var card in Cards)
            {
                if (card.Toggle.isOn)
                {
                    return card;
                }
            }

            return Cards[0];
        }

    }

}