using CasoEstudio2_API.Entities;
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
        //[HttpGet]
        //[Route("api/ConsultarCasas")]
        //public List<CasasEnt> ConsultarCasas()
        //{
        //    using (var bd = new CasoEstudioLNEntities())
        //    {
        //        var datos = (from x in bd.CasasSistema
        //                     select x).ToList();

        //        if (datos.Count > 0)
        //        {
        //            List<CasasEnt> res = new List<CasasEnt>();
        //            foreach (var item in datos)
        //            {
        //                res.Add(new CasasEnt
        //                {
        //                    IdCasa = item.IdCasa,
        //                    DescripcionCasa = item.DescripcionCasa,
        //                    PrecioCasa = item.PrecioCasa,
        //                    UsuarioAlquiler = item.UsuarioAlquiler,
        //                    FechaAlquiler = (DateTime)item.FechaAlquiler,
        //                    Estado = "Disponible"
        //                });
        //            }

        //            return res;
        //        }

        //        return new List<CasasEnt>();
        //    }
        //}

      

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
    }
}
