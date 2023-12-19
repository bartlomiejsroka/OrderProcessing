namespace OrderProcessing.WebApi.Enums
{
    public enum RowType
    {
        Document = 'H',
        Article = 'B',
        Comment = 'C'
    }

    internal static class RowTypeExtension
    {
        public static RowType ToRowType(this char firstChar)
        => firstChar switch
        {
            (char)RowType.Document => RowType.Document,
            (char)RowType.Comment => RowType.Comment,
            (char)RowType.Article => RowType.Article,
            _ => throw new ArgumentOutOfRangeException($"'{firstChar}' is not valid start line marker")
        };
    }
}
