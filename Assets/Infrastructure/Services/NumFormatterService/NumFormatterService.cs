using UnityEngine;

public class NumFormatterService : INumFormatterService
{
    private readonly string[] _numFormatChars = new[]
    {
        "",
        "K",
        "M",
        "B",
        "T",
        "Q",
        "Qt",
        "S"
    };


    public string FormatNum(int num)
    {
        if (num == 0)
            return "0";

        float floatNum = Mathf.Round(num);
        
        int i = 0;
        while (i + 1 < _numFormatChars.Length && floatNum >= 1000f)
        {
            floatNum /= 1000f;
            i++;
        }

        return floatNum.ToString("#.##").Replace(",", ".") + _numFormatChars[i];
    }

    public string FormatNum(float num)
    {
        if (num == 0)
            return "0";

        num = Mathf.Round(num);
        
        int i = 0;
        while (i + 1 < _numFormatChars.Length && num <= 1000f)
        {
            num /= 1000f;
            i++;
        }

        return num.ToString("#.##").Replace(",", ".") + _numFormatChars[i];
    }
}
