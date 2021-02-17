using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PuttyWrapper
{
    public static class Constants
    {
        public static readonly string DataPath =
            Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PuttyWrapper");
    }
}
