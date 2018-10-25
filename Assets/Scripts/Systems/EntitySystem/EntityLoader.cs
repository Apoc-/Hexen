using System.Xml.Linq;
using UnityEngine;

namespace Systems.EntitySystem
{
    public class EntityLoader : MonoBehaviour
    {
        string _path = Application.dataPath + "/StreamingAssets";
        static void LoadXmlFile()
        {
            var xml = XDocument.Load("");
        }
    }
}