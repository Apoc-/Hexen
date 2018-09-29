using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class ArcaneApprentice : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Arcane Apprentice";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/ArcaneApprentice");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Common;
            Faction = FactionNames.Elves;
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