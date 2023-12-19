namespace OrderProcessing.WebApi.Extensions
{
    public static class ConvertExtension
    {
        public static bool CanParseToDecimal(this string value)
            => decimal.TryParse(value.Replace(".", ","), out var _);

        public static decimal ParseToDecimal(this string value)
           => decimal.Parse(value.Replace(".", ","));
    }
}
