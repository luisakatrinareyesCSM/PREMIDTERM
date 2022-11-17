using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Luisapangilinan.Midterm.Infrastructure.Domain.Models
{
    public class Class
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? YearLevel { get; set; }
        public Meeting? Meeting { get; set; }
        public DateTime? Startdate { get; set; }
        public Guid? CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }


    }
    public enum Meeting
    {
        MFW,
        TTH,
        SAT
    }
}
