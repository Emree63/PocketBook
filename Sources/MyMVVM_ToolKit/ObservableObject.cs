using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyMVVM_ToolKit;

public class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual void SetProperty<T>(T member, Action<T> action, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(member, value)) return;
        action(value);
        OnPropertyChanged(propertyName);
    }

    protected virtual void SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(member, value)) return;
        member = value;
        OnPropertyChanged(propertyName);
    }
}

