/* DialNumber.cs
 * 
 * The script for containing a dial number in setting menu.
 * Also serves the functionality to increment/decrement numbers.
 */

using UnityEngine;
using UnityEngine.UI;

public class DialNumber : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private Text text;
    [SerializeField] private bool isOneDigit;

    // Update is called once per frame
    void Update()
    {
        text.text = number.ToString();
    }

    public void Increment()
    {
        number += 1;

        if (number <= 9) return;

        number = isOneDigit ? 1 : 0;
    }

    public void Decrement()
    {
        number -= 1;

        if (number < 0) number = 9;

        if (isOneDigit && number < 1)
        {
            number = 9;
        }
    }
}
