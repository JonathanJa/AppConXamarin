using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MiPrimerAPP.Model
{
    public class MenuModel : BaseBinding
    {
        private List<MenuCLS> _listaMenu;
        public List<MenuCLS> listaMenu
        {
            get { return _listaMenu; }
            set { SetValue(ref _listaMenu, value); }
        }

    }
}
