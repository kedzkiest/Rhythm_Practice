/* DialNumberManager.cs
 * 
 * The script for calculating a dial number in setting menu.
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class DialNumberManager : SingleTonMonoBehaviour<DialNumberManager>
{
    [SerializeField] Text[] dialTexts;

    public int GetDialNumber()
    {
        int ans = 0;

        for(int i = 0; i < dialTexts.Length; i++)
        {
            ans += Int32.Parse(dialTexts[i].text) * (int)Mathf.Pow(10, i);
        }

        return ans;
    }
}
