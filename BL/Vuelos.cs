﻿using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ML;
using System;
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
                    var query = context.Vuelos.FromSql($"EXECUTE dbo.GetAllVuelo").ToList();

                    if (query != null)
                    {
                        foreach(var registros in query)
                        {
                            ML.Vuelos vuelo = new ML.Vuelos();

                            vuelo.Id_Vuelo = registros.IdVuelo;
                            vuelo.Numero_Vuelo = registros.NumeroVuelo;
                            vuelo.Origen = registros.Origen;
                            vuelo.Hora_Salida = registros.HoraSalida;
                            vuelo.Hora_LLegada = registros.HoraLlegada;
                            vuelo.aerolinia = new ML.Aerolinea();
                            vuelo.aerolinia.Id_Aerolinia = registros.IdAerolineaNavigation.IdAerolinea;
                            vuelo.aerolinia.AerolineaNombre = registros.IdAerolineaNavigation.AerolineaNombre;

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
                    select obj).Single();

                    vuelo.Id_Vuelo = query.IdVuelo;
                    vuelo.Numero_Vuelo = query.NumeroVuelo;
                    vuelo.Origen = query.Origen;
                    vuelo.Hora_Salida = query.HoraSalida;
                    vuelo.Hora_LLegada = query.HoraLlegada;
                    vuelo.aerolinia = new ML.Aerolinea();
                    vuelo.aerolinia.Id_Aerolinia = query.IdAerolineaNavigation.IdAerolinea;
                    vuelo.aerolinia.AerolineaNombre = query.IdAerolineaNavigation.AerolineaNombre;

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