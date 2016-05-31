using System;
using System.Reflection;

namespace NCmd
{

    /// <summary>
    /// A command is operation that is executed at the request of the user. 
    /// The request is normally the user typing the name of the command at the 
    /// shell prompt.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The name of the command. This is what the user types at the prompt
        /// to initiate command execution.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Some useful text that helps the user execute the command properly.
        /// </summary>
        string HelpText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        void Execute(string arg);
        
    }

    public class AutoCommand : ICommand
    {

        private readonly MethodInfo _method;
        private readonly object _parent;

        public string CommandName { get; set; }
        public string HelpText { get; set; }

        public AutoCommand(object obj, MethodInfo info, string command, string help="")
        {            
            _parent = obj;
            _method = info;
            HelpText = help;
            CommandName = command;
        }

        public void Execute(string arg)
        {
            _method.Invoke(_parent, new object[]{arg});
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<string> _execute;

        public string CommandName { get; set; }
        public string HelpText { get; set; }

        public RelayCommand(Action<string> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            this._execute = execute;
        }
        public void Execute(string arg)
        {
            _execute(arg);
        }
    }
}
