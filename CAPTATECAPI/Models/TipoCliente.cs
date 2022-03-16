using System;
using System.Collections.Generic;

namespace CAPTATECAPI.Models
{
    public partial class TipoCliente
    {
        public TipoCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Tipocliid { get; set; }
        public string Tipocliente1 { get; set; } = null!;

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
