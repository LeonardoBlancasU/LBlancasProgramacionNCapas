using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool Salir = false;
            while (!Salir)
            {
                Console.WriteLine("Que te gustaria hacer?");
                Console.WriteLine("1. Ingresar un Usuario");
                Console.WriteLine("2. Actualizar un Usuario");
                Console.WriteLine("3. Borrar un Usuario");
                Console.WriteLine("4. Mostrar Usuarios Registrados");
                Console.WriteLine("5. Mostrar un Usuario por su ID");
                Console.WriteLine("6. Salir");
                int Opcion = Convert.ToInt32(Console.ReadLine());

                switch (Opcion)
                {
                    case 1:
                        PL.Usuario.Add();
                        break;
                    case 2:
                        PL.Usuario.Update();
                        break;
                    case 3:
                        PL.Usuario.Delete();
                        break;
                    case 4:
                        PL.Usuario.GetAll();
                        break;
                    case 5:
                        PL.Usuario.GetById();
                        break;
                    case 6:
                        Salir = true;
                        Console.WriteLine("Saliendo del Menu");
                        Console.WriteLine("Presiona una tecla para salir...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opcion no valida, saliendo....");
                        Console.WriteLine("Presiona una tecla para salir...");
                        Console.ReadKey();
                        Salir = true;
                        break;

                }
            }
        }
    }
}

