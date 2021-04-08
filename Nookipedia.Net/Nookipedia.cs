using System;

namespace Nookipedia.Net
{
    public static class NookipediaConstants
    {
        public static readonly Version NookipediaAPIVersion = new Version(1, 3, 0);
        public static readonly Version NookipediaNetVersion = new Version(NookipediaAPIVersion.Major, NookipediaAPIVersion.Minor, NookipediaAPIVersion.Build, 4);
    }
}
