using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("ColoniaGetByIdMunicipio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMunicipio", IdMunicipio);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    da.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {

                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = Convert.ToInt32(row[0].ToString());
                            colonia.Nombre = (row[1].ToString());
                            colonia.CodigoPostal = (row[2].ToString());


                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Colonias.";
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetByIdMunicipioEFSP(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var coloniasDB = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();
                    if(coloniasDB.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var coloniaDB in coloniasDB)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = coloniaDB.IdColonia;
                            colonia.Nombre = coloniaDB.Nombre;
                            colonia.CodigoPostal = coloniaDB.CodigoPostal;

                            result.Objects.Add(colonia);
                        }
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Colonias.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetByIdMunicipioEFLQ(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from coloniaDB in context.Colonias
                                 where coloniaDB.IdMunicipio == IdMunicipio
                                 select new { coloniaDB.IdColonia, coloniaDB.Nombre, coloniaDB.CodigoPostal });
                    result.Objects = new List<object>();
                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach (var item in query) 
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = item.IdColonia;
                            colonia.Nombre = item.Nombre;
                            colonia.CodigoPostal = item.CodigoPostal;
                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Colonias";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
