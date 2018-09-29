using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public class TargetingHelper : MonoBehaviour
    {
        public Npc CurrentTargetedNpc = null;
        private GameObject selectionEffect = null;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentTargetedNpc != null)
                {
                    CancelNpcTargeting();
                }

                TargetNpc();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (CurrentTargetedNpc != null)
                {
                    CancelNpcTargeting();
                }
            }
        }

        private void TargetNpc()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Npcs")))
            {
                CurrentTargetedNpc = hit.transform.gameObject.GetComponentInParent<Npc>();

                DisplaySelectionEffect();
            }

            GameManager.Instance.TowerBuildManager.BuiltTowers.ForEach(tower => tower.ForceRetarget());
        }

        private void CancelNpcTargeting()
        {
            CurrentTargetedNpc = null;
            RemoveSelectionEffect();
        }

        private void DisplaySelectionEffect()
        {
            selectionEffect = Instantiate(Resources.Load<GameObject>("Sfx/SelectionEffect"));
            selectionEffect.transform.parent = CurrentTargetedNpc.transform;
            selectionEffect.transform.localPosition = Vector3.zero;
        }

        private void RemoveSelectionEffect()
        {
            Destroy(selectionEffect.gameObject);
            selectionEffect = null;
        }

        public bool CurrentTargetInRange(Tower tower)
        {
            if (CurrentTargetedNpc == null) return false;

            var tPos = tower.transform.position;
            var ctPos = CurrentTargetedNpc.gameObject.transform.position;
            var range = tower.GetAttributeValue(AttributeName.AttackRange);

            return Vector3.Distance(tPos, ctPos) <= range;
        }

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