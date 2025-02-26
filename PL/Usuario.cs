using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Usuario
    {
        public static void Add()
        {

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el nombre del Usuario: ");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la edad del Usuario");

            usuario.Edad = Convert.ToByte(Console.ReadLine());

            Console.WriteLine("Ingrese la direccion del Usuario");

            usuario.Direccion = Console.ReadLine();

            Console.WriteLine("Ingrese el curp del Usuario");

            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Ingrese el costo de registrar al Usuario");

            usuario.Costo = Convert.ToDecimal(Console.ReadLine());


            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Se insertó correctamente el Usuario");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al insertar al Usuario");
            }
        }

        public static void Update()
        {

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el ID del Usuario a Actualizar: ");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre del Usuario: ");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la edad del Usuario");

            usuario.Edad = Convert.ToByte(Console.ReadLine());

            Console.WriteLine("Ingrese la direccion del Usuario");

            usuario.Direccion = Console.ReadLine();

            Console.WriteLine("Ingrese el curp del Usuario");

            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Ingrese el costo de registrar al Usuario");

            usuario.Costo = Convert.ToDecimal(Console.ReadLine());


            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Se actualizo correctamente el Usuario");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al actualizar al Usuario");
            }
        }

        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el ID del Usuario a Eliminar: ");

            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Se elimino correctamente el Usuario");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al eliminar al Usuario");
            }
            Console.WriteLine("Presiona una tecla para salir...");
            Console.ReadKey();
        }

    }
    }

