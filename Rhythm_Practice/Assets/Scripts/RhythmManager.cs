/* RhythmManager.cs
 * 
 * The script for changing BPM, gain, and tempo to manage the rhythm.
 * 
 */

using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmManager : SingleTonMonoBehaviour<RhythmManager>
{
    private double bpm = 120.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    public int beat = 4;

    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    // Count variable for first 3~4 click sounds
    private int firstClickCount = 0;

    public event Action OnClick = () => { };
    private void OnEnable()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = 44100;
        nextTick = startTick * sampleRate;
        running = true;

        firstClickCount = 0;
    }

    private double currentTime;
    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }

                if(firstClickCount < signatureHi + 1)
                {
                    firstClickCount += 1;
                }

                OnClick();
                Debug.Log("Tick: " + accent + "/" + signatureHi);

                Debug.Log(AudioSettings.dspTime - currentTime);
                currentTime = AudioSettings.dspTime;
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }

    public double GetBPM()
    {
        return bpm;
    }

    public void SetBPM(double _bpm)
    {
        bpm = _bpm;
    }

    // Method for playing first special click sounds
    public int GetFirstClickCount()
    {
        return firstClickCount;
    }

    // Method for knowing the place to change the notes/notes backgrounds
    public int GetCurrentAccent()
    {
        if(accent % beat == 0)
        {
            return beat;
        }
        else
        {
            return accent % 4;
        }
    }

    public void SetBeat(int _beat)
    {
        beat = _beat;
        signatureHi = _beat;
    }
}