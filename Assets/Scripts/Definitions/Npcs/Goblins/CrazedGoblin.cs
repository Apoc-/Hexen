using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Goblins
{
    public class CrazedGoblin : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Crazed Goblin";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/CrazedGoblin");
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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed*2));
        }

        private void SlowDown(Npc npc, NpcHitData hitData)
        {
            var effect = new AttributeEffect(-0.75f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this, 0.5f);
            Attributes[AttributeName.MovementSpeed].AddAttributeEffect(effect);
        }
    }
}
