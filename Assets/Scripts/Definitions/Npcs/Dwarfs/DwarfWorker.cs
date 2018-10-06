using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Dwarfs
{
    public class DwarfWorker : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Dwarf Worker";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/DwarfWorker");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Common;
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
        }
    }
}