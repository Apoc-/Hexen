using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class DwarfArchitect : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Dwarf Architect";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/DwarfArchitect");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Uncommon;
            Faction = FactionNames.Dwarfs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f));

            GetAttribute(AttributeName.GoldRewardFactor).AddAttributeEffect(new AttributeEffect(
                0.5f, 
                AttributeName.GoldRewardFactor, 
                AttributeEffectType.PercentMul, this));

            GetAttribute(AttributeName.XPRewardFactor).AddAttributeEffect(new AttributeEffect(
                0.5f, AttributeName.XPRewardFactor, 
                AttributeEffectType.PercentMul, 
                this));
        }
    }
}