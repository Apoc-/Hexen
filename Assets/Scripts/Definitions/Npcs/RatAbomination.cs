using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using UnityEngine;

namespace Definitions.Npcs
{
    public class RatAbomination : Npc
    {
        protected override void InitNpcData()
        {
            Name = "RatAbomination";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/NpcModels/RatAbominationModel");
            HealthBarOffset = 0.6f;
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