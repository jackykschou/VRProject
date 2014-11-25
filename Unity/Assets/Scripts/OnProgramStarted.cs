using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnProgramStarted : MonoBehaviour
    {
        private const int GameLevelSceneIndex = 1;

        void Awake()
        {
            StartCoroutine(LoadStartGameLevel());
        }

        IEnumerator LoadStartGameLevel()
        {
            yield return new WaitForSeconds(0.5f);
            Application.LoadLevel(GameLevelSceneIndex);
        }
    }
}
