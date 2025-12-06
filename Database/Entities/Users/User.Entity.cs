using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPIRIAN.Database.Users;

public class User
{
    public User() { }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }
};