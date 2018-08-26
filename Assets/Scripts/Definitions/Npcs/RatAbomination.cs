using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class RatAbomination : Npc
    {
        protected override void InitNpc()
        {
            this.Name = "RatAbomination";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/RatAbominationModel");
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 100.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.XPReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 2f));
        }
    }
}