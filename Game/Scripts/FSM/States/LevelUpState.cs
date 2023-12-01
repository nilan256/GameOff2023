using Game.Events;
using UnityEngine;

namespace Game.FSM.States
{

    public class LevelUpState : SimpleState
    {

        protected override void Run()
        {
            Player.Xp = Mathf.Max(Player.Xp - Player.XpRequired, 0);
            Player.PlayerLevel += 1;
            Player.XpRequired = Evaluator.GetXpRequired(Player.PlayerLevel);
            
            Gameplay.PendingSkillBonus += 1;
            
            LevelUppedEvent.Send();
        }

    }

}