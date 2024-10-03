using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class VeterinaryDr
    {

        [Key]
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string? Designation { get; set; }

        public string? EmailAddress { get; set; }

        public string? ClinicAddress { get; set; }

        public string? WorkingHours { get; set;}

        public long? Contact { get; set; }

    }
}
