using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using AttributeName = Assets.Scripts.Systems.AttributeSystem.AttributeName;

namespace Assets.Scripts.Definitions.Npcs
{
    public class DrokTol : Npc
    {
        private float damageReduction = 10.0f;

        protected override void InitNpcData()
        {
            this.Name = "Drok'Tol";
            this.Model = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/DrokTol");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Orcs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.MaxHealth, 100.0f, 0.4f));
            AddAttribute(new Attribute(AttributeName.MovementSpeed, 0.75f, 0f));
            AddAttribute(new Attribute(AttributeName.AbsoluteDamageReduction, 10.0f, 0.0f));
        }
    }
}