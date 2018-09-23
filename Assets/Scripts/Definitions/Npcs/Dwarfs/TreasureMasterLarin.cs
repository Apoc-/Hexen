using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class TreasureMasterLarin : Npc
    {
        private bool isChest = false;
        private bool landed = false;
        private Vector3 target;
        private float coinSpawnInterval = 0.25f;
        private float coinSpawnTimer = 0;
        private float coinSpawnMaxCount = 10;
        private float coinSpawnCount = 0;

        protected override void InitNpcData()
        {
            this.Name = "Treasure Master Larin";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/TreasureMasterLarin");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Dwarfs;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f));
        }

        public override void Die(bool silent = false)
        {
            if (isChest == false)
            {
                ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/TreasureChest");
                ReloadNpcModel();

                var effect = new AttributeEffect(2.0f, AttributeName.MaxHealth, AttributeEffectType.PercentMul, this);
                GetAttribute(AttributeName.MaxHealth).AddAttributeEffect(effect);

                Heal();
                ShouldDie = false;

                this.target = this.transform.position;
                this.transform.Translate(Vector3.up * 3f);

                isChest = true;
            }
            else
            {
                base.Die(silent: true);
            }
        }

        protected virtual void FixedUpdate()
        {
            if (!isChest)
            {
                base.FixedUpdate();
                return;
            }

            var position = this.transform.position;
            if (Vector3.Distance(position, this.target) > 0.1f)
            {
                var direction = this.target - position;

                transform.SetPositionAndRotation(
                    position + direction.normalized * 16f * Time.fixedDeltaTime,
                    Quaternion.identity);
            }
            else
            {
                if (!landed)
                {
                    landed = true;
                    this.Splatter();
                }
            }

            coinSpawnTimer += Time.fixedDeltaTime;
            if (coinSpawnTimer >= coinSpawnInterval && landed)
            {
                if(coinSpawnCount >= coinSpawnMaxCount) base.Die(true);
                SpawnCoin();
                coinSpawnTimer = 0f;
            }
        }

        private void SpawnCoin()
        {
            coinSpawnCount += 1;
            var gm = GameManager.Instance;
            var dwarfs = gm.FactionManager.GetFactionByName(FactionNames.Dwarfs);
            var coin = dwarfs.GetNpc<GoldCoin>();

            var npc = gm.WaveGenerator.GenerateSingleNpc(coin);
            

            gm.WaveSpawner.SpawnSingleNpcForCurrentWave(npc, this.transform.position, Target);
        }
    }
}