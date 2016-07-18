using System;
using System.Reflection;
using System.IO;

namespace NCmd
{
    /// <summary>
    /// IProgramMetaData is an interface for a pure data object
    /// whose member attributes contain descriptive information
    /// about a program. 
    /// </summary>
    public interface IProgramMetaData
    {
        string Title { get; set; }
        string Description { get; set; }        
        string Version { get; set; }
        DateTime? BuildDateTime { get; set; }
        string Copyright { get; set; }
        string LicenseStatement { get; set; }
    }

    /// <summary>
    /// A default implementation of IProgramMetaData that can
    /// be used for any reason. The gotcha here is that the 
    /// BuildDateTime by default is null, so be careful when
    /// using this.
    /// </summary>
    public class ProgramMetaData : IProgramMetaData
    {
        public string Title { get; set; }
        public string Description { get; set; }        
        public string Version { get; set; }
        public string Copyright { get; set; }
        public string LicenseStatement { get; set; }
        public DateTime? BuildDateTime { get; set; }
    }

    /// <summary>
    /// AutoProgramMetaData takes an assembly and tries to use reflection
    /// to fill out as many of the IProgramMetaData members as possible. 
    /// </summary>
    public class AutoProgramMetaData : ProgramMetaData
    {
        public const string DefaultLicenseStatement = "{0} \n" +
            "This is free software; see the source for copying conditions.  There is NO \n" +
            "warranty; not even for MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.";

        private Assembly assembly;

        public AutoProgramMetaData(Assembly a)
        {
            assembly = a;            
            this.Version = GetVersion();
            this.Title = GetTitle();
            this.Description = GetDescription();
            this.Copyright = GetCopyright();
            this.LicenseStatement = GetLicenseStatement();
            this.BuildDateTime = GetLinkerTime(TimeZoneInfo.Local);
        }

        #region Reflection methods for getting information about assembly
        private string GetVersion()
        {
            return assembly.GetName().Version.ToString();
        }

        private string GetTitle()
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length <= 0)
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            var titleAttribute = (AssemblyTitleAttribute) attributes[0];
            return titleAttribute.Title.Length > 0 ? titleAttribute.Title : System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }

        private string GetDescription()
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        private string GetCopyright()
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

        }

        private string GetLicenseStatement()
        {
            return string.Format(DefaultLicenseStatement, GetCopyright());
        }

        private DateTime? GetLinkerTime(TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int cPeHeaderOffset = 60;
            const int cLinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, cPeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + cLinkerTimestampOffset);
            if (secondsSince1970 == 0) return null;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }
        #endregion
    }
}
