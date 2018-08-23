using UnityEngine;

namespace Assets.EditorExtensions.PostProcessing.Runtime.Attributes
{
    public sealed class TrackballAttribute : PropertyAttribute
    {
        public readonly string method;

        public TrackballAttribute(string method)
        {
            this.method = method;
        }
    }
}
