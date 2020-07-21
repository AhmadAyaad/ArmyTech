
namespace ArmyTech.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentTeacher")]
    public partial class StudentTeacher
    {
        public int ID { get; set; }

        public int? StudentId { get; set; }

        public int? TeacherId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
