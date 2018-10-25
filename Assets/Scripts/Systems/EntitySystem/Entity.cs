using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.EntitySystem
{
    public abstract class Entity : MonoBehaviour, IHasAttributes, IAttributeEffectSource, IAuraTarget
    {
        public string Name;
        public AttributeContainer Attributes;
        protected GameObject ModelPrefab;
        protected GameObject ModelGameObject;
        public int Level = 1;
        public Rarities Rarity;
        public FactionNames Faction = FactionNames.Humans;
        public string Description;
        public Tile CurrentTile;

        //events
        public delegate void LevelUpEvent(int level);
        public event LevelUpEvent OnLevelUp;
        
        //initialization
        private void InitEntity()
        {
            InitAttributes();
            InitModel();
        }

        protected abstract void InitAttributes();
        
        protected void InitModel()
        {
            if (ModelGameObject != null) return; //already loaded

            ModelGameObject = Instantiate(ModelPrefab);
            ModelGameObject.transform.SetParent(transform, false);
        }
        
        
        //IHasAttributes implementation
        public void AddAttribute(Attribute attr)
        {
            Attributes.AddAttribute(attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return Attributes[attrName];
        }

        public float GetAttributeValue(AttributeName attrName)
        {
            return Attributes[attrName].Value;
        }

        public bool HasAttribute(AttributeName attrName)
        {
            return Attributes.HasAttribute(attrName);
        }
        
        public void RemoveFinishedTimedAttributeEffects()
        {
            foreach (var pair in Attributes)
            {
                pair.Value.RemovedFinishedAttributeEffects();
            }
        }
        
        //Leveling
        protected void LevelUp(bool silent = false)
        {
            Level += 1;

            if (!silent)
            {
                OnLevelUp?.Invoke(Level);
                PlayLevelUpEffect();
            }

            foreach (var keyValuePair in Attributes)
            {
                keyValuePair.Value.LevelUp();
            }
        }
        
        private void PlayLevelUpEffect()
        {
            var pe = new ParticleEffectData("LevelUpEffect", gameObject, 3f);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(pe);
        }
    }
}