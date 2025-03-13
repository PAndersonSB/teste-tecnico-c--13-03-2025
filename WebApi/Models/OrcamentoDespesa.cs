namespace WebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OrcamentoDespesa")]
public class OrcamentoDespesa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrcamentoDespesaID { get; set; } // 

    [Required]
    public int Codigo { get; set; } // i
    public string? Ficha { get; set; } // 
    [Required]
    public short Ano { get; set; } // 
    [Required]
    [Column(TypeName = "decimal(12,2)")]
    public decimal Valor { get; set; } // 
    [Required]
    [Column(TypeName = "date")]
    public DateTime DataCriacao { get; set; } // 
    [Required]
    public int UnidadeGestora { get; set; } // 
    public string? ElencoContaCodigo { get; set; } // 
    public int? FonteRecurso { get; set; } // 
    public int? Numero { get; set; } // 
}