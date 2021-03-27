using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nookipedia.Net
{
    class NookipediaException : Exception
    {
        public NookipediaException(string message, Exception innerException) : base(message, innerException) { }
    }

    class MissingOptionalException : Exception
    {
        public MissingOptionalException(Exception innerException = null) : base(null, innerException) { }
    }
}
