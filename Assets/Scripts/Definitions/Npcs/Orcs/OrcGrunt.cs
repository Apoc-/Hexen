using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Orcs
{
    public class OrcGrunt : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Orc Grunt";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/OrcGrunt");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Uncommon;
            Faction = FactionNames.Orcs;

            OnHit += SpeedBurst;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));
        }

        protected void SpeedBurst(Npc npc, NpcHitData hitData)
        {
            var effect = new AttributeEffect(0.5f, AttributeName.MovementSpeed, AttributeEffectType.Flat, this, 0.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(effect);
        }
    }
}