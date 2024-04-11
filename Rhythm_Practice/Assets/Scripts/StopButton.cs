using UnityEngine;
using UnityEngine.UI;

public class StopButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            button.onClick.Invoke();
        }
    }
}
