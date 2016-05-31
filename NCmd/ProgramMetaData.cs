namespace NCmd
{
    public class ProgramMetaData
    {
        public const string DEFAULT_LICENSE_STATEMENT =
@"Copyright (C) {0} {1}
This is free software; see the source for copying conditions.  There is NO
warranty; not even for MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
";

        public string Title { get; set; }
        public string[] Authors { get; set; }

        public string Version { get; set; }
        public string CopyrightYear { get; set; }

        public string LicenseStatement { get; set; }

    }
}
