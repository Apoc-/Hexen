using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Goblins
{
    public class GoblinKing : Npc
    {
        private float _stunDuration = 4.0f;
        private float _stunRadius = 3.0f;
        private bool _stunTriggered;

        protected override void InitNpcData()
        {
            Name = "Goblin King";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinKing");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Goblins;

            OnHit += CheckExplode;
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

        private void CheckExplode(Npc npc, NpcHitData hitData)
        {
            if (_stunTriggered) return;
            if (npc.CurrentHealth > npc.Attributes[AttributeName.MaxHealth].Value / 2.0f) return;
            
            var towers = TargetingHelper.GetTowersInRadius(transform.position, _stunRadius);

            towers.ForEach(tower =>
            {
                tower.Stun(_stunDuration, this);
            });

            _stunTriggered = true;
        }
    }
}
