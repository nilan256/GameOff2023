using HutongGames.PlayMaker;

namespace Game.FSM
{

    [ActionCategory("Game")]
    public abstract class GameAction : FsmStateAction
    {

        protected GameplayController Gameplay => GameplayController.Current;

        protected Player Player
        {
            get
            {
                if (!Gameplay) return null;
                return Gameplay.Player;
            }
        }

    }

}