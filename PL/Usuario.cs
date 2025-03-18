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

            Console.WriteLine("Ingrese el apellido paterno del Usuario");

            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del Usuario");

            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del Usuario");

            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingrese el email del Usuario");

            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese la password del Usuario");

            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingrese la Fecha de Nacimiento del Usuario dd-mm-yyyy");

            usuario.FechaNacimiento = Console.ReadLine();   

            Console.WriteLine("Ingrese el Sexo del Usuario (F o M)");

            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese el telefono del Usuario");

            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el celular del Usuario");

            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese Estatus del Usuario");

            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            //Console.WriteLine("Ingrese la imagen del Usuario");

            //usuario.Imagen = Convert.ToByte[](Console.ReadLine());

            Console.WriteLine("Ingrese el curp del Usuario");

            usuario.CURP = Console.ReadLine();
            
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

            Console.WriteLine("Ingrese el apellido paterno del Usuario");

            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del Usuario");

            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del Usuario");

            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingrese el email del Usuario");

            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese la password del Usuario");

            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingrese la Fecha de Nacimiento del Usuario");

            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Sexo del Usuario (F o M)");

            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese el telefono del Usuario");

            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el celular del Usuario");

            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese Estatus del Usuario");

            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            //Console.WriteLine("Ingrese la imagen del Usuario");

            //usuario.Imagen = Convert.ToByte[](Console.ReadLine());

            Console.WriteLine("Ingrese el curp del Usuario");

            usuario.CURP = Console.ReadLine();

            Console.WriteLine("Ingresa el ID Rol del Usuario");
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
                    Console.WriteLine("Nombre de Usuario:" + usuario.UserName);
                    Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                    Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                    Console.WriteLine("Email:" + usuario.Email);
                    Console.WriteLine("Password:" + usuario.Password);
                    Console.WriteLine("Fecha de Nacimiento:" + usuario.FechaNacimiento);
                    Console.WriteLine("Sexo:" + usuario.Sexo);
                    Console.WriteLine("Telefono:" + usuario.Telefono);
                    Console.WriteLine("Celular:" + usuario.Celular);
                    Console.WriteLine("Estatus:" + usuario.Estatus);
                    Console.WriteLine("CURP: " + usuario.CURP);
                    Console.WriteLine("Imagen:" + usuario.Imagen);
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
                Console.WriteLine("ID: " + usuario.IdUsuario);
                Console.WriteLine("Nombre:" + usuario.Nombre);
                Console.WriteLine("Nombre de Usuario:" + usuario.UserName);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Email:" + usuario.Email);
                Console.WriteLine("Password:" + usuario.Password);
                Console.WriteLine("Fecha de Nacimiento:" + usuario.FechaNacimiento);
                Console.WriteLine("Sexo:" + usuario.Sexo);
                Console.WriteLine("Telefono:" + usuario.Telefono);
                Console.WriteLine("Celular:" + usuario.Celular);
                Console.WriteLine("Estatus:" + usuario.Estatus);
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Imagen:" + usuario.Imagen);
                Console.WriteLine("ID ROL:" + usuario.Rol.IdRol + "\n");
            }
            else
            {
                Console.WriteLine($"Ocurrió un problema al mostrar la informacion: {result.ErrorMessage}");
            }
            
        }
    }
}

