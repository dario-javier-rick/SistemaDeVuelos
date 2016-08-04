using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TPFinal
{
    class Vuelos
    {
        public static void mostrarABM(Logins lista, DateTime inicio, string usuario)
        {
            Console.Clear();
            Console.WriteLine("Sistema de vuelos V1.0");
            Console.WriteLine("1 - Nuevo vuelo");
            Console.WriteLine("2 - Eliminar vuelo");
            Console.WriteLine("3 - Modificar vuelo");
            Console.WriteLine("4 - Mostrar vuelos");
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
            string cod_aerolinea = Console.ReadLine();
            string chequeaSiExiste = con.realizarConsulta("SELECT Descripción FROM Aerolíneas WHERE cod_aerolinea = " + cod_aerolinea, 1, false);
            // Verifico con una consulta si la aerolínea existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
            if (chequeaSiExiste.Length == 1)
            {
                Console.WriteLine("Aerolinea no existe!");
                Console.WriteLine("Presione una tecla para volver al menú principal!");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Elija código de conexión: ");
                string cod_conexion = Console.ReadLine();
                string chequeaSiExiste2 = con.realizarConsulta("SELECT pais_origen FROM Conexiones WHERE cod_conexion = " + cod_conexion, 1, false);
                // Verifico con una consulta si la conexión existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
                if (chequeaSiExiste2.Length == 1)
                {
                    Console.WriteLine("Conexión no existe!");
                    Console.WriteLine("Presione una tecla para volver al menú principal!");
                    Console.ReadKey();
                }
                else
                {
                    string chequeaSiCorresponde = con.realizarConsulta("SELECT ciudad_origen FROM Conexiones WHERE cod_aerolinea = " + cod_aerolinea + "  AND cod_conexion = " + cod_conexion + "", 1, false); // verifico si la conexión corresponde a la aerolínea seleccionada
                    if (chequeaSiCorresponde.Length == 1)
                    {
                        Console.WriteLine("Conexión no corresponde con aerolínea!");
                        Console.WriteLine("Presione una tecla para volver al menú principal!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("Elija día de vuelo: ");
                        try
                        {
                            int dia = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Elija mes de vuelo: ");
                            int mes = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Elija año de vuelo: ");
                            int año = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Ingrese precio de vuelo: ");
                            int precio = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Ingrse puntaje de vuelo: ");
                            int puntaje = Convert.ToInt32(Console.ReadLine()); // Debe ser de 1 a 10
                            if (puntaje >= 1 && puntaje <= 10)
                            {
                                con.realizarModificacion("INSERT INTO Vuelos VALUES(" + cod_aerolinea + "," + cod_conexion + ",'" + año + "-" + mes + "-" + dia + "'," + precio + ",DEFAULT, " + puntaje + ")"); // Inserto el vuelo
                                string consideracion = con.realizarConsulta("SELECT AVG(Puntaje) FROM Vuelos WHERE cod_aerolinea = " + cod_aerolinea + "", 1, false); // Promedio el promedio de puntaje de esa aerolinea
                                con.realizarModificacion("UPDATE Aerolíneas SET Consideracion = '" + consideracion + "' WHERE cod_aerolinea = " + cod_aerolinea + ""); // Actualizo promedio de consideración
                                Console.WriteLine("Vuelo agregado!");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.WriteLine("El puntaje debe estar entre 1 y 10!");
                                Thread.Sleep(1000);
                            }

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No ha ingresado valores numéricos correctos!");
                            Thread.Sleep(1000);
                        }

                    }
                }
            }
        }
        private static void darDeBaja()
        {
            Console.Clear();
            Console.Write("Elija código de aerolínea: ");
            string cod_aerolinea = Console.ReadLine();
            ConexionBBDD c = new ConexionBBDD();
            string chequeaSiExiste = c.realizarConsulta("SELECT Fecha FROM Vuelos WHERE cod_aerolinea = " + cod_aerolinea, 1, false);
            // Verifico con una consulta si la conexión existe. Si la consulta me devuelve algo con más de un caracter, es porque existe.
            if (chequeaSiExiste.Length == 1)
            {
                Console.WriteLine("Aerolínea no existe o no tiene vuelos asignados!");
                Console.WriteLine("Presione una tecla para volver al menú principal!");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Elija código de conexión: ");
                string cod_conexion = Console.ReadLine();
                string chequeaSiExisteConexion = c.realizarConsulta("SELECT Fecha FROM Vuelos WHERE cod_conexion = " + cod_conexion, 1, false);
                if (chequeaSiExisteConexion.Length == 1)
                {
                    Console.WriteLine("Conexión no existe o no tiene vuelos asignados!");
                    Console.WriteLine("Presione una tecla para volver al menú principal!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Ingrese día del vuelo");
                    string dia = Console.ReadLine();
                    Console.WriteLine("Ingrese mes del vuelo");
                    string mes = Console.ReadLine();
                    Console.WriteLine("Ingrese año del vuelo");
                    string año = Console.ReadLine();
                    c.realizarModificacion("DELETE FROM Vuelos WHERE cod_aerolinea = " + cod_aerolinea + " AND cod_conexion = " + cod_conexion + " AND Fecha = '"+año+"/"+mes+"/"+dia+"' ");
                    string consideracion = c.realizarConsulta("SELECT AVG(Puntaje) FROM Vuelos WHERE cod_aerolinea = " + cod_aerolinea + "", 1, false); // Promedio el promedio de puntaje de esa aerolinea
                    if (Convert.ToString(consideracion[0]) == " ") // Si la consulta anterior da vacía, es porque la aerolínea ya no tiene vuelos asignados
                    {
                        consideracion = "0"; // Por lo tanto, seteo la consideración en 0
                    }
                    c.realizarModificacion("UPDATE Aerolíneas SET Consideracion = '" + consideracion + "' WHERE cod_aerolinea = " + cod_aerolinea + ""); // Actualizo promedio de consideración
                    Console.WriteLine("Vuelo eliminado!");
                    Thread.Sleep(1000);
                }
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
            return BBDD.realizarConsulta("Select COLUMN_NAME From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'Vuelos' ", 1, false) + "\n\n" + BBDD.realizarConsulta("SELECT * FROM Vuelos", 6, true);
        }
    }
}
