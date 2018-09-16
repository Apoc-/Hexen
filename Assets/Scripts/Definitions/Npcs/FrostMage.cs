using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.NpcSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class FrostMage : AuraNpc
    {
        protected override void InitNpcData()
        {
            this.Name = "Frost Mage";
            this.Model = Resources.Load<GameObject>("Prefabs/Npcs/Elves/FrostMage");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Elves;

            var auraAttrEffect = new AttributeEffect(-0.25f, AttributeName.AttackSpeed, AttributeEffectType.PercentMul, this);
            this.AuraEffects.Add(new AuraEffect(auraAttrEffect, true, false));
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 20.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f, 0f));
            AddAttribute(new Attribute(AttributeName.AuraRange, 2f, 0f));
        }
    }
}