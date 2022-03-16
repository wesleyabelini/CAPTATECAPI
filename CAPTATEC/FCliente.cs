using Entity;
using System.Net;

namespace CAPTATEC
{
    public partial class FCliente : Form
    {
        AccessAPI.AccessAPI api = new AccessAPI.AccessAPI();

        List<SituacaoCliente> situacaoCliente = new List<SituacaoCliente>();
        List<TipoCliente> tiposCliente = new List<TipoCliente>();

        Cliente cliente;

        bool _isNovo;

        public FCliente(bool isNovo, Cliente cli = null)
        {
            InitializeComponent();
            _isNovo = isNovo;

            cliente = cli;

            Start();

            if (!isNovo) PreencheCampos();
        }

        private void Start()
        {
            Task t = Task.Run(() =>
            {
                situacaoCliente = api.ReceiveAllSituacaoCliente().Result;
                tiposCliente = api.ReceiveAllTipoCliente().Result;
            });

            t.Wait();
            SetCombos();
        }

        private void SetCombos()
        {
            cmbTipoCli.DataSource = tiposCliente;
            cmbTipoCli.DisplayMember = "TipoCliente1";
            cmbTipoCli.ValueMember = "TipoCliId";

            cmbSitCli.DataSource = situacaoCliente;
            cmbSitCli.DisplayMember = "Situacaocliente1";
            cmbSitCli.ValueMember = "Situacaocliid";
        }

        private void PreencheCampos()
        {
            txtNome.Text = cliente.Nome;
            txtCPF.Text = cliente.Cpf;
            cmbTipoCli.SelectedValue = cliente.TipoCli;
            cmbSexo.Text = cliente.Sexo;
            cmbSitCli.SelectedValue = cliente.SitCli;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cli = new Cliente()
                {
                    Nome = txtNome.Text,
                    Cpf = txtCPF.Text,
                    TipoCli = Convert.ToInt16(cmbTipoCli.SelectedValue),
                    Sexo = cmbSexo.Text,
                    SitCli = Convert.ToInt16(cmbSitCli.SelectedValue),
                };

                if (!_isNovo) cli.Clienteid = cliente.Clienteid;

                cliente = cli;
                HttpStatusCode s = HttpStatusCode.OK;

                if (_isNovo)
                {
                    Task t = Task.Run(() => { s = api.InsertAClient(cli).Result; });
                    t.Wait();
                }
                else
                {
                    Task t = Task.Run(() => { s = api.UpdateAClient(cli).Result; });
                    t.Wait();
                }

                if ((int)s != (int)HttpStatusCode.OK && (int)s != (int)HttpStatusCode.Created)
                {
                    if ((int)s == (int)HttpStatusCode.Conflict)
                        MessageBox.Show("Houve um conflito entre os dados na operação. Verifique o CPF do cliente!", Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if ((int)s == (int)HttpStatusCode.BadRequest)
                        MessageBox.Show("Não foi possível executar a operação. Verifique os dados: Nome, CPF do cliente!", Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível cadastrar o cliente:\r\n\r\n" + cliente.Cpf + "\r\nNome:" + cliente.Nome, 
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (int)Keys.Back) e.Handled = true;
        }
    }
}