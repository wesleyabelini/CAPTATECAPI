namespace Entity
{
    public partial class SituacaoCliente
    {
        public SituacaoCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Situacaocliid { get; set; }
        public string Situacaocliente1 { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
