using UnityEditor;

namespace Assets.Scripts.Utility.Editor
{
    public class SceneMenuItem : UnityEditor.Editor
    {
        private const string SceneFolderPath = "Assets/Scenes/";
        private const string SceneFileExtension = ".unity";

        private const string TestSceneFolder = "Tests/";

        [MenuItem("Open Scene/GameStart")]
        public static void OpenGGameStart()
        {
            OpenScene("GameStart");
        }

        [MenuItem("Open Scene/Testing/GameLogicComponentEventTest")]
        public static void OpenGameLogicComponentEventTest()
        {
            OpenScene(TestSceneFolder + "GameLogicComponentEventTest");
        }

        [MenuItem("Open Scene/Jamming/Sprint0")]
        public static void OpenSprint0()
        {
            OpenScene("Sprint0");
        }

        private static void OpenScene(string scenePath)
        {
            if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
            {
                EditorApplication.OpenScene(SceneFolderPath + scenePath + SceneFileExtension);
            }
        }
    }
}
