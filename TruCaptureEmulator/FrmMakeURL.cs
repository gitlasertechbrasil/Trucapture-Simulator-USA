using br.com.ltb.Camera.Pumatronix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    public partial class FrmMakeURL : Form
    {
        string[] values = null;
        private bool _allowEmpty = false;

        public string URL { get; set; } = string.Empty;

        public FrmMakeURL(List<ParameterCGI> parameters)
        {
            InitializeComponent();

            values = Enum.GetNames(typeof(ParameterCGI.Parameters));
            string[] onlyRead = Enum.GetNames(typeof(ParameterCGI.ReadParameters));

            Array.Sort(values);

            foreach (string s in values)
            {
                ParameterCGI.Parameters currentParameter = (ParameterCGI.Parameters)Enum.Parse(typeof(ParameterCGI.Parameters), s);

                if (s != "DESCONHECIDO")
                {
                    ParameterCGI prm = parameters.Find(p => p.Tag.ToString() == s);
                    //List<object[]> unsortedList = new List<object[]>();

                    if (prm != null)
                    {
                        //bool selected = false;
                        //if (prm.Type == ParameterCGI.TypeOfParameter.SET)
                        //{
                        //    selected = true;
                        //}
                        //unsortedList.Add(new object[] { selected, s, prm.Value });                        


                        dgvParameters.Rows.Add(new object[] { false, s, prm.Value });

                        //dgvParameters.Rows[dgvParameters.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Blue;
                        //dgvParameters.Refresh();
                    }
                    else
                    {
                        dgvParameters.Rows.Add(new object[] { false, s, string.Empty });
                        //unsortedList.Add(new object[] { false, s, string.Empty });
                    }
                }
            }

            dgvParameters.AllowUserToAddRows = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pctSave_Click(object sender, EventArgs e)
        {
            URL = string.Empty;
            pctSave.Focus();
            dgvParameters.Focus();

            string erros = string.Empty;
            bool versaoAlterada = false;
            int versao;
            int revisao;

            foreach (DataGridViewRow r in dgvParameters.Rows)
            {
                if (r.Cells[0].Value != null && Convert.ToBoolean(r.Cells[0].Value)
                    && r.Cells[1].Value != null && !string.IsNullOrEmpty(r.Cells[1].Value.ToString())
                    && r.Cells[2].Value != null && !string.IsNullOrEmpty(r.Cells[2].Value.ToString()))
                {
                    if (values.Contains(r.Cells[1].Value.ToString()))
                    {
                        ParameterCGI current = Program.ParameterList.First(t => t.Tag.ToString() == r.Cells[1].Value.ToString());
                        int index = Program.ParameterList.IndexOf(current);

                        string valor = r.Cells[2].Value.ToString();


                        if (current.Tag == ParameterCGI.Parameters.Versao)
                        {
                            try
                            {
                                versao = Convert.ToInt32(r.Cells[2].Value.ToString());
                                versaoAlterada = true;
                            }
                            catch (Exception)
                            {
                                erros += $"Não foi possível alterar a versão, o valor {valor} é inválido para o parâmetro\r\n";
                                continue;
                            }
                        }
                        else if (current.Tag == ParameterCGI.Parameters.Revisao)
                        {
                            try
                            {
                                revisao = Convert.ToInt32(r.Cells[2].Value.ToString());
                                versaoAlterada = true;
                            }
                            catch (Exception)
                            {
                                erros += $"Não foi possível alterar a revisão, o valor {valor} é inválido para o parâmetro\r\n";
                                continue;
                            }
                        }
                        else if(current.Tag == ParameterCGI.Parameters.Password)
                        {
                            Program.senha = valor;
                            continue;
                        }

                        Program.ParameterList[index] = new ParameterCGI((ParameterCGI.Parameters)Enum.Parse(typeof(ParameterCGI.Parameters), r.Cells[1].Value.ToString()),
                                                    r.Cells[2].Value.ToString(), ParameterCGI.TypeOfParameter.GET);
                        DialogResult = DialogResult.Yes;
                    }
                }
            }

            if(versaoAlterada)
            {
                string vs = string.Empty;
                string rev = string.Empty;

                ParameterCGI current = Program.ParameterList.First(t => t.Tag == ParameterCGI.Parameters.Versao);
                vs = current.Value;
                current = Program.ParameterList.First(t => t.Tag == ParameterCGI.Parameters.Revisao);
                rev = current.Value;


                string[] splt = Program.version.Split('.');
                Program.version = $"v{vs}.{rev}.{splt[2]}";

            }

            if(!string.IsNullOrEmpty(erros))
            {
                MessageBox.Show(erros, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvParameters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }

}
