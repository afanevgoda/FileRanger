using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class Snapshot{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("createdAt")] public DateTime CreatedAt { get; set; }

    [Column("drive")] public string Drive { get; set; }
    [Column("hostname")] public string Hostname { get; set; }
    public List<Folder> Folders { get; set; }
    
    public List<File> Files { get; set; }
}