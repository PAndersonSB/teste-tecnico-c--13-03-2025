namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ProgramacaoFinanceiraDespesaConfig")]
    public class ProgramacaoFinanceiraDespesaConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramacaoFinanceiraDespesaConfigID { get; set; } // PK, int, NOT NULL

        [Required]
        public short Ano { get; set; } // smallint, NOT NULL

        [Required]
        public int UnidadeGestoraIDFK { get; set; } // int, NOT NULL

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes01Perc { get; set; } // decimal(10,2), NOT NULL

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes02Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes03Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes04Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes05Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes06Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes07Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes08Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes09Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes10Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes11Perc { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mes12Perc { get; set; }
    }