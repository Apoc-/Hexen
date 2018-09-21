using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.NpcSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class DwarfTaxCollector : AuraNpc
    {
        protected override void InitNpcData()
        {
            this.Name = "Dwarf Tax Collector";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/DwarfTaxCollector");
            this.HealthBarOffset = 0.4f;

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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f));
        }
    }
}