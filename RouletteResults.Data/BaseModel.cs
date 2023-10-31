using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RouletteResults.Data
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string prop) => PropertyChanged(this, new PropertyChangedEventArgs(prop));

        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string name = null)
        {
            if (Equals(member, val)) return;
            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected static string ThrowNullEx(string msg) => throw new ArgumentNullException(msg);
    }
}
