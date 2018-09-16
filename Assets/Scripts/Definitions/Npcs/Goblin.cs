using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class Goblin : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Goblin";
            this.Model = Resources.Load<GameObject>("Prefabs/NpcModels/GoblinModel");
            this.HealthBarOffset = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 6.0f, 0.4f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 4f));
        }
    }
}