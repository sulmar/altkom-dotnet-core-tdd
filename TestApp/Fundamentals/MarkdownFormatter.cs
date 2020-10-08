using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    // Markdown syntax
    // https://www.markdownguide.org/basic-syntax/

    public class MarkdownFormatter
    {
        public string FormatAsBold(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(content))
                throw new ArgumentException(nameof(content));

            Thread.Sleep(TimeSpan.FromSeconds(0.07));

            return $"**{content}**";
        }

        public Task<string> FormatAsBoldAsync(string content)
        {
            return Task.FromResult(FormatAsBold(content));
        }
    }
}
