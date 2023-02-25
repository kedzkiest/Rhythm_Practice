/* SingletonMonoBehaviour.cs
 * 
 * This is a base class for RhythmManager to be a singleton.
 */

using UnityEngine;

public class SingleTonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    Debug.LogError(typeof(T) + " is nothing");
                }
            }

            return instance;
        }
    }
}
