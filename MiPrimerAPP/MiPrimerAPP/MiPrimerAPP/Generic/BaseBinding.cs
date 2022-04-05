using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiPrimerAPP.Generic
{
    public class BaseBinding : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		//aplicamos la libreria para hacer mas resumido de los que son de la propiedad INotifyPropertyChanged
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return;
			field = value;
			OnPropertyChanged(propertyName);

		}
	}
}
