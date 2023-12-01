using Game.Events;
using Sirenix.OdinInspector;

namespace Game.Pickup
{

    public class PickableXp : Pickable
    {

        [MinValue(0)]
        public int Value;

        protected override void OnPicked()
        {
            GainXpEvent.Trigger(Value);
        }

    }

}