namespace OrderProcessing.Tests.Shared.Extension
{
    public static class DecimalExtension
    {
        public static string ToStringWithDot(this decimal value)
            => value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
}
