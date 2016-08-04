using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TPFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(95, 30); // Seteo este tamaño de ventana para que muestre los datos de manera prolija
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Logins lista = new Logins(@"C:\Users\AlienDJR\Downloads\TPFinal - Darío Rick\TPFinal\Logins.txt");
            bienvenida();
            Console.Clear();
            DateTime inicio = DateTime.Now;
            Console.Write("Ingrese usuario: ");
            string usuario = Console.ReadLine();
            if (solicitarLogin(usuario))
            {
                lista.agregarLogInicio(usuario, DateTime.Now);
                while (true)
                {
                    Console.Clear();
                    mostrarOpciones();
                    try
                    {
                        int x = Convert.ToInt32(Console.ReadLine());
                        elegirOpciones(x,lista,inicio,usuario);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No ha ingresado una opción numérica!");
                        Thread.Sleep(1000);
                    }
                }
            }
            else
            {
                Console.WriteLine("Ha ingresado mal su usuario o contraseña! El programa se cerrará ahora.");
                Thread.Sleep(1000);
            }
        }




        #region Interfaz de usuario
        private static void bienvenida()
        {
            string s = "Bienvenido al sistema de vuelos!";
            string x = "Hecho por Darío Rick. Academia .NET C#";
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                Thread.Sleep(60);
            }
            Console.SetCursorPosition(3, 3);
            for (int i = 0; i < x.Length; i++)
            {
                Console.Write(x[i]);
                Thread.Sleep(60);
            }
            Thread.Sleep(1500);
        }
        private static void mostrarOpciones()
        {
            Console.WriteLine("Sistema de vuelos V1.0");
            Console.WriteLine("1 - ABM Aerolíneas");
            Console.WriteLine("2 - ABM Conexiones");
            Console.WriteLine("3 - ABM Vuelos");
            Console.WriteLine("4 - Reporte");
            Console.WriteLine("5 - Salir");
            Console.Write("Elija opción: ");
        }
        private static void elegirOpciones(int x, Logins lista, DateTime inicio,string usuario)
        {
            switch (x)
            {
                case 1:
                    Aerolíneas.mostrarABM(lista, inicio, usuario);
                    break;
                case 2:
                    Conexiones.mostrarABM(lista, inicio, usuario);
                    break;
                case 3:
                    Vuelos.mostrarABM(lista, inicio, usuario);
                    break;
                case 4:
                    reporteTema2();
                    break;
                case 5:
                    Console.WriteLine("Gracias por usar mi programa! Dario Rick");
                    lista.agregarLogSalida(DateTime.Now);
                    Thread.Sleep(2000);
                    Environment.Exit(-1);
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    Thread.Sleep(1000);
                    break;
            }
        }
        #endregion

        private static void reporteTema2()
        {
            Console.Clear();
            ConexionBBDD con = new ConexionBBDD();
            //El usuario ingresa un Código de Aerolínea, un Código de Conexión y un rango de puntuaciones 
            Console.Write("Ingrese un código de aerolínea: ");
            string codaero = Console.ReadLine();
            Console.Write("Ingrese un código de conexión: ");
            string codconexion = Console.ReadLine();
            Console.WriteLine("Ingrese un rango de puntuaciones: ");
            Console.Write("Entre: ");
            string puntuaciones1 = Console.ReadLine();
            Console.Write("Y entre: ");
            string puntuaciones2 = Console.ReadLine();
            string consulta2 = con.realizarConsulta("SELECT A.Descripción,A.Teléfono,A.url,C.cod_conexion,C.ciudad_origen,C.ciudad_destino,C.pais_origen,C.pais_destino,C.aeropuerto_origen,C.aeropuerto_destino FROM Vuelos V JOIN Aerolíneas A ON V.cod_aerolinea = A.cod_aerolinea JOIN Conexiones C ON C.cod_conexion = V.cod_conexion WHERE V.cod_aerolinea = " + codaero + " AND C.cod_conexion = " + codconexion + " AND V.Puntaje BETWEEN " + puntuaciones1 + " AND " + puntuaciones2 + "", 10, true);
            Console.WriteLine(con.realizarConsulta("SELECT Descripción,Teléfono,url FROM Aerolíneas WHERE cod_aerolinea = "+codaero+"", 3, false)+"\n");
            Console.WriteLine(con.realizarConsulta("SELECT cod_conexion,ciudad_origen,ciudad_destino,pais_origen,pais_destino,aeropuerto_origen,aeropuerto_destino FROM Conexiones WHERE cod_conexion = " + codconexion + "", 7, false) + "\n");
            string consulta = con.realizarConsulta("SELECT V.Fecha,V.Precio,V.Moneda,V.Puntaje FROM Vuelos V JOIN Aerolíneas A ON V.cod_aerolinea = A.cod_aerolinea JOIN Conexiones C ON C.cod_conexion = V.cod_conexion WHERE V.cod_aerolinea = " + codaero + " AND C.cod_conexion = " + codconexion + " AND V.Puntaje BETWEEN " + puntuaciones1 + " AND " + puntuaciones2 + "", 4, true);
            Console.WriteLine("     Fecha               -   Precio  - Moneda -  Puntaje");
            Console.WriteLine(consulta);            
            Console.WriteLine("\nPresione una tecla para volver al menú anterior");
            Console.ReadKey();

        }
        private static bool solicitarLogin(string usuario)
        {
            ConexionBBDD bbdd = new ConexionBBDD();
            var consulta = bbdd.realizarConsulta("SELECT contrasenia FROM Usuarios WHERE usuario = '" + usuario + "'", 1, false);
            if (consulta != null)
            {
                Console.Write("Ingrese contraseña: ");
                string contraseña = Console.ReadLine();
                string contra = "";
                for (int i = 1; i < consulta.Length - 3; i++)
                {
                    contra = contra + consulta[i];
                }
                if (contra == contraseña)
                {
                    return true;
                }
            }
            return false;
        }



    }

}
