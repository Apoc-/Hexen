using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class FrostEmitter : AuraTower
    {
        public override void InitTowerData()
        {
            Name = "Frost Emitter";
            Faction = FactionNames.Elves;
            Rarity = Rarities.Rare;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that slows nearby creeps by 25% and deals continous damage to them.";

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Elves/Emitter");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/Banner");
            AuraEffects.Add(new AuraEffect(
                attributeEffect: new AttributeEffect(-0.25f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this), 
                affectsTowers: false, 
                affectsNpcs: true));

            WeaponHeight = 1;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AuraRange, GameSettings.BaseLineTowerAuraRange));

            AddAttribute(new Attribute(AttributeName.AuraDamage
                , GameSettings.BaselineTowerDmg[Rarity] / 4  //fourth due to ticks
                , GameSettings.BaselineTowerDmgInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.AuraTicksPerSecond, 4.0f));
        }
    }
}
