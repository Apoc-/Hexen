using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Goblins
{
    public class GoblinBombsquad : Npc
    {
        private float stunDuration = 4.0f;
        private float stunRadius = 1.0f;

        protected override void InitNpcData()
        {
            this.Name = "Goblin Bombsquad";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinBombsquad");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Goblins;

            OnDeath += StunKiller;
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

        private void StunKiller(Npc npc, Tower killer)
        {
            var towers = TargetingHelper.GetTowersInRadius(transform.position, stunRadius);
            
            if (killer == null) return;

            killer.Stun(stunDuration, this);
        }
    }
}
