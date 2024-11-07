namespace MvvmPresentation.App.Console
{
    internal class CommandEventArgs : EventArgs
    {
        public char Command { get; }

        public CommandEventArgs(char command)
        {
            Command = command;
        }
    }
}