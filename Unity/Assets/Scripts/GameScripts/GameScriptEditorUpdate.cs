using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts
{
    [ExecuteInEditMode]
    public sealed class GameScriptEditorUpdate : MonoBehaviour 
    {
#if UNITY_EDITOR
        void Update()
        {
            GetComponents<GameScript>().ToList().
                ForEach(s => s.EditorUpdate());
        }
#endif
    }
}
