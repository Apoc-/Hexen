namespace Systems.GameSystem
{
    class SceneNameProvider
    {
        public static string GetNameFromScene(HexenScene scene)
        {
            switch (scene)
            {
                case HexenScene.GameScene:
                    return "GameScene";
                case HexenScene.StartMenuScene:
                    return "StartMenuScene";
                default:
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
                    return HexenScene.StartMenuScene;
            }
        }
    }
}
