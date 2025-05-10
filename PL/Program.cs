using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            string archivo = @"C:\Users\digis\Documents\Leonardo Blancas Uribe\LBlancasProgramacionNCapas\PL_MVC\archivos\datosprueba.txt";
            try { 
                using (StreamReader sr = new StreamReader(archivo))
                {
                    string fila;
                    while ((fila = sr.ReadLine()) != null)
                    {
                        string[] columnas = fila.Split('|');
                        Console.WriteLine("Datos Registrados en el TXT:");
                        foreach (string columna in columnas) {
                        Console.WriteLine(columna);
                        }
                    }
                }
            }
            catch {
                Console.WriteLine("Ocurrio un problema al leer el archivo");
            }
        }
    }
}

