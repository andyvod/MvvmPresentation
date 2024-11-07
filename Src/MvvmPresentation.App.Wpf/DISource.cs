using System;
using System.Windows.Markup;

namespace MvvmPresentation.App.Wpf
{
    public class DISource : MarkupExtension
    {
        public static Func<Type?, object?>? Resolver { get; set; }

        public Type? Type { get; init; }

        public override object? ProvideValue(IServiceProvider serviceProvider) => Resolver?.Invoke(Type);
    }
}
