using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebFacturaMvc.Datos
{
    public class DBImagen: DbContext
    {
        public DbSet<configuracion> Imagens { get; set; }
    }
}