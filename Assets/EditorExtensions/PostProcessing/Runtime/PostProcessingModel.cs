using System;
using Assets.EditorExtensions.PostProcessing.Runtime.Attributes;
using UnityEngine;

namespace Assets.EditorExtensions.PostProcessing.Runtime
{
    [Serializable]
    public abstract class PostProcessingModel
    {
        [SerializeField, GetSet("enabled")]
        bool m_Enabled;
        public bool enabled
        {
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;

                if (value)
                    OnValidate();
            }
        }

        public abstract void Reset();

        public virtual void OnValidate()
        {}
    }
}
