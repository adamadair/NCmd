using System;

namespace NCmd
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CmdShellAttribute : Attribute
    {

    }

    /// <summary>
    /// CmdCommand attribute can be used to identify methods as 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CmdCommandAttribute : Attribute
    {
        /// <summary>
        /// The command text that is required to be typed by the user to execute
        /// the method the attribute marks. The command parser only looks for the
        /// first word in the input. Setting the command parameter to a multi-word
        /// string would ensure that it never gets executed.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// A text description of the command. This is text will be diplayed 
        /// to the user when using the help command.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field )]
    public class CmdCommandHelpAttribute : Attribute
    {
        public CmdCommandHelpAttribute(string command)
        {
            this.Command = command;
        }

        public string Command { get; }
    }
}