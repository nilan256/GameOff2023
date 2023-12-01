using System.Collections;
using Doozy.Runtime.UIManager.Containers;
using Game.UI;

namespace Game.FSM.States
{

    public class GameFinishState : CoroutineState
    {

        public string ViewName;

        protected override IEnumerator Run()
        {
            UIView.Show(UIConstants.DefaultCategory, ViewName);
            while (!Finished)
            {
                // loop until the "Exit" button be clicked
                yield return null;
            }
        }

    }

}