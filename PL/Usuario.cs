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

            Console.WriteLine("Ingresa el ID Rol del Usuario");
            usuario.Rol =new ML.Rol();
            usuario.Rol.IdRol = Convert.ToByte(Console.ReadLine()) ;

            //ML.Result result = BL.Usuario.Add(usuario);
            ML.Result result = BL.Usuario.AddSP(usuario);

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

            Console.WriteLine("Ingresa el ID Rol del Usuario a Actualizar");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToByte(Console.ReadLine());

            //ML.Result result = BL.Usuario.Update(usuario);
            ML.Result result = BL.Usuario.UpdateSP(usuario);

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
        {   Console.WriteLine("Ingrese el ID del Usuario a Eliminar: ");

            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            //ML.Result result = BL.Usuario.Delete(usuario);
            ML.Result result = BL.Usuario.DeleteSP(IdUsuario);

            if (result.Correct)
            {
                Console.WriteLine("Se elimino correctamente el Usuario");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al eliminar al Usuario");
            }
        }

        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAllAdapter();

            if (result.Correct)
            {
                Console.WriteLine("Aqui tienes la Lista de Usuarios:\n");
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("ID: " + usuario.IdUsuario);
                    Console.WriteLine("Nombre:" + usuario.Nombre);
                    Console.WriteLine("Edad: " + usuario.Edad);
                    Console.WriteLine("Dirección: " + usuario.Direccion);
                    Console.WriteLine("CURP: " + usuario.Curp);
                    Console.WriteLine("Costo: " + usuario.Costo);
                    Console.WriteLine("ID ROL:" + usuario.Rol.IdRol + "\n");
                }
            }
            else
            {
                Console.WriteLine($"Ocurrió un problema: {result.ErrorMessage} ");
            }
           
        }

        public static void GetById()
        {
           
            Console.WriteLine("Ingresa el Id del Usuario que quieras Mostrar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetByIdAdapter(IdUsuario);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine("Aqui tienes la Información del Usuario:\n");
                Console.WriteLine($"ID: {usuario.IdUsuario}");
                Console.WriteLine($"Nombre: {usuario.Nombre}");
                Console.WriteLine($"Edad: {usuario.Edad}");
                Console.WriteLine($"Dirección: {usuario.Direccion}");
                Console.WriteLine($"CURP: {usuario.Curp}");
                Console.WriteLine($"Costo: {usuario.Costo}");
                Console.WriteLine("ID ROL:" + usuario.Rol.IdRol + "\n");
            }
            else
            {
                Console.WriteLine($"Ocurrió un problema al mostrar la informacion: {result.ErrorMessage}");
            }
            
        }
    }
}

