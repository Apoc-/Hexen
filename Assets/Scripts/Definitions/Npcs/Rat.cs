using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class Rat : Npc
    {
        protected override void InitNpc()
        {
            this.Name = "Rat";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/RatModel");
            this.HealthBarOffset = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 4.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 0f, 1f, LevelIncrementType.Flat));
            AddAttribute(new Attribute(AttributeName.XPReward, 0f, 2f, LevelIncrementType.Flat));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 3f));
        }
    }
}