using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Elves
{
    public class FrostMage : AuraNpc
    {
        protected override void InitNpcData()
        {
            this.Name = "Frost Mage";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/FrostMage");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Elves;

            var auraAttrEffect = new AttributeEffect(-0.25f, AttributeName.AttackSpeed, AttributeEffectType.PercentMul, this);
            this.AuraEffects.Add(new AuraEffect(auraAttrEffect, true, false));
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));
            AddAttribute(new Attribute(AttributeName.AuraRange, GameSettings.BaseLineNpcAuraRange));
        }
    }
}