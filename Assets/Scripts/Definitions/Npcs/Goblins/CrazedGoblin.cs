using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class CrazedGoblin : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Crazed Goblin";
            this.Model = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/CrazedGoblin");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Uncommon;
            Faction = FactionNames.Goblins;

            OnHit += SlowDown;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 3.0f));
        }

        private void SlowDown(NpcHitData hitData, Npc npc)
        {
            var effect = new AttributeEffect(-0.75f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this, 0.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(effect);
        }
    }
}
