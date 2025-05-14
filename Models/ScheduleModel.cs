using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;

namespace apibase.Models
{
    public class ScheduleModel
    {
        [Key]
        public int Schedule_Id { get; set; }
        public int Subject_Id { get; set; }
        public int User_Id { get; set; }
        public int Classroom_Id { get; set; }
        public int Group_Id { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }

        // Навигационные свойства
        public Subject Subject { get; set; }
        public LessonType LessonType { get; set; }
        public UserModel Teacher { get; set; }
        public Classroom Classroom { get; set; }
        public Group Group { get; set; }
    }

    public class Subject
    {
        [Key]
        public int Subject_Id { get; set; }

        [Column("subject_name")]
        public string Name { get; set; }

        [Column("lesson_type_id")] // Столбец в таблице subject
        public int LessonTypeId { get; set; }


        [ForeignKey("LessonTypeId")] // Указывает, что LessonTypeId связывает с LessonType
        public LessonType LessonType { get; set; } // Навигационное свойство
    }


    public class LessonType
    {
        [Key]
        public int Lesson_Type_Id { get; set; }

        [Column("name")]
        public string Name { get; set; } 
    }

    public class Classroom
    {
        [Key]
        public int Classroom_Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
    
    public class Group
    {
        [Key]
        [Column("group_id")]
        public int Group_Id { get; set; }

        [Column("group_name")]
        public string Name { get; set; }
    }


}
