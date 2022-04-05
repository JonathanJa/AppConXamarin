using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MiPrimerAPP.Model
{
    public class PersonaModel:BaseBinding
    {
        private bool _IsVisibleButton;
        public bool IsVisibleButton
        {
            get { return _IsVisibleButton; }
            set { SetValue(ref _IsVisibleButton, value); }
        }

        private bool _IsVisibleContra;
        public bool IsVisibleContra
        {
            get { return _IsVisibleContra; }
            set { SetValue(ref _IsVisibleContra, value); }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetValue(ref _IsVisible, value); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        //persona
        private List<PersonaCLS> _listaPersona;
        public List<PersonaCLS> listaPersona
        {
            get { return _listaPersona; }
            set { SetValue(ref _listaPersona, value); }
        }


        private PersonaCLS _oPersonaCLS;
        public PersonaCLS oPersonaCLS
        {
            get { return _oPersonaCLS; }
            set { SetValue(ref _oPersonaCLS, value); }
        }
    }
}
