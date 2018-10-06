using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Dwarfs
{
    public class GoldCoin : Npc
    {
        protected override void InitNpcData()
        {
            Name = "Gold Coin";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/GoldCoin");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.None;
            Faction = FactionNames.Dwarfs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarities.Rare],
                GameSettings.BaselineNpcHpInc[Rarities.Rare]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));

            var goldEffect = new AttributeEffect(1, AttributeName.GoldReward, AttributeEffectType.SetValue, this);
            var xpEffect = new AttributeEffect(0, AttributeName.XPReward, AttributeEffectType.SetValue, this);

            GetAttribute(AttributeName.GoldReward).AddAttributeEffect(goldEffect);
            GetAttribute(AttributeName.XPReward).AddAttributeEffect(xpEffect);
        }
    }
}