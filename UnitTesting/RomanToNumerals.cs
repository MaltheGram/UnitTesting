namespace TestSuite;

/**
 I          1
 V          5
 X         10
 L         50
 C        100
 D        500
 M       1000
 */
public class RomanToNumerals
{

    Dictionary < char, int > romanValues = new() {
        { 'I', 1 },
        { 'V',5 }, 
        { 'X',10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 }
    };
 
    public int Convert(string roman)
    {
        int result = 0;
        int prevValue = 0;

        for (int i = roman.Length - 1; i >= 0; i--)
        {
            int currentValue = romanValues[roman[i]];

            if (currentValue >= prevValue)
                result += currentValue;
            else
                result -= currentValue;

            prevValue = currentValue;
        }

        return result;
    }
}