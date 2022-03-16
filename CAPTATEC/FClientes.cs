using Entity;
using System.Net;

namespace CAPTATEC
{
    public partial class FClientes : Form
    {
        AccessAPI.AccessAPI api = new AccessAPI.AccessAPI();

        List<Cliente> allCliente = new List<Cliente>();
        List<Cliente> filteredCliente = new List<Cliente>();

        List<SituacaoCliente> situacaoCliente = new List<SituacaoCliente>();
        List<TipoCliente> tiposCliente = new List<TipoCliente>();

        Cliente currentClient = new Cliente();

        public FClientes()
        {
            InitializeComponent();
            StartRefresh();
        }

        private void StartRefresh()
        {
            try
            {
                Task t = Task.Run(() =>
                {
                    allCliente = api.ReceiveAllCliente().Result;
                    situacaoCliente = api.ReceiveAllSituacaoCliente().Result;
                    tiposCliente = api.ReceiveAllTipoCliente().Result;

                    filteredCliente = allCliente;
                });

                t.Wait();

                SetCombos();
                SetGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve uma falha ao acessar a API e preencher os campos.\r\n" + ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetGridView()
        {
            dgvCliente.DataSource = filteredCliente;

            dgvCliente.Columns["ClienteId"].Visible = false;
            dgvCliente.Columns["TipoCli"].Visible = false;
            dgvCliente.Columns["SitCli"].Visible = false;
            dgvCliente.Columns["SitCliNavigation"].Visible = false;
            dgvCliente.Columns["TipoCliNavigation"].Visible = false;

            dgvCliente.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCliente.Columns["CPF"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCliente.Columns["Sexo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvCliente.Columns["Sexo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void PreencheCliente()
        {
            int id = Convert.ToInt32(dgvCliente.CurrentRow.Cells[0].Value);

            currentClient = allCliente.Where(x => x.Clienteid == id).FirstOrDefault();

            if(currentClient != null)
            {
                txtNome.Text = currentClient.Nome;
                txtCPF.Text = currentClient.Cpf;
                cmbTipoCli.SelectedValue = currentClient.TipoCli;
                cmbSexo.Text = currentClient.Sexo;
                cmbSitCli.SelectedValue = currentClient.SitCli;
            }
        }

        private void OpenClienteForm(bool isNovo, Cliente cli = null)
        {
            FCliente fCliente = new FCliente(isNovo, cli);
            if (DialogResult.OK == fCliente.ShowDialog()) StartRefresh();
        }

        private void dgvCliente_SelectionChanged(object sender, EventArgs e)
        {
            PreencheCliente();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            OpenClienteForm(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            OpenClienteForm(false, currentClient);
        }

        private void dgvCliente_DoubleClick(object sender, EventArgs e)
        {
            OpenClienteForm(false, currentClient);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o cliente " + currentClient.Nome + "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                HttpStatusCode a = HttpStatusCode.OK;
                Task t = Task.Run(() => { a = api.DeleteAClient(currentClient).Result; });

                t.Wait();

                if((int)a == (int)HttpStatusCode.OK) StartRefresh();
                else MessageBox.Show("Não foi possível realizar a exclusão do cliente:\r\r\r\rCPF: " + currentClient.Cpf + "\r\rNome: " + currentClient.Nome, 
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            filteredCliente = allCliente.Where(x => x.Cpf.Contains(txtFilterCPF.Text) || x.Nome.Contains(txtFilterCPF.Text)).ToList();
            SetGridView();
        }

        private void txtRefresh_Click(object sender, EventArgs e)
        {
            StartRefresh();
        }
    }
}