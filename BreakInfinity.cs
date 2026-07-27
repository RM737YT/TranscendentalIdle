public class LargeNumber
{
    public double Mantissa;
    public LargeNumber Exponent;
    public LargeNumber(double mantissa, LargeNumber exponent = null)
    {
        Mantissa = mantissa;
        Exponent = exponent;
    }
    public override string ToString()
    {
        if (Exponent == null)
        {
            if (Mantissa == Math.Truncate(Mantissa))
            {
                return Mantissa.ToString("0");
            }
            return Mantissa.ToString("0.00");
        }

        string mantissaString = Mantissa.ToString("0.00");
        return mantissaString + "e" + Exponent.ToString();
    }

    // Parse nested exponents (e.g., "1.00e1e100" -> 1.0 with exponent containing nested exponents)
    public static LargeNumber ParseNestedExponent(string input)
    {
        // Find the first 'e' to separate mantissa and exponent part
        int eIndex = input.IndexOf('e');
        
        if (eIndex == -1)
        {
            // No exponent, just a number
            return new LargeNumber(double.Parse(input));
        }

        // Extract mantissa
        double mantissa = double.Parse(input.Substring(0, eIndex));
        
        // Extract exponent string (everything after the 'e')
        string exponentStr = input.Substring(eIndex + 1);
        
        // Recursively parse the exponent part to support nesting
        LargeNumber exponent = ParseNestedExponent(exponentStr);
        
        return new LargeNumber(mantissa, exponent);
    }
}