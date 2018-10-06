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
        private float stunDuration = 4.0f;
        private float stunRadius = 3.0f;
        private bool stunTriggered = false;

        protected override void InitNpcData()
        {
            this.Name = "Goblin King";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinKing");
            this.HealthBarOffset = 0.4f;

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
            if (stunTriggered) return;
            if (npc.CurrentHealth > npc.Attributes[AttributeName.MaxHealth].Value / 2.0f) return;
            
            var towers = TargetingHelper.GetTowersInRadius(transform.position, stunRadius);

            towers.ForEach(tower =>
            {
                tower.Stun(stunDuration, this);
            });

            stunTriggered = true;
        }
    }
}
