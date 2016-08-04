using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TPFinal
{
    class ConexionBBDD
    {
        SqlConnection conn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=TPFinal;Integrated Security=True");
        SqlDataReader rdr = null;

        //CONSULTAR TABLA(DEVUELVE STRING)          
        public string realizarConsulta(string SqlCommand, int camposADevolver, bool listar)
        {
            string s = " ";
            try
            {
                conn.Open();
                // Establecemos la consulta y le pasamos por parametro la conexion            
                SqlCommand cmd = new SqlCommand(SqlCommand, conn);
                // Ejecutamos la consulta y llenamos el DataReader            
                rdr = cmd.ExecuteReader();
                // Devolvemos en s el  primer campo de todos nuestros resultados
                while (rdr.Read())                     // Chequea cantidad de campos
                {
                    for (int i = 0; i < camposADevolver; i++)
                    {
                        s = s + rdr[i];
                        s = s + " - ";
                    }
                    if (listar)
                    {
                        s = s + "\n ";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // cerramos el reader            
                if (rdr != null)
                {
                    rdr.Close();
                }
                // Cerramos la conexion            
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return s;
        }
        //MANIPULAR TABLAS
        public void realizarModificacion(string modificación)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(modificación, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
