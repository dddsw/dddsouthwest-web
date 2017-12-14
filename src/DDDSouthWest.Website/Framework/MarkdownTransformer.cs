using CommonMark;

namespace DDDSouthWest.Website.Framework
{
    public class MarkdownTransformer
    {
        public string ToHtml(string markdown)
        {
            return CommonMarkConverter.Convert(markdown);
        }
    }
}