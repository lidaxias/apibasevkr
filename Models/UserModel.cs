using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apibase.Models
{
    public class UserModel
    {

        [Key] 
        [Column("user_id")]
        public int IdUser { get; set; }

        [Column("user_name")]
        public string Username { get; set; }

        [Column("user_login")]
        public string Login { get; set; }

        [Column("user_password")]
        public string Password { get; set; }

        [Column("user_phone")]
        public string Phone { get; set; }

    }
}
