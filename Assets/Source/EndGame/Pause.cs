using UnityEngine;

namespace Source.EndGame
{
    public class Pause
    {
        public void SetPause(bool isPause)
        {
            Time.timeScale = isPause ? 0f : 1f;
        }
    }
}
