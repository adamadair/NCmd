using System;
using NCmd;

namespace NCmdDemo
{
    [CmdShell()]
    internal sealed class DemoShell : Cmd
    {
        /// <summary>
        /// help text for the 'date' command.
        /// </summary>
        private const string ShowDateHelpText = "Displays the current date";

        /// <summary>
        /// Help text for the 'time' command. This method shows how to use
        /// the 'CmdCommandHelp' attribute to identify the help text for the
        /// time command.
        /// </summary>
        [CmdCommandHelp("time")]
        public const string ShowTimeHelpText = "Displays the current time";

        /// <summary>
        /// Help_exit shows a method of documenting a command by naming convention.
        /// </summary>
        public const string Help_exit = "Exit the program.";

        [CmdCommand(Command = "date", Description = ShowDateHelpText)]
        public void ShowDate(string arg)
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());    
        }

        /// <summary>
        /// Do_time demonstrates 
        /// </summary>
        /// <param name="arg"></param>
        public void Do_time(string arg)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
        }

        /// <summary>
        /// Providing an explicit exit command is not necessary because
        /// ctrl-D will exit the command shell, but it is good form to provide
        /// a command named 'exit' or 'quit'. All such a method has to do is
        /// call the ExitLoop() method.
        /// </summary>
        /// <param name="arg">Any arguments provided will promptly be ignored.</param>
        public void Do_exit(string arg)
        {
            ExitLoop();
        }

        /// <summary>
        /// Another command by convention, but this one will be undocumented.
        /// </summary>
        /// <param name="arg"></param>
        public void Do_something(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                Console.WriteLine("Doing something...");
            }
            else
            {
                Console.WriteLine($"What do you mean by '{arg}'?");
            }
        }

        public DemoShell()
        {
            // Intro is the text that gets displayed when the shell starts. 
            Intro = "NCmd Demonstration Shell\n========================\n\n";

            // Command prompt is what gets displayed to the user when 
            // prompting for input. This field can acutally be updated
            // dynamically so that the prompt can change over the course
            // of execution.
            CommandPrompt = "> ";

            // Set to save command history for future use set a unique
            // History file name. 
            HistoryFileName = "NCmdDemo";
        }


        public override void PostCmd(string line)
        {
            base.PostCmd(line);
            Console.WriteLine();
        }

        public override string PreCmd(string line)
        {
            Console.WriteLine("PreCmd fired!");
            return base.PreCmd(line);
            
        }

        /// <summary>
        /// EmptyLine can be overriden to handle case where the user has entered 
        /// a blank line. The default case is to do nothing. In this case we 
        /// provide the user with some assistance. Another useful option would be 
        /// to repeat the last command.
        /// </summary>
        public override void EmptyLine()
        {
            Console.WriteLine("Please enter a command or type 'help' for assitance.");
        }

        /// <summary>
        /// PreLoop can be overridden to perfom and sort of remaining 
        /// initialization that is required right before entering the
        /// shell loop. In this case we just inform the user that 
        /// execution is about to enter the shell loop.
        /// </summary>
        public override void PreLoop()
        {
            Console.WriteLine("Entering shell loop.");
        }

        public override void PostLoop()
        {
            Console.WriteLine("Shell loop exited.");
        }
    }
}
