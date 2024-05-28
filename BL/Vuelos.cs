using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vuelos
    {
        public static (bool, string, Exception) Add(ML.Vuelos vuelo)
        {
            try
            {
                using(var context = new GitHubProyectoHtContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddVuelo {vuelo.Numero_Vuelo}, '{vuelo.Destino}', '{vuelo.Origen}', '{vuelo.Hora_Salida}', '{vuelo.Hora_LLegada}', {vuelo.aerolinia.Id_Aerolinia}");
                    if(query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch(Exception ex) {
                return(false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception) Update(ML.Vuelos vuelo)
        {
            try
            {
                using (var context = new GitHubProyectoHtContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateVuelo {vuelo.Id_Vuelo},{vuelo.Numero_Vuelo}, '{vuelo.Destino}', '{vuelo.Origen}', '{vuelo.Hora_Salida}', '{vuelo.Hora_LLegada}', {vuelo.aerolinia.Id_Aerolinia}");
                    if (query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception) Delete(int IdVuelo)
        {
            try
            {
                using (var context = new GitHubProyectoHtContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteVuelo {IdVuelo}");
                    if (query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string?, List<ML.Vuelos>, Exception) GetAll()
        {
            List<ML.Vuelos> vuelos= new List<ML.Vuelos>();
            try
            {
                using(var context = new GitHubProyectoHtContext())
                {
                    var query = (from obj in context.Vuelos
                                join arl in context.Aerolineas on obj.IdAerolinea equals arl.IdAerolinea
                                select new {obj, aerolinea = arl.AerolineaNombre}).ToList();
                    if (query != null)
                    {
                        foreach(var registros in query)
                        {
                            ML.Vuelos vuelo = new ML.Vuelos();

                            vuelo.Id_Vuelo = registros.obj.IdVuelo;
                            vuelo.Numero_Vuelo = registros.obj.NumeroVuelo;
                            vuelo.Origen = registros.obj.Origen;
                            vuelo.Destino = registros.obj.Destino;
                            vuelo.Hora_Salida = registros.obj.HoraSalida;
                            vuelo.Hora_LLegada = registros.obj.HoraLlegada;
                            vuelo.aerolinia = new ML.Aerolinea();
                            vuelo.aerolinia.Id_Aerolinia = Convert.ToInt32(registros.obj.IdAerolinea);
                            vuelo.aerolinia.AerolineaNombre = registros.aerolinea;

                            vuelos.Add(vuelo);
                        }
                        context.SaveChanges();
                    }
                    return (true, null, vuelos, null);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
        }
        public static (bool, string?, ML.Vuelos, Exception?) GetById(int IdVuelos)
        {
            ML.Vuelos vuelo = new ML.Vuelos();
            try
            {
                using (var context = new GitHubProyectoHtContext())
                {
                    var query = (from obj in context.Vuelos
                     where obj.IdVuelo.Equals(IdVuelos)
                     join arl in context.Aerolineas on obj.IdAerolinea equals arl.IdAerolinea
                     select new { obj, aerolinea = arl.AerolineaNombre }).Single();

                    vuelo.Id_Vuelo = query.obj.IdVuelo;
                    vuelo.Numero_Vuelo = query.obj.NumeroVuelo;
                    vuelo.Origen = query.obj.Origen;
                    vuelo.Destino = query.obj.Destino;
                    vuelo.Hora_Salida = query.obj.HoraSalida;
                    vuelo.Hora_LLegada = query.obj.HoraLlegada;
                    vuelo.aerolinia = new ML.Aerolinea();
                    vuelo.aerolinia.Id_Aerolinia = Convert.ToInt32(query.obj.IdAerolinea);
                    vuelo.aerolinia.AerolineaNombre = query.aerolinea;

                    if (query != null)
                    {
                        return (true, null, vuelo, null);
                    }
                    else
                    {
                        return (false, null, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null, ex);
            }
        }
    }
}
