﻿using CasoEstudio2_API.Entities;
using CasoEstudio2_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CasoEstudio2_API.Controllers
{
    public class CasasController : ApiController
    {
       

        [HttpGet]
        [Route("api/ConsultarCasa")]
        public CasasEnt ConsultarCasa(long q)
        {
            using (var bd = new CasoEstudioLNEntities())
            {
                var datos = (from x in bd.CasasSistema
                             where x.IdCasa == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    CasasEnt res = new CasasEnt();
                    res.IdCasa = datos.IdCasa;
                    res.DescripcionCasa = datos.DescripcionCasa;
                    res.PrecioCasa = datos.PrecioCasa;
                    res.UsuarioAlquiler = datos.UsuarioAlquiler ?? "N/A"; 
                    res.FechaAlquiler = datos.FechaAlquiler.HasValue ? (DateTime)datos.FechaAlquiler : default(DateTime);
                    res.Estado = (datos.UsuarioAlquiler != null) ? "Reservada" : "Disponible";
                    return res;
                }

                return null;
            }
        }



        [HttpGet]
        [Route("api/ConsultarCasas")]
        public List<CasasEnt> ConsultarCasas()
        {
            using (var bd = new CasoEstudioLNEntities())
            {
                var datos = (from x in bd.CasasSistema
                             where x.PrecioCasa >= 115000 && x.PrecioCasa <= 180000
                             select x).ToList();

                if (datos.Count > 0)
                {
                    List<CasasEnt> res = new List<CasasEnt>();
                    foreach (var item in datos)
                    {
                        var casa = new CasasEnt
                        {
                            IdCasa = item.IdCasa,
                            DescripcionCasa = item.DescripcionCasa,
                            PrecioCasa = item.PrecioCasa,
                            Estado = (item.UsuarioAlquiler != null) ? "Reservada" : "Disponible"
                        };

                        // Verificar si UsuarioAlquiler es nulo
                        if (item.UsuarioAlquiler != null)
                        {
                            casa.UsuarioAlquiler = item.UsuarioAlquiler;
                        }
                        else
                        {
                            casa.UsuarioAlquiler = "N/A"; // O cualquier valor que desees
                        }

                        // Verificar si FechaAlquiler es nula
                        if (item.FechaAlquiler != null)
                        {
                            casa.FechaAlquiler = (DateTime)item.FechaAlquiler;
                        }
                        else
                        {
                            casa.FechaAlquiler = DateTime.MinValue; // O cualquier valor que desees
                        }

                        res.Add(casa);
                    }

                    return res;
                }

                return new List<CasasEnt>();
            }
        }

        [HttpPut]
        [Route("api/EditarCasas")]
        public int EditarCasas(CasasEnt entidad)
        {
            using (var bd = new CasoEstudioLNEntities())
            {
                var datos = (from x in bd.CasasSistema
                             where x.IdCasa == entidad.IdCasa
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.UsuarioAlquiler = entidad.UsuarioAlquiler;
                    datos.FechaAlquiler = DateTime.Now;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }
    }

    
}
