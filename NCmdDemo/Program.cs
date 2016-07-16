using System;
using NCmd;

namespace NCmdDemo
{
    internal class Program
    {
        // setup the command line argument parser.
        static ArgumentParser ArgParse = new ArgumentParser () {
            { "h|?|help", "Display usage message and exit.", v => ShowHelp () },
            { "v|version", "Display version information.", v=>ShowVersion() }
        };

        static void Main(string[] args)
        {
            ArgParse.Parse (args);
            var ds = new DemoShell();
            ds.CmdLoop();
        }

        private static void ShowHelp ()
        {
            ArgParse.WriteArgumentDescriptions (Console.Out);
            Environment.Exit (0);
        }

        private static void ShowVersion ()
        {
            Console.WriteLine ("NCmdDemo v 1.0");
            Environment.Exit (0);
        }
    }
}
