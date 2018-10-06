using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Orcs
{
    public class OrcTrooper : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Orc Trooper";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/OrcTrooper");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Common;
            Faction = FactionNames.Orcs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));
        }
    }
}