using Doozy.Runtime.Soundy;
using Doozy.Runtime.Soundy.Ids;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.UI
{

    public class UIGameFinish : MonoBehaviour
    {

        [Required]
        public UIView View;
        
        public TextMeshProUGUI TextTitle;
        public TextMeshProUGUI TextDescription;
        public TextMeshProUGUI TextKills;
        public TextMeshProUGUI TextPlayerLevel;

        public SoundId WinSound;
        public SoundId LossSound;

        private void Start()
        {
            View.OnShowCallback.Event.AddListener(OnShowing);
        }

        private void OnShowing()
        {
            var gameplay = GameplayController.Current;
            if (gameplay.IsWin)
            {
                TextTitle.text = "Congratulation";
                TextDescription.text = "You win the game, thanks for your play.\n"
                    + "This game was made in 1 month for GameOff2023, the content is still limited currently, if you like it, please feel free to give us a review."
                    + "Click \"Exit\" to back to main menu.";
                SoundyService.PlaySound(WinSound);
            }
            else
            {
                TextTitle.text = "You Died";
                var maxLevel = AssetManager.Current.Database.GameplaySetting.MaximumFinitePlayerLevel;
                TextDescription.text = $"You have to level up to {maxLevel} to win the game.";
                SoundyService.PlaySound(LossSound);
            }
            TextKills.text = gameplay.TotalKills.ToString();
            TextPlayerLevel.text = gameplay.Player.PlayerLevel.ToString();
        }

    }

}