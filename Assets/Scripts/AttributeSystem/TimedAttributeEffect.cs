using System.Timers;
using Hexen;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Assets.Scripts.AttributeSystem
{
    public class TimedAttributeEffect : AttributeEffect
    {
        private float duration;

        private float interval = 1000.0f;
        private float timeActive = 0;
        private Timer timer;
        private Attribute affectedAttribute;

        public TimedAttributeEffect(float value, Attribute affectedAttribute, AttributeEffectType effectType, AttributeEffectSource effectSource, float duration) : base(value, affectedAttribute.AttributeName, effectType, effectSource)
        {
            this.affectedAttribute = affectedAttribute;
            this.duration = duration;
            
            timer = new Timer(interval);
            timer.Elapsed += OnElapsed;
            timer.Start();
        }

        public TimedAttributeEffect(TimedAttributeEffect source) : base(source)
        {
        }

        private void OnElapsed(object source, ElapsedEventArgs e)
        {
            timeActive += this.interval;

            if (timeActive >= this.duration)
            {
                timer.Stop();
                
                affectedAttribute.RemoveAttributeEffect(this);
                return;
            }
        }
    }
}