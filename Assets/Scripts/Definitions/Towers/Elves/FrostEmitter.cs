using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Elves
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
