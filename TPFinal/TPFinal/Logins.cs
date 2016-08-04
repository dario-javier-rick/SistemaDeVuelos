using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TPFinal
{
    class Logins
    {
        List<string> logins = new List<string>();
        string path;


        public Logins(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = sr.ReadToEnd();
                    for (int i = 0; i < s.Length - 3; i++)
                    {
                        string login = "";
                        while (Convert.ToString(s[i]) != "\n")
                        {
                            login = login + Convert.ToString(s[i]);
                            i++;
                        }
                        logins.Add(login);
                    }
                }
            }
            this.path = path;
        }


        internal void agregarLogInicio(string usuario, DateTime inicio)  
        {
            logins.Add(usuario + ";" + inicio + ";");
            guardar();
        }

        internal void agregarLogSalida(DateTime salida)  
        {
            int cantidad = logins.Count;
            string s = logins[cantidad-1] + Convert.ToString(salida);
            logins[cantidad-1] = s;
            guardar();
        }


        internal void agregarLog(string usuario, DateTime inicio, DateTime salida)  
        {
            logins.Add(usuario + ";" + inicio + ";" + salida + "\n");
            guardar();
        }

        private void guardar()
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int i = 0; i < logins.Count; i++)
                {
                    sw.WriteLine(logins[i]);
                }
            }
        }
    }

}
