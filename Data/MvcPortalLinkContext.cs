using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mheportal.Models;

 
 namespace mheportal.Models
 {

    public class MvcPortalLinkContext : DbContext
    {
        private static bool _EnsureCreated;
        public MvcPortalLinkContext (DbContextOptions<MvcPortalLinkContext> options)
            : base(options)
        {
            if (!_EnsureCreated)
            {
                Database.EnsureCreated();
                _EnsureCreated = true;
            }
        }

        public DbSet<mheportal.Models.PortalLinkDataModel> PortalLink { get; set; }
    }
 }
