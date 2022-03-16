using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public partial class Cliente
    {
        [Key]
        public int Clienteid { get; set; }
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public int TipoCli { get; set; }
        public string? Sexo { get; set; }
        public int SitCli { get; set; }
        public virtual SituacaoCliente? SitCliNavigation { get; set; } = null!;
        public virtual TipoCliente? TipoCliNavigation { get; set; } = null!;
    }
}
