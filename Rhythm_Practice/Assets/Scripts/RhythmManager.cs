/* RhythmManager.cs
 * 
 * The script for changing BPM, gain, and tempo to manage the rhythm.
 * 
 */

using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmManager : SingleTonMonoBehaviour<RhythmManager>
{
    public double bpm = 140.0F;
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

    private void OnEnable()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;

        firstClickCount = 0;
    }

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

                if(firstClickCount < signatureHi+1)
                {
                    firstClickCount += 1;
                }

                Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
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
}