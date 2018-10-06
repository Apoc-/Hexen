using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;
using AttributeName = Systems.AttributeSystem.AttributeName;

namespace Definitions.Npcs.Orcs
{
    public class DrokTol : Npc
    {
        protected override void InitNpcData()
        {
            Name = "Drok'Tol";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/DrokTol");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Orcs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed / 2));
            AddAttribute(new Attribute(AttributeName.AbsoluteDamageReduction, 10.0f));
        }
    }
}