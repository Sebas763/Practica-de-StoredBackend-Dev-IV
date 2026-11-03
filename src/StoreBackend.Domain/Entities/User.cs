using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class User
{
    [Key] //atributos
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; private set; }

    [Required]
    public Guid UserResourceId { get; set; }

    [Column("Name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("Email")]
    [StringLength(100)]
    public string? Email { get; set; }
}
