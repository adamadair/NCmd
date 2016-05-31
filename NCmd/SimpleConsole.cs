using System;
using System.Collections.Generic;
using System.Text;
using C = System.Console;

namespace NCmd
{
    /// <summary>
    /// SimpleConsole provides utility functions for dealing with System.Console
    /// more efficiently. All methods are static.
    /// </summary>
    internal static class SimpleConsole
    {
        public static string GetString(bool cursorVisible)
        {
            var cv = C.CursorVisible;
            C.CursorVisible = cursorVisible;
            var returnValue = C.ReadLine();
            C.CursorVisible = cv;
            return returnValue;
        }

        public static char PromptCharChoice(string prompt, char[] choices)
        {
            var l = new List<char>(choices);
            if (choices.Length <= 0) return char.MinValue;
            C.Write(prompt);
            while (true)
            {
                var key = C.ReadKey(true);
                if (l.Contains(key.KeyChar))
                {
                    return key.KeyChar;
                }
            }
        }

        public static char PromptChar(string prompt)
        {
            C.Write(prompt);
            return C.ReadKey(true).KeyChar;
        }

        public static string PromptUser(string prompt, bool cursorVisible)
        {
            C.Write(prompt);
            return GetString(cursorVisible);
        }

        public static string PromptUser(string prompt)
        {
            return PromptUser(prompt, true);
        }

        /*
         * My Write and Write Line functions
         */
        public static void Wl(string s) { C.WriteLine(s); }
        public static void Wl() { C.WriteLine(); }
        public static void W(string s) { C.Write(s); }

        public static string Rl() { return C.ReadLine(); }

        public static void VersionStatement(ProgramMetaData p)
        {
            var sb = new StringBuilder();
            sb.Append(p.Title);
            sb.Append(" ");
            sb.Append(p.Version);
            sb.Append(Environment.NewLine);
            if (p.Authors != null && p.Authors.Length > 0)
            {
                sb.Append(GetAuthorStatement(p.Authors));
            }
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(p.LicenseStatement);
            sb.Append(Environment.NewLine);
            Wl(sb.ToString());
        }

        private static string GetAuthorStatement(string[] auths)
        {
            var s = "Written by ";
            var len = auths.Length;
            switch (len)
            {
                case 1:
                    s += auths[0];
                    break;
                case 2:
                    s += auths[0] + " and " + auths[1];
                    break;
                default:
                    s += auths[0];
                    for (var i = 0; i < auths.Length - 1; ++i)
                    {
                        s += "," + auths[0];
                    }
                    s += ", and " + auths[auths.Length - 1];
                    break;
            }
            s += ".";
            return s;
        }
    }
}
