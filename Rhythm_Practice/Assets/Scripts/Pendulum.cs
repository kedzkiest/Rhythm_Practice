using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pendulum : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float maxLeftTiltAngle;

    [SerializeField]
    private float maxRightTiltAngle;

    private enum Mode
    {
        ROT_Y,
        ROT_Z,
    };
    [SerializeField]
    private Mode mode;

    private enum state
    {
        GO_LEFT,
        GO_RIGHT
    };

    private float stateSwitchTime;
    private state currentState;
    [SerializeField] Ease ease;

    private void Start()
    {
        currentState = state.GO_LEFT;

        RhythmManager.Instance.OnClick += SwitchState;
    }

    private void Update()
    {
        stateSwitchTime = (float)((double)60.0 / RhythmManager.Instance.bpm);

        if (currentState == state.GO_LEFT)
        {
            if (mode == Mode.ROT_Y)
            {
                image.rectTransform.DOLocalRotate(new Vector3(0, 0, maxLeftTiltAngle), stateSwitchTime).SetEase(ease);
            }
            else if(mode == Mode.ROT_Z)
            {
                image.rectTransform.DOLocalRotate(new Vector3(0, maxLeftTiltAngle, 0), stateSwitchTime).SetEase(ease);
            }
        }
        else if(currentState == state.GO_RIGHT)
        {
            if (mode == Mode.ROT_Y)
            {
                image.rectTransform.DOLocalRotate(new Vector3(0, 0, -maxRightTiltAngle), stateSwitchTime).SetEase(ease);
            }
            else if(mode == Mode.ROT_Z)
            {
                image.rectTransform.DOLocalRotate(new Vector3(0, -maxRightTiltAngle, 0), stateSwitchTime).SetEase(ease);
            }
        }
    }

    public void SwitchState()
    {
        if (currentState == state.GO_LEFT)
        {
            currentState = state.GO_RIGHT;
        }
        else if (currentState == state.GO_RIGHT)
        {
            currentState = state.GO_LEFT;
        }

        Debug.Log("Switched");
    }

    public void Stop()
    {

    }
}
