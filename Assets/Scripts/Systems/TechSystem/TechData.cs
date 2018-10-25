using Systems.TechTreeSystem;

namespace Systems.TechSystem
{
    public class TechData
    {
        public static void DefineTechs()
        {
            var singleTarget = Tech.Define
                .WithName("Single Target")
                .WithDescription("Increases your chances to get bears that attack multiple Targets.");
            
            var multiTarget = Tech.Define
                .WithName("Multi Target")
                .WithDescription("Increases your chances to get bears with negativ effects on the baddies.");

            var offensive = Tech.Define
                .WithName("Offensive")
                .WithDescription("Increases your chances to get bears that affect other bears or baddies.")
                .Unlocks(singleTarget)
                .Unlocks(multiTarget);
            
            var buff = Tech.Define
                .WithName("Buff")
                .WithDescription("Increases your chances to get bears with positive effects on other bears.");
            
            var debuff = Tech.Define
                .WithName("Debuff")
                .WithDescription("Increases your chances to get bears with negativ effects on the baddies.");

            var support = Tech.Define
                .WithName("Support")
                .WithDescription("Increases your chances to get bears that affect other bears or baddies.")
                .Unlocks(buff)
                .Unlocks(debuff);
            
            var eco = Tech.Define
                .WithName("Economy")
                .WithDescription("Increases your chances to get bears with effects based on your economy.");
            
            var xp = Tech.Define
                .WithName("Experience")
                .WithDescription("Increases your chances to get bears with effects based on the experience gained by towers.");

            var special = Tech.Define
                .WithName("Special")
                .WithDescription("Increases your chances to get towers with special effects.")
                .Unlocks(xp)
                .Unlocks(eco);
            
            TechManager.RegisterTech(special);
            TechManager.RegisterTech(xp);
            TechManager.RegisterTech(eco);
            
            TechManager.RegisterTech(support);
            TechManager.RegisterTech(debuff);
            TechManager.RegisterTech(buff);
            
            TechManager.RegisterTech(offensive);
            TechManager.RegisterTech(multiTarget);
            TechManager.RegisterTech(singleTarget);
        }
    }
}