using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Dwarfs
{
    public class DwarfArchitect : Npc
    {
        protected override void InitNpcData()
        {
            Name = "Dwarf Architect";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/DwarfArchitect");
            HealthBarOffset = 0.4f;

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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));

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