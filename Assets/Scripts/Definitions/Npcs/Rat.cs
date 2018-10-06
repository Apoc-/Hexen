using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using UnityEngine;

namespace Definitions.Npcs
{
    public class Rat : Npc
    {
        protected override void InitNpcData()
        {
            Name = "Rat";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/NpcModels/RatModel");
            HealthBarOffset = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 3f));
        }
    }
}