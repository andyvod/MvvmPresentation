using System.ComponentModel;

namespace MvvmPresentation.App.Console
{
    internal abstract class BaseView
    {
        protected INotifyPropertyChanged ViewModelINotify;

        public delegate void CommandEventHandler(object? sender, CommandEventArgs e);
        protected event CommandEventHandler? CommandEvent;

        public delegate void ViewEventHandler(object? sender, EventArgs e);
        protected event ViewEventHandler? ViewLoaded;

        public BaseView(object viewModel)
        {
            if (viewModel is not INotifyPropertyChanged vm)
            {
                throw new InvalidOperationException("Некорректный тип модели представления!");
            }

            ViewModelINotify = vm;            
        }

        public virtual void Show() {
            DisplayHelp();
            SetBindings();
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void DisplayHelp();
        protected abstract void SetBindings();

        protected void SetTrigger(string propertyName, Action action)
        {
            ViewModelINotify.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != propertyName)
                    return;

                action();
            };
        }

        protected void Display(string line)
        {
            System.Console.WriteLine(line);
        }

        protected void Display(string[] lines)
        {
            foreach (var line in lines)
            {
                System.Console.WriteLine(line);
            }
        }

        protected void Display<TRow>(List<TRow> data) where TRow : class
        {
            System.Console.WriteLine($"Печать грида: {typeof(TRow)}");
        }

        protected void OnCommandEntered(char command)
        {
            CommandEvent?.Invoke(this, new CommandEventArgs(command));
        }
    }
}