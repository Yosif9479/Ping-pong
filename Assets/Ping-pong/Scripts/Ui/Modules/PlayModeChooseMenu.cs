using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui.Modules
{
    public class PlayModeChooseMenu : MonoBehaviour
    {
        public void ChooseMultiplayer()
        {
            SceneManager.LoadScene("Game");   
        }

        public void ChooseSolo()
        {
            UnityEngine.Debug.LogError("Solo game doesn't implemented yet");
        }
    }
}