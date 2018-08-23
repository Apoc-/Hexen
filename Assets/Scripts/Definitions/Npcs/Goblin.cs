using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class Goblin : Npc
    {
        protected override void InitNpc()
        {
            this.Name = "Goblin";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/GoblinModel");
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 6.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.XPReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 4f));
        }
    }
}