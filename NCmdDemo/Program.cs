using System;
using NCmd;

namespace NCmdDemo
{
    /// <summary>
    /// The main entry point to the NCmd demonstration application. 
    /// Demonstrates setting up program argument parser (ArgParse) and
    /// starting a command shell (demoShell).
    /// </summary>
    internal class Program
    {
        // setup the command line argument parser.
        private static readonly ArgumentParser ArgParse = new ArgumentParser () {
            { "h|?|help", "Display usage message and exit.", v => ShowHelp () },
            { "v|version", "Display version information.", v=>ShowVersion() }
        };

        static void Main(string[] args)
        {
            ArgParse.Parse (args);
            var ds = new DemoShell();
            ds.CmdLoop();
        }

        /// <summary>
        /// Display usage statement and exit program
        /// </summary>
        private static void ShowHelp ()
        {
            var ap = new AutoProgramMetaData(typeof(Program).Assembly);
            Cmd.WriteUsageStatement(ap, ArgParse, Console.Out);
            Environment.Exit (0);
        }

        /// <summary>
        /// Display program version statement and exit program
        /// </summary>
        private static void ShowVersion ()
        {
            var ap = new AutoProgramMetaData(typeof(Program).Assembly);
            Cmd.WriteVersionStatement(ap, Console.Out);
            Environment.Exit (0);
        }
    }
}
