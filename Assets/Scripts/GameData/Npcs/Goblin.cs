using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
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

            var baseHealth = 6.0f;

            AddAttribute(new ClampedAttribute(AttributeName.Health, baseHealth, 0f, baseHealth, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.GoldReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.XPReward, 1f, 0.5f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 4f));
        }
    }
}