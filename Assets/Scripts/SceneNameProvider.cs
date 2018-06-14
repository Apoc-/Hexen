using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;

namespace Hexen
{
    class SceneNameProvider
    {
        public static string GetNameFromScene(HexenScene scene)
        {
            switch (scene)
            {
                case HexenScene.GameScene:
                    return "GameScene";
                default:
                case HexenScene.StartMenuScene:
                    return "StartMenuScene";
            }
        }

        public static HexenScene GetSceneFromName(string sceneName)
        {
            switch (sceneName)
            {
                case "GameScene":
                    return HexenScene.GameScene;
                default:
                case "StartMenuScene":
                    return HexenScene.StartMenuScene;
            }
        }
    }
}
