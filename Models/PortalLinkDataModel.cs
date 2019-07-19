using System;
using System.ComponentModel.DataAnnotations;

namespace mheportal.Models
{
    public class PortalLinkDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }       
        public string LinkUrl { get; set; }
    
    }
}