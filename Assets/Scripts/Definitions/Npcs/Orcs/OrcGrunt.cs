using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1f));
        }

        protected void SpeedBurst(NpcHitData hitData, Npc npc)
        {
            var effect = new AttributeEffect(0.5f, AttributeName.MovementSpeed, AttributeEffectType.Flat, this, 0.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(effect);
        }
    }
}