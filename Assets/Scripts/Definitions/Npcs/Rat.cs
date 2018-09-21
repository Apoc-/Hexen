using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class Rat : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Rat";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/NpcModels/RatModel");
            this.HealthBarOffset = 0.4f;
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