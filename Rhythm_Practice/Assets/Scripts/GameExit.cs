/* GameExit.cs
 * 
 * The script for terminating this game (since currently this game is built for Windows/MacOS)
 * Without game terminating functionality in game, users have to use Alt/Command+F4
 */

using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
