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
            this.HealthBarOffset = 0.6f;
            //this.HealthBarScale = 2.0f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 50.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 0f, 4f, LevelIncrementType.Flat));
            AddAttribute(new Attribute(AttributeName.XPReward, 0f, 4f, LevelIncrementType.Flat));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.5f));
        }
    }
}