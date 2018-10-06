using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Goblins
{
    public class GoblinUnderling : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Goblin Underling";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinUnderling");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Common;
            Faction = FactionNames.Goblins;
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
