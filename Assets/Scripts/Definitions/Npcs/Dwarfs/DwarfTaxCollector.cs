using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Dwarfs
{
    public class DwarfTaxCollector : AuraNpc
    {
        protected override void InitNpcData()
        {
            Name = "Dwarf Tax Collector";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/DwarfTaxCollector");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Dwarfs;

            var effect = new AttributeEffect(0.5f, AttributeName.MovementSpeed, AttributeEffectType.PercentAdd, this);
            var speedAura = new AuraEffect(effect, false, true);
            AuraEffects.Add(speedAura);
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(
                AttributeName.AuraRange,
                GameSettings.BaseLineNpcAuraRange));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed * 1.2f));
        }
    }
}