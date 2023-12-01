using Game.Data;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Simple Property")]
    public class SimplePropertySkill : SkillData
    {

        public BonusPropertyType Type;
        public float Value;


        public override bool CanGain(Player player)
        {
            return true;
        }

        public override void OnGained(Player player)
        {
            switch (Type)
            {
            case BonusPropertyType.MaximumHealth:
                player.MaximumHealth += Value;
                player.CurrentHealth = Mathf.Min(player.CurrentHealth + Value, player.MaximumHealth);
                break;
            case BonusPropertyType.HealthRegen:
                player.HealthRegen += Value;
                break;
            case BonusPropertyType.AttackPower:
                player.AttackPower += Value;
                break;
            case BonusPropertyType.AttackSpeed:
                player.AttackSpeed += Value;
                break;
            case BonusPropertyType.KnockbackPower:
                player.KnockbackPower += Value;
                break;
            case BonusPropertyType.CritChance:
                player.CritChance += Value;
                break;
            case BonusPropertyType.CritPower:
                player.CritPower += Value;
                break;
            case BonusPropertyType.DamageResistance:
                player.DamageResistance += Value;
                break;
            case BonusPropertyType.MoveSpeed:
                player.MoveSpeed += Value;
                break;
            case BonusPropertyType.PickupRange:
                player.PickupRange += Value;
                break;
            case BonusPropertyType.ProjectileAmount:
                player.ProjectileAmount += (int)Value;
                break;
            case BonusPropertyType.XpMultiplier:
                player.XpMultiplier += Value;
                break;
            default:
                Debug.LogError("Unsupported type " + Type, this);
                break;
            }
            
        }

    }

}