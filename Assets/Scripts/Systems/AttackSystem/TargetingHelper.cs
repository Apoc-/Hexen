using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public static class TargetingHelper
    {
        public static List<Collider> GetCollidersInRadius(Vector3 origin, float radius, string layerName)
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;

            var topCap = origin + new Vector3(0, 5, 0);
            var botCap = new Vector3(origin.x, baseHeight - 5, origin.z);

            var colliders = new List<Collider>(Physics.OverlapCapsule(topCap, botCap, radius, LayerMask.GetMask(layerName)));
            return colliders;
        }

        public static List<Tower> GetTowersInRadius(Vector3 origin, float radius)
        {
            return GetCollidersInRadius(origin, radius, "Towers")
                .Select(col => col.GetComponentInParent<Tower>())
                .ToList();
        }

        public static List<Npc> GetNpcsInRadius(Vector3 origin, float radius)
        {
            return GetCollidersInRadius(origin, radius, "Npcs")
                .Select(col => col.GetComponentInParent<Npc>())
                .ToList();
        }
    }
}