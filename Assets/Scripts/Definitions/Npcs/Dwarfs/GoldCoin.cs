using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class GoldCoin : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Gold Coin";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/GoldCoin");
            this.HealthBarOffset = 0.4f;

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