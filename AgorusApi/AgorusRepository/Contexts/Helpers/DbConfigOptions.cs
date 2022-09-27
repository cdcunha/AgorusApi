using Microsoft.Extensions.Options;

namespace AgorusApi.Context.Helper
{
    public class DbConfigOptions
    {
        public const string DbConfig = "DbConfig";

        public string FileName { get; set; } = string.Empty;
    }
}
