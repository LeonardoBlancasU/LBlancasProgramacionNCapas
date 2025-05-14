using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class CargaMasiva
    {
        public static void LeerArchivo()
        {

            string ruta = @"C:\Users\digis\Documents\Leonardo Blancas Uribe\datosprueba.txt";
            try
            {
                string NoInsertados = "";
                using (StreamReader file = new StreamReader(ruta))
                {
                    string line = file.ReadLine();
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] datos = line.Split('|');
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Nombre = datos[0];
                        usuario.ApellidoPaterno = datos[1];
                        usuario.ApellidoMaterno = datos[2];
                        usuario.Email = datos[3];
                        usuario.UserName = datos[4];
                        usuario.FechaNacimiento = datos[5];
                        usuario.Sexo = datos[6];
                        usuario.CURP = datos[7];
                        usuario.Estatus = Convert.ToBoolean(datos[8]);
                        usuario.Telefono = datos[9];
                        usuario.Celular = datos[10];
                        usuario.Password = datos[11];
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Convert.ToByte(datos[12]);
                        usuario.Direccion = new ML.Direccion();

                        string Errores = ValidarDatos(usuario);

                        //string rutaNoInsertados = @"C:\Users\digis\Documents\Leonardo Blancas Uribe\LBlancasProgramacionNCapas\Errores\NoInsertados_Totales.txt";

                        if (Errores == "")
                        {
                            ML.Result result = BL.Usuario.AddEFLQ(usuario);
                            if (!result.Correct)
                            {  
                                NoInsertados = NoInsertados + "No se pudo insertar el usuario " + usuario.Nombre + "|" + usuario.ApellidoPaterno + "|" + usuario.ApellidoMaterno + "| \n ";            
                            }
                        }
                        else
                        {

                             NoInsertados = NoInsertados + $"No se pudo insertar el usuario {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno} debido a que: " + Errores;
                        }
                    }    
                }
                string rutaNoInsertados = @"C:\Users\digis\Documents\Leonardo Blancas Uribe\LBlancasProgramacionNCapas\Errores\NoInsertados_Totales.txt";
                using (StreamWriter noInsertados = new StreamWriter(rutaNoInsertados))
                { 
                    noInsertados.WriteLine("No se agregaron los siguientes usuarios:");
                    noInsertados.WriteLine(NoInsertados);                
                }

            }
            catch
            {
                Console.WriteLine("Ocurrio un problema al leer el archivo");
            }
        }

        public static string ValidarDatos(ML.Usuario usuario)
        {
            string Errores = "";

            if (!Regex.IsMatch(usuario.Nombre, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores = "Para el nombre solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.ApellidoPaterno, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores =Errores + "Para el apellido paterno solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.ApellidoMaterno, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores = Errores + "Para el apellido materno solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.Password, @"^(?!.*(\d)\1)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$"))
            {
                Errores = Errores + "La Contraseña debe tener una Minuscula, una Mayuscula, un Numero, un Caracter Especial (@$!%*?&), no debe contener numeros repetidos.\n";
            }
            if(!Regex.IsMatch(usuario.UserName, @"^(?=.*\d)[A-ZÁÉÍÓÚÑ][A-Za-záéíóúñÁÉÍÓÚÑ\d-]*$"))
            {
                Errores = Errores + "El Nombre de Usuario debe iniciar con Mayuscula y tener al menos un numero.\n";
            }
            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Errores = Errores + "El Email no es valido";
            }
            if (!Regex.IsMatch(usuario.FechaNacimiento, @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
            {
                Errores = Errores + "La Fecha de Nacimiento es invalida, el formato es dd/mm/aaaa.\n";
            }
            if (!Regex.IsMatch(usuario.Sexo, @"^[MF]{1}$"))
            {
                Errores = Errores + "El Sexo es invalido solo se permite M: masculino o F: femenino.\n";
            }
            if (!Regex.IsMatch(usuario.Telefono, @"^\d{10}$"))
            {
                Errores = Errores + "El Telefono solo debe tener numeros y exactamente 10 digitos.\n";
            }
            if (!Regex.IsMatch(usuario.Celular, @"^\d{10}$"))
            {
                Errores = Errores + "El Celular solo debe tener numeros y exactamente 10 digitos.\n";
            }
            if(!Regex.IsMatch(usuario.CURP, @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$"))
            {
                Errores = Errores + "El Curp no es valido.\n";
            }
            return Errores;
        }
    }
}
