using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileRanger.DAL.Models;

public class Folder{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    [Column("fullPath")] public string FullPath { get; set; }
    [Column("parentPath")] public string ParentPath { get; set; }
}