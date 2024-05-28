using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aerolinea
    {
        public static (bool, string?, List<ML.Aerolinea>, Exception) GetAll()
        {
            List<ML.Aerolinea> aerolineas = new List<ML.Aerolinea>();
            try
            {
                using (var context = new GitHubProyectoHtContext())
                {
                    var query = context.Aerolineas.FromSql($"EXECUTE dbo.GetAllAerolineas").ToList();

                    if (query != null)
                    {
                        foreach (var registros in query)
                        {
                            ML.Aerolinea aerolinea = new ML.Aerolinea();

                            aerolinea.Id_Aerolinia = registros.IdAerolinea;
                            aerolinea.AerolineaNombre = registros.AerolineaNombre;

                            aerolineas.Add(aerolinea);
                        }
                        context.SaveChanges();
                    }
                    return (true, null, aerolineas, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
        }
    }
}
