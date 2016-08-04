using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TPFinal
{
    class Conexiones
    {
        public static void mostrarABM(Logins lista, DateTime inicio, string usuario)
        {
            Console.Clear();
            Console.WriteLine("Sistema de vuelos V1.0");
            Console.WriteLine("1 - Nueva conexión");
            Console.WriteLine("2 - Eliminar conexión");
            Console.WriteLine("3 - Modificar conexión");
            Console.WriteLine("4 - Mostrar conexiónes");
            Console.WriteLine("5 - Volver al menú anterior");
            Console.WriteLine("6 - Salir");
            Console.Write("Elija opción: ");
            try
            {
                int x = Convert.ToInt32(Console.ReadLine());
                elegirOpciones(x, lista, usuario, inicio);
            }
            catch (Exception)
            {
                Console.WriteLine("No ha ingresado una opción numérica!");
                Thread.Sleep(1000);
            }
        }
        private static void elegirOpciones(int x, Logins lista, string usuario, DateTime inicio)
        {
            switch (x)
            {
                case 1:
                    darDeAlta();
                    break;
                case 2:
                    darDeBaja();
                    break;
                case 3:
                    modificar();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine(listar());
                    Console.WriteLine("Presione una tecla para volver al menú principal");
                    Console.ReadKey();
                    break;
                case 5:
                    break;
                case 6:
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
        private static void darDeAlta()
        {
            Console.Clear();
            ConexionBBDD con = new ConexionBBDD();
            Console.Write("Elija código de aerolínea: ");
            string código = Console.ReadLine();
            string chequeaSiExiste = con.realizarConsulta("SELECT Descripción FROM Aerolíneas WHERE cod_aerolinea = " + código, 1, false);
            // Verifico con una consulta si la aerolínea existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
            if (chequeaSiExiste.Length == 1)
            {
                Console.WriteLine("Aerolinea no existe!");
                Console.WriteLine("Presione una tecla para volver al menú principal!");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Elija ciudad de origen: ");
                string ciudad_origen = Console.ReadLine();
                Console.Write("Elija ciudad de destino: ");
                string ciudad_destino = Console.ReadLine();
                Console.Write("Elija país de origen: ");
                string país_origen = Console.ReadLine();
                Console.Write("Elija país de destino: ");
                string país_destino = Console.ReadLine();
                Console.Write("Elija aeropuerto de origen: ");
                string aeropuerto_origen = Console.ReadLine();
                Console.Write("Elija aeropuerto de destino: ");
                string aeropuerto_destino = Console.ReadLine();
                con.realizarModificacion("INSERT INTO Conexiones VALUES (" + código + ",'" + ciudad_origen + "','" + ciudad_destino + "','" + país_origen + "','" + país_destino + "','" + aeropuerto_origen + "','" + aeropuerto_destino + "')");
                Console.WriteLine("Conexión agregada!");
                Thread.Sleep(1000);
            }
        }
        private static void darDeBaja()
        {
            Console.Clear();
            Console.WriteLine("Recuerde que eliminando la conexión, eliminará los vuelos asociados! ");
            Console.Write("Elija código de conexión: ");
            string s = Console.ReadLine();
            ConexionBBDD c = new ConexionBBDD();
            string chequeaSiExiste = c.realizarConsulta("SELECT cod_aerolinea FROM Conexiones WHERE cod_conexion = " + s, 1, false);
            // Verifico con una consulta si la conexión existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
            if (chequeaSiExiste.Length == 1)
            {
                Console.WriteLine("Conexión no existe!");
                Console.WriteLine("Presione una tecla para volver al menú principal!");
                Console.ReadKey();
            }
            else
            {
                c.realizarModificacion("DELETE FROM Vuelos WHERE cod_conexion = '" + s + "'");
                c.realizarModificacion("DELETE FROM Conexiones WHERE cod_conexion = '" + s + "'");
                Console.WriteLine("Conexión eliminada!");
                Thread.Sleep(1000);
            }

        }
        private static void modificar()
        {
            Console.Clear();
            ConexionBBDD co = new ConexionBBDD();
            Console.Write("Elija código de conexión para modificarla: ");
            string cod_conexion = Console.ReadLine();

            string chequeaSiExiste = co.realizarConsulta("SELECT cod_aerolinea FROM Conexiones WHERE cod_conexion = " + cod_conexion, 1, false);
            // Verifico con una consulta si la conexión existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
            if (chequeaSiExiste.Length == 1)
            {
                Console.WriteLine("Conexión no existe!");
                Console.WriteLine("Presione una tecla para volver al menú principal!");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Elija nuevo código de aerolínea: ");
                string cod_aerolinea = Console.ReadLine();
                Console.Write("Elija nueva ciudad de origen: ");
                string ciudad_origen = Console.ReadLine();
                Console.Write("Elija nueva ciudad de destino: ");
                string ciudad_destino = Console.ReadLine();
                Console.Write("Elija nuevo país de origen: ");
                string pais_origen = Console.ReadLine();
                Console.Write("Elija nuevo país de destino: ");
                string pais_destino = Console.ReadLine();
                Console.Write("Elija nuevo aeropuerto de origen: ");
                string aeropuerto_origen = Console.ReadLine();
                Console.Write("Elija nueva aeropuerto de destino: ");
                string aeropuerto_destino = Console.ReadLine();

                co.realizarModificacion("UPDATE Conexiones SET cod_aerolinea = " + cod_aerolinea + ", ciudad_origen='" + ciudad_origen + "', ciudad_destino='" + ciudad_destino + "',pais_origen='" + pais_origen + "',pais_destino='" + pais_destino + "',aeropuerto_origen='" + aeropuerto_origen + "',aeropuerto_destino='" + aeropuerto_destino + "' WHERE cod_conexion = " + cod_conexion);
                Console.WriteLine("Conexión modificada!");
                Thread.Sleep(1000);
            }
        }

        public static string listar()
        {
            ConexionBBDD BBDD = new ConexionBBDD();
            return BBDD.realizarConsulta("Select COLUMN_NAME From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'Conexiones' ", 1, false) + "\n\n" + BBDD.realizarConsulta("SELECT * FROM Conexiones", 8, true);
        }
    }
}
