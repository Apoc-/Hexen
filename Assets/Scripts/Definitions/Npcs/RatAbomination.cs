using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class RatAbomination : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "RatAbomination";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/RatAbominationModel");
            this.HealthBarOffset = 0.6f;
            //this.HealthBarScale = 2.0f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.5f));
        }
    }
}