using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using UnityEngine;
using AttributeName = Systems.AttributeSystem.AttributeName;

namespace Definitions.Npcs.Elves
{
    public class Arotas : Npc
    {
        private bool isEgg;
        private float hatchTime = 5.0f;

        protected override void InitNpcData()
        {
            this.Name = "Arotas";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/Arotas");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Elves;

            this.OnHit += CheckHit;
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

        private void CheckHit(Npc npc, NpcHitData hitData)
        {
            if (isEgg) return;

            if (!isSpawned) return;

            var wouldKill = (CurrentHealth - hitData.Dmg <= 0);
            if (!wouldKill) return;

            hitData.Dmg = 0;

            MorphToEgg();
        }

        private void MorphToEgg()
        {
            var sfx = new ParticleEffectData("FireCircle", gameObject, hatchTime);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(sfx);

            var maxHealthAttr = Attributes[AttributeName.MaxHealth];
            var movementSpeedAttr = Attributes[AttributeName.MovementSpeed];

            maxHealthAttr.AddAttributeEffect(new AttributeEffect(maxHealthAttr.Value/3, AttributeName.MaxHealth, AttributeEffectType.SetValue, this, hatchTime, MorphToPhoenix));
            Heal();

            movementSpeedAttr.AddAttributeEffect(new AttributeEffect(0.0f, AttributeName.MovementSpeed, AttributeEffectType.SetValue, this, hatchTime));

            this.healthBar.UpdateHealth(CurrentHealth / maxHealthAttr.Value);

            isEgg = true;
        }

        private void MorphToPhoenix(Attribute attribute)
        {
            isEgg = false;
            Heal();

            var burstEffect = new AttributeEffect(2f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this, 1.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(burstEffect);
        }
    }
}