using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El Nombre Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Nombre debe iniciar con Mayuscula")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Nombre de Usuario")]
        [RegularExpression(@"^(?=.*\d)[A-ZÁÉÍÓÚÑ][A-Za-záéíóúñÁÉÍÓÚÑ\d-]*$", ErrorMessage = "El Nombre de Usuario debe iniciar con Mayuscula y tener al menos un numero")]
        [MinLength(8, ErrorMessage = "El Nombre de Usuario Debe tener minimo 8 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Apellido Paterno")]
        [MaxLength(50, ErrorMessage = "El Apellido Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Apellido debe iniciar con Mayuscula")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Apellido Materno")]
        [MaxLength(50, ErrorMessage = "El Apellido Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Nombre debe iniciar con Mayuscula")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "Correo no valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?!.*(\d)\1)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$", ErrorMessage = "Debe tener una Minuscula, una Mayuscula, un Numero, un Caracter Especial (@$!%*?&), no debe contener numeros repetidos ni consecutivos")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Sexo")]
        [MaxLength(1, ErrorMessage = "Solo se Acepta F o M")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Phone]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Phone]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Estatus")]
        public bool Estatus { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Curp")]
        [MinLength(18, ErrorMessage = "La Curp debe tener 18 Caracteres")]
        [MaxLength(18, ErrorMessage = "La Curp debe tener 18 Caracteres")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage ="Curp no Valido")]
        public string CURP { get; set; }

        public byte[] Imagen { get; set; }

        public List<object> Usuarios { get; set; }

        public ML.Rol Rol { get; set; }

        public string ImagenBase64 { get; set; }

        public ML.Direccion Direccion { get; set; }
    }
}
