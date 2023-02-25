using System.Collections;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private AudioSource _ring;
    [SerializeField] private double _bpm = 140d;

    private double _metronomeStartDspTime;
    private double _buffer = 2 / 60d;

    private int cnt = 0;
    private bool isRingOnce;

    private void Start()
    {
        _metronomeStartDspTime = AudioSettings.dspTime;
    }

    private void FixedUpdate()
    {
        double nxtRng = NextRingTime();

        if (nxtRng < AudioSettings.dspTime + _buffer)
        {
            _ring.PlayScheduled(nxtRng);
        }
    }

    private double NextRingTime()
    {
        double beatInterval = 60d / _bpm;
        double elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        double beats = System.Math.Floor(elapsedDspTime / beatInterval);

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
}
