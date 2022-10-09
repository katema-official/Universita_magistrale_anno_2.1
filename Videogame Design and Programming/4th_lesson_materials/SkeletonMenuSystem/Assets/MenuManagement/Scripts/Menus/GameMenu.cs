using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        public void OnPausePressed()
        {
            Time.timeScale = 0f;
            PauseMenu.Open();
        }
    }
}
