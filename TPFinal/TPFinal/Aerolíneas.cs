using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TPFinal
{
    class Aerolíneas 
    {
        public static void mostrarABM(Logins lista,DateTime inicio, string usuario)
        {
            Console.Clear();
            Console.WriteLine("Sistema de vuelos V1.0");
            Console.WriteLine("1 - Nueva aerolínea");
            Console.WriteLine("2 - Eliminar aerolínea");
            Console.WriteLine("3 - Modificar aerolínea");
            Console.WriteLine("4 - Mostrar aerolíneas ordenadas por consideración");
            Console.WriteLine("5 - Volver al menú anterior");
            Console.WriteLine("6 - Salir");
            Console.Write("Elija opción: "); 
            try
            {
                int x = Convert.ToInt32(Console.ReadLine());
                elegirOpciones(x,lista,usuario,inicio);
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
                    Console.WriteLine(listarPorConsideracion());
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
            Console.Write("Elija descripción de aerolínea: ");
            string descripción = Console.ReadLine();
            Console.Write("Elija teléfono de aerolínea: ");
            string teléfono = Console.ReadLine();
            Console.Write("Elija url de aerolínea: ");
            string url = Console.ReadLine();
            ConexionBBDD con = new ConexionBBDD();
            con.realizarModificacion("INSERT INTO Aerolíneas VALUES ('" + descripción + "', '" + teléfono + "', '" + url + "',0)");
            //Se pone consideración en 0 ya que al ser una aerolínea nueva, no tiene vuelos registrados en un principio
            Console.WriteLine("Aerolínea agregada!");
            Thread.Sleep(1000);
        }
        private static void darDeBaja()
        {
            Console.Clear();
            Console.WriteLine("Recuerde que eliminando la aerolínea, eliminará en consecuencia las conexiónes y los vuelos asociados! ");
            Console.Write("Elija código de aerolínea: ");
            string s = Console.ReadLine();
            ConexionBBDD c = new ConexionBBDD();
            c.realizarModificacion("DELETE FROM Vuelos WHERE cod_aerolinea = '" + s + "'");
            c.realizarModificacion("DELETE FROM Conexiones WHERE cod_aerolinea = '" + s + "'");
            c.realizarModificacion("DELETE FROM Aerolíneas WHERE cod_aerolinea = '" + s + "'");
            Console.WriteLine("Aerolínea eliminada!");
            Thread.Sleep(1000);
        }
        private static void modificar()
        {
            Console.Clear();
            Console.Write("Elija código de aerolínea para modificarla: ");
            string cod = Console.ReadLine();
            Console.Write("Elija nueva descripción de aerolínea: ");
            string descripción = Console.ReadLine();
            Console.Write("Elija nuevo teléfono de aerolínea: ");
            string teléfono = Console.ReadLine();
            Console.Write("Elija nueva url de aerolínea: ");
            string url = Console.ReadLine();
            ConexionBBDD co = new ConexionBBDD();
            co.realizarModificacion("UPDATE Aerolíneas SET Descripción ='" + descripción + "', Teléfono = '" + teléfono + "', url = '" + url + "'  WHERE cod_aerolinea = " + cod);
            Console.WriteLine("Aerolinea modificada!");
            Thread.Sleep(1000);
        }

        public static string listar()
        {
            ConexionBBDD BBDD = new ConexionBBDD();
            return BBDD.realizarConsulta("Select COLUMN_NAME From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'Aerolíneas' ", 1, false) + "\n\n" + BBDD.realizarConsulta("SELECT * FROM Aerolíneas", 5, true);
        }

        public static string listarPorConsideracion()
        {
            ConexionBBDD BBDD = new ConexionBBDD();
            return BBDD.realizarConsulta("Select COLUMN_NAME From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'Aerolíneas' ", 1, false) + "\n\n" + BBDD.realizarConsulta("SELECT * FROM Aerolíneas ORDER BY Consideracion DESC", 5, true);
        }
    }
}
