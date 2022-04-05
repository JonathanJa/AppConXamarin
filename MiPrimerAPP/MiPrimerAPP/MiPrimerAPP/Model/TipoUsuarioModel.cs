using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MiPrimerAPP.Model
{
    public class TipoUsuarioModel : BaseBinding
    {
        private List<TipoUsuarioCLS> _listaTipoUsuario;
        public List<TipoUsuarioCLS> listaTipoUsuario
        {
            get { return _listaTipoUsuario; }
            set { SetValue(ref _listaTipoUsuario, value); }
        }

        private List<PaginaCLS> _listaPagina;
        public List<PaginaCLS> listaPagina
        {
            get { return _listaPagina; }
            set { SetValue(ref _listaPagina, value); }
        }
        private TipoUsuarioCLS _oTipoUsuarioCLS;
        public TipoUsuarioCLS oTipoUsuarioCLS
        {
            get { return _oTipoUsuarioCLS; }
            set { SetValue(ref _oTipoUsuarioCLS, value); }
        }

    }
}
