using System.Collections.Generic;

namespace Systems.TechTreeSystem
{
    public class TechManager
    {
        private static List<Tech> techs = new List<Tech>();

        public static void RegisterTech(Tech tech)
        {
            techs.Add(tech);
        }
    }
}