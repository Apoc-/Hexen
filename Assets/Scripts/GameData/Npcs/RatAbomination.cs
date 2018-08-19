using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
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

            AddAttribute(new ClampedAttribute(AttributeName.Health, 1000f, 0f, 1000f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.XPReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 4f));
        }
    }
}