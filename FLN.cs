public class FLN
{
    public static object fln(double result)
    {
        if (double.IsInfinity(result))
        {
            return new LargeNumber(1.0, new LargeNumber(double.MaxValue));
        }

        if (Math.Abs(result) < 1e33)
        {
            if (result == Math.Truncate(result))
            {
                return result.ToString("0");
            }
            return result.ToString("0.###############################");
        }

        double exponent = Math.Log10(Math.Abs(result));
        double mantissa = result / Math.Pow(10, Math.Floor(exponent));
        return new LargeNumber(mantissa, new LargeNumber(Math.Floor(exponent)));
    }

    public static string Format(double result)
    {
        var formatted = fln(result);
        return formatted?.ToString() ?? result.ToString();
    }
}