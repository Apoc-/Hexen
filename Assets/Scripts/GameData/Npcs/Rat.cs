using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
{
    public class Rat : Npc
    {
        protected override void InitNpc()
        {
            this.Name = "Rat";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/RatModel");
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new ClampedAttribute(AttributeName.Health, 6f, 0f, 6f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.XPReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 4f));
        }
    }
}