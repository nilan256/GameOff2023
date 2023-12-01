namespace Game.Combat
{

    public abstract class ProjectileBehaviour : GameBehaviour
    {

        public PlayerAttack Owner { get; protected set; }

        private void Awake()
        {
            Owner = GetComponentInParent<PlayerAttack>();
        }

    }

}