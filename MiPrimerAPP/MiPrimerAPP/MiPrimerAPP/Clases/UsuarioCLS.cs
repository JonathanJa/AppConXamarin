using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MiPrimerAPP.Clases
{
    public class UsuarioCLS:INotifyPropertyChanged
    {
        public int iidusuario { get; set; }
        public int iidtipousuario { get; set; }
        private string _NombreUsuario { get; set; }
        private string _Contra { get; set; }
        public string NombreUsuario { 
            get {

                return _NombreUsuario;
                
            }
            set {
                if(this._NombreUsuario != value)
                {
                    this._NombreUsuario = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.NombreUsuario)));

                }
            }
        }
        public string Contra { 
            get 
            {
                return _Contra;
            }
            set 
            {
                if (this._Contra != value)
                {
                    this._Contra = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Contra)));

                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
