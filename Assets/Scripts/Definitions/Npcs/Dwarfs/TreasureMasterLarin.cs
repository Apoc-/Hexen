using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Dwarfs
{
    public class TreasureMasterLarin : Npc
    {
        private bool _isChest;
        private bool _landed;
        private Vector3 _target;
        private float _coinSpawnInterval = 0.25f;
        private float _coinSpawnTimer;
        private float _coinSpawnMaxCount = 10;
        private float _coinSpawnCount;

        protected override void InitNpcData()
        {
            Name = "Treasure Master Larin";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/TreasureMasterLarin");
            HealthBarOffset = 0.4f;

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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));
        }

        public override void Die(bool silent = false)
        {
            if (_isChest == false)
            {
                ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Dwarfs/TreasureChest");
                ReloadNpcModel();

                var effect = new AttributeEffect(2.0f, AttributeName.MaxHealth, AttributeEffectType.PercentMul, this);
                GetAttribute(AttributeName.MaxHealth).AddAttributeEffect(effect);

                Heal();
                ShouldDie = false;

                _target = transform.position;
                transform.Translate(Vector3.up * 3f);

                _isChest = true;
            }
            else
            {
                base.Die(silent: true);
            }
        }

        protected new virtual void FixedUpdate()
        {
            if (!_isChest)
            {
                base.FixedUpdate();
                return;
            }

            var position = transform.position;
            if (Vector3.Distance(position, _target) > 0.1f)
            {
                var direction = _target - position;

                transform.SetPositionAndRotation(
                    position + direction.normalized * 16f * Time.fixedDeltaTime,
                    Quaternion.identity);
            }
            else
            {
                if (!_landed)
                {
                    _landed = true;
                    Splatter();
                }
            }

            _coinSpawnTimer += Time.fixedDeltaTime;
            if (_coinSpawnTimer >= _coinSpawnInterval && _landed)
            {
                if(_coinSpawnCount >= _coinSpawnMaxCount) base.Die(true);
                SpawnCoin();
                _coinSpawnTimer = 0f;
            }
        }

        private void SpawnCoin()
        {
            _coinSpawnCount += 1;
            var gm = GameManager.Instance;
            var dwarfs = gm.FactionManager.GetFactionByName(FactionNames.Dwarfs);
            var coin = dwarfs.GetNpc<GoldCoin>();
           
            var npc = gm.WaveGenerator.GenerateSingleNpc(coin);
            
            gm.WaveSpawner.SpawnSingleNpcForCurrentWave(npc, transform.position, Target);
        }
    }
}