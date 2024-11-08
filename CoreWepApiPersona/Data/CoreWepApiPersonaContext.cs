using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreWepApiPersona.Modelo;

namespace CoreWepApiPersona.Data
{
    public class CoreWepApiPersonaContext : DbContext
    {
        public CoreWepApiPersonaContext (DbContextOptions<CoreWepApiPersonaContext> options)
            : base(options)
        {
        }

        public DbSet<CoreWepApiPersona.Modelo.Persona> Persona { get; set; }
    }
}
