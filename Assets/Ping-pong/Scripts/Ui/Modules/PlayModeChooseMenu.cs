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
            SceneManager.LoadScene("Game with bot");
        }
    }
}