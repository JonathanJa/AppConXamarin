using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Generic
{
    public static class Settings
    {
       
            private static ISettings AppSettings
            {
                get
                {
                    return CrossSettings.Current;
                }
            }

            #region Setting Constants
            //idUsuario
            private const string SettingsKeyidUsuario = "idusuario";
            private static readonly int SettingsDefaultidUsuario = 0;

             //idTipoUsuario
            private const string SettingsKeyidTipoUsuario = "idTipousuario";
            private static readonly int SettingsDefaultidTipoUsuario = 0;

            //RecordarContra
            private const string SettingsKeyRecordarContra = "RecordarContra";
            private static readonly bool SettingsDefaultRecordarContra = false;


            //Producto
            private const string SettingsKeyProducto = "ProductList";
            private static readonly string SettingsDefaultProducto = "";
        #endregion


        public static int idUsuario
            {
                get
                {
                    return AppSettings.GetValueOrDefault(SettingsKeyidUsuario, SettingsDefaultidUsuario);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(SettingsKeyidUsuario, value);
                }
            }

        public static int idTipoUsuario
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKeyidTipoUsuario, SettingsDefaultidTipoUsuario);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKeyidTipoUsuario, value);
            }
        }
        
        public static bool RecordarContra
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKeyRecordarContra, SettingsDefaultRecordarContra);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKeyRecordarContra, value);
            }
        }


        public static string ProductListAdd
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKeyProducto, SettingsDefaultProducto);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKeyProducto, value);
            }
        }
    }

}
