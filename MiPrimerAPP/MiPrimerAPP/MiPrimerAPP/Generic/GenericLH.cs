using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Xamarin.Forms;
using MiPrimerAPP.Clases;
using System.Text.RegularExpressions;
using Plugin.Media.Abstractions;
using System.IO;
using System.Linq;

namespace MiPrimerAPP.Generic
{
    public class GenericLH

    {

        public static View buscarHermano(View oControl,string classId)
        {
            //Stepper objStepper = (Stepper)s;
            StackLayout cont = (StackLayout)oControl.Parent;
            List<View> list = cont.Children.ToList();
            foreach (View o in list)
            {
                if (o.ClassId == classId)
                {
                    return o;


                }
            }
            return null;
        }

        public static ImageSource convertirBase64aImageSource(string base64)
        {
            byte[] buffer = Convert.FromBase64String(base64);
            //MemoryStream ms = new MemoryStream(buffer);
            return ImageSource.FromStream(() => new MemoryStream(buffer));
        }

        public static ImageSource convertirArrayDeBytesAImagenSource(byte[] arrayByte)
        {
            //byte[] buffer = Convert.FromBase64String(base64);
            //MemoryStream ms = new MemoryStream(arrayByte);
            return ImageSource.FromStream(() => new MemoryStream(arrayByte));
        }

        public static String convertirMediaFileABase64(MediaFile media)
        {
            Stream s = media.GetStream();
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                byte[] buffer = ms.ToArray();
                return Convert.ToBase64String(buffer);
            }
        }

        public static Byte[] convertirMediaFileAArrayDeBytes(MediaFile media)
        {
            Stream s = media.GetStream();
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                byte[] buffer = ms.ToArray();
                return buffer;
            }
        }

        public static ImageSource convertirMediaFileAImageSource(MediaFile media)
        {
            return ImageSource.FromStream(() => { return media.GetStream(); });
        }
        public static void limpiarTexto(StackLayout limpiar,string classId = "limpiar")
        {
            string nombre;
            foreach (View oView in limpiar.Children)
            {
                if (oView.ClassId != null && oView.ClassId.Contains(classId))
                {
                    nombre = oView.GetType().Name;
                    if (nombre.Equals("Entry")) ((Entry)oView).ClearValue(Xamarin.Forms.Entry.TextProperty);
                    else if (nombre.Equals("Editor")) ((Editor)oView).ClearValue(Xamarin.Forms.Editor.TextProperty);
                    else if (nombre.Equals("Picker")) ((Picker)oView).SelectedIndex = 0;
                    else if (nombre.Equals("Image")) ((Image)oView).ClearValue(Xamarin.Forms.Image.SourceProperty); 
                }
            }
        }
        public static ErrorCLS ObligatorioTexto(StackLayout oStackLayout, string claseId = "obligatorio")
        {
            ErrorCLS oErrorCLS = new ErrorCLS();
            oErrorCLS.exito = true;
            oErrorCLS.mensaje = null;
            string nombre;
            string valor = "";
            var nombreclase = "";
            foreach (View oview in oStackLayout.Children)
            {
                if (oview.ClassId != null && oview.ClassId.Contains(claseId))
                {
                    nombre = oview.GetType().Name;
                    //Aca vemos si es un Entry o Editor(Sacamos el valor y nombre clase)
                    if (nombre.Equals("Entry"))
                    {
                        valor = ((Entry)oview).Text;
                        nombreclase = ((Entry)oview).@class == null ? "" : ((Entry)oview).@class[0];
                    }
                    else if (nombre.Equals("Editor"))
                    {
                        valor = ((Editor)oview).Text;
                        nombreclase = ((Editor)oview).@class == null ? "" : ((Editor)oview).@class[0];
                    }
                    if (nombre.Equals("Entry") || nombre.Equals("Editor"))
                    {
                        //Validamos
                        if (valor == null || valor == "")
                        {

                            oErrorCLS.mensaje = "Debe ingresar " + nombreclase;
                            oErrorCLS.exito = false; break;
                        }
                        else
                        {
                            if (oview.ClassId.Contains("sle"))
                            {
                                if (!Regex.Match(valor, "^[a-zA-Z]+$").Success)
                                {
                                    oErrorCLS.mensaje = "Solo Debe ingresar letras en el campo " + nombreclase;
                                    oErrorCLS.exito = false; break;
                                }
                            }
                            else if (oview.ClassId.Contains("slcenb"))
                            {
                                if (!Regex.Match(valor, "^[a-zA-Z ]+$").Success)
                                {
                                    oErrorCLS.mensaje = "Solo Debe ingresar letras o espacios en blanco en el campo  " + nombreclase;
                                    oErrorCLS.exito = false; break;
                                }
                            }
                            else if (oview.ClassId.Contains("snu"))
                            {
                                if (!Regex.Match(valor, "^[0-9]+$").Success)
                                {
                                    oErrorCLS.mensaje = "Solo Debe ingresar nùmeros en el campo  " + nombreclase;
                                    oErrorCLS.exito = false; break;
                                }
                            }
                        }

                    }
                    else if (nombre.Equals("Picker"))
                    {
                        if (((Picker)oview).SelectedIndex == 0)
                        {
                            oErrorCLS.mensaje = "Debe Seleccionar " + ((Picker)oview).@class[0];
                            oErrorCLS.exito = false; break;
                        }

                    }
                    else if (nombre.Equals("Image"))
                    {
                        if (((Image)oview).Source == null)
                        {
                            oErrorCLS.mensaje = "Debe Seleccionar " + ((Image)oview).@class[0];
                            oErrorCLS.exito = false; break;
                        }

                    }
                }
            }
            return oErrorCLS;
        }

       


        public static string cifrarCadena(string cadena)
        {
            SHA256Managed sha1 = new SHA256Managed();
            byte[] bytecadena = Encoding.Default.GetBytes(cadena);
            byte[] bytecifrado = sha1.ComputeHash(bytecadena);
            return BitConverter.ToString(bytecifrado).Replace("-", "");
        }


        //guardar categoria
        public static async Task<int> Post<T>(string url, T model)
        {
            HttpClient cliente = new HttpClient();
            var cara = JsonConvert.SerializeObject(model);
            var cuerpo = new StringContent(cara, Encoding.UTF8, "application/json");
            var responsive = await cliente.PostAsync(url, cuerpo);
            if (!responsive.IsSuccessStatusCode) return 0;
            else
            {
                int res = int.Parse(await responsive.Content.ReadAsStringAsync());
                return res;

            }
        }









        //eliminar categoria
        public static async Task<int> Delete(string url)
        {
            HttpClient cliente = new HttpClient();
            var rta = await cliente.DeleteAsync(url);
            if (!rta.IsSuccessStatusCode) return 0;
            else
            {
                var result = await rta.Content.ReadAsStringAsync();
                return int.Parse(result);


            }
        }

        //retorna la data
        //Listar categoria
        public static async Task<List<T>> GetAll<T>(string url)
        {
            HttpClient cliente = new HttpClient();
            var rta = await cliente.GetAsync(url);
            if (!rta.IsSuccessStatusCode) return new List<T>();
            else
            {
                var result = await rta.Content.ReadAsStringAsync();
                List<T> l = JsonConvert.DeserializeObject<List<T>>(result);
                return l;
            }
        }



        public static async Task<T> Get<T>(string url)
        {
            HttpClient cliente = new HttpClient();
            var rta = await cliente.GetAsync(url);
           
                var result = await rta.Content.ReadAsStringAsync();
                T l = JsonConvert.DeserializeObject<T>(result);
                return l;
            
        }
    }
}
