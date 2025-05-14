using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apibase.Models
{
    //[Table("visits")] // Указываем имя таблицы в БД
    public class VisitModel
    {
        [Key]
        public int Visit_Id { get; set; }

        [Column("student_id")]
        public int Student_Id { get; set; }

        [Column("group_id")]
        public int Group_Id { get; set; }

        [Column("status")]
        public string Status { get; set; } // "present", "absent", "sick"

        [Column("date")]
        public DateTime Date { get; set; }

        // Навигационные свойства
        public Student Student { get; set; }
        public Group Group { get; set; }
    }

    public class Student
    {
        [Key] // Первичный ключ
        public int Student_Id { get; set; }

        [Column("student_name")]
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        // Навигационное свойство для группы
        public virtual Group Group { get; set; }

    }


    public class Semester
    {
        [Key]
        [Column("semester_id")]
        public int Semester_Id { get; set; }

        [Required]
        [Column("semester_name")]
        public string Semester_Name { get; set; }

        [Column("group_id")]
        public int Group_Id { get; set; }


        // Навигационные свойства
        public virtual Group Group { get; set; }
    }
}

