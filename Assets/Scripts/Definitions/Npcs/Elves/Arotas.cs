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
        private bool _isEgg;
        private float _hatchTime = 5.0f;

        protected override void InitNpcData()
        {
            Name = "Arotas";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/Arotas");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Elves;

            OnHit += CheckHit;
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
            if (_isEgg) return;

            if (!IsSpawned) return;

            var wouldKill = (CurrentHealth - hitData.Dmg <= 0);
            if (!wouldKill) return;

            hitData.Dmg = 0;

            MorphToEgg();
        }

        private void MorphToEgg()
        {
            var sfx = new ParticleEffectData("FireCircle", gameObject, _hatchTime);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(sfx);

            var maxHealthAttr = Attributes[AttributeName.MaxHealth];
            var movementSpeedAttr = Attributes[AttributeName.MovementSpeed];

            maxHealthAttr.AddAttributeEffect(new AttributeEffect(maxHealthAttr.Value/3, AttributeName.MaxHealth, AttributeEffectType.SetValue, this, _hatchTime, MorphToPhoenix));
            Heal();

            movementSpeedAttr.AddAttributeEffect(new AttributeEffect(0.0f, AttributeName.MovementSpeed, AttributeEffectType.SetValue, this, _hatchTime));

            HealthBar.UpdateHealth(CurrentHealth / maxHealthAttr.Value);

            _isEgg = true;
        }

        private void MorphToPhoenix(Attribute attribute)
        {
            _isEgg = false;
            Heal();

            var burstEffect = new AttributeEffect(2f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this, 1.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(burstEffect);
        }
    }
}