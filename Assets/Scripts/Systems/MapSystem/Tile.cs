using Systems.NpcSystem;
using Systems.TowerSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.MapSystem
{
    public class Tile : MonoBehaviour
    {
        public Material Material { get; set; }
        public TileType TileType { get; set; }
        [CanBeNull] private TileEffect tileEffect;
        private float tileEffectDuration;
        private float tileEffectTimer;

        public Tower PlacedTower;

        public bool IsEmpty = true;

        public int NumberInPath = -1;

        public float DeltaHeight { get; set; }

        private void Update()
        {
            HandleTileEffect();
        }

        private void HandleTileEffect()
        {
            if (!(tileEffectDuration > 0)) return;
            tileEffectTimer += Time.deltaTime;

            if (!(tileEffectTimer >= tileEffectDuration)) return;

            tileEffect = null;
            tileEffectTimer = 0;
            tileEffectDuration = -1;
        }

        public Vector3 GetTopCenter()
        {
            var h = GetTileHeight();
            return transform.position + new Vector3(0, h, 0);
        }

        public float GetTileHeight()
        {
            var meshFilter = GetComponent<MeshFilter>();
            return meshFilter.mesh.bounds.size.y;
        }

        public void SetTileEffect(TileEffect tileEffect, float duration = -1)
        {
            this.tileEffect = tileEffect;
            tileEffectDuration = duration;
            tileEffectTimer = 0;
        }

        public void EnterTile(Npc npc)
        {
            tileEffect?.ApplyEffectToNpc(npc);
        }
    }
}
