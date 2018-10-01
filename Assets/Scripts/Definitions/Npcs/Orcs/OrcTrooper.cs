using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
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