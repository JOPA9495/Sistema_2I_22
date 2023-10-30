using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_2I_03
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void txt_Id_TextChanged(object sender, EventArgs e)
        {

        }


        private void btn_Click(object sender, EventArgs e)
        {
            string strCon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\joão\TI\bd_22_banco.3.mdb";

            try
            {

                if (txt_id.Text != string.Empty)
                {
                    //Declarar objetos de Classe de Conexão
                    OleDbConnection conectar = new OleDbConnection(strCon);
                    //Abrir Objeto de Conexão com Banco de Dados criada acima;
                    conectar.Open();

                    //Mondando a String (concatenando) com os objetos do Formulário 
                    string strSQL = "SELECT * FROM fornecedores WHERE (ID =" + txt_id.Text + ")";

                    //Criar o Objeto com a classe de Command (comando) para armazenar a Instrução / Comando SQL
                    OleDbCommand comandoSQL = new OleDbCommand(strSQL, conectar);
                    //Criar objeto DbDataReader
                    DbDataReader lerRegistro = comandoSQL.ExecuteReader();

                    //Metodo Read(): Informe um "boolean" "True" (exite Registro) e "False" (Não Existe Registro),
                    //Possibilita Ler o Proximo Regsitro de uma Tabela (Enquanto for True, Se existir registro)

                    if (lerRegistro.Read())
                    {
                        //Populando Objetos do Form com Dados do Registro Lido (lerRegistro)
                        txt_empresa.Text = lerRegistro.GetString(1);
                        txt_contato.Text = lerRegistro.GetString(2);
                        txt_ramo.Text = lerRegistro.GetString(3);
                        txt_telefone.Text = lerRegistro.GetString(4);
                        txt_email.Text = lerRegistro.GetString(5);
                        txt_site.Text = lerRegistro.GetString(6);
                        txt_cnpj.Text = lerRegistro.GetString(7);

                        if (DialogResult.No == MessageBox.Show("Deseja Editar Registro?", "Sistema Informa",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button2))
                        {
                            //Limpar Objetos do Formulário
                            LimparDados();

                        }
                        else
                        {
                            //Habilitar botões
                            btn_excluir.Enabled = true;
                            btn_alterar.Enabled = true;

                            //Desabilitando Campo CPF
                            txt_id.Enabled = false;
                        }


                    }
                    else
                    {
                        MessageBox.Show("Identificador " + txt_id.Text + " Não Localizado!!!", "Sistema Informa");
                        txt_id.Focus();
                    }



                    //Fechar Conexão
                    conectar.Close();

                }
                else
                {
                    MessageBox.Show("Preencher o campo Identificador (ID)!!!");
                }

                //Votar Cursor para o Objeto de Formulario
                txt_id.Focus();


            }

            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void LimparDados()
        {
            txt_id.Clear();
            txt_empresa.Clear();
            txt_ramo.Clear();
            txt_email.Clear();
            txt_cnpj.Clear();
            txt_contato.Clear();
            txt_telefone.Clear();
            txt_site.Clear();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btn_inserir_Click(object sender, EventArgs e)
        {
            //Inicio Inserir
            string strCon = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\joão\TI\bd_22_banco.3.mdb";

            try
            {
                if (txt_empresa.Text != string.Empty)
                {
                    //Declarar objetos de Classe de ConexÃ£o
                    OleDbConnection conectar = new OleDbConnection(strCon);
                    //Abrir Objeto de ConexÃ£o com Banco de Dados criada acima;
                    conectar.Open();

                    //Montando a variavel tipo String "strSQL" de Leitura de Dados (concatenando) com os objetos do FormulÃ¡rio
                    string strSQL = "SELECT * FROM fornecedores WHERE (empresa =" + "'" + txt_empresa.Text + "')";

                    //Criar o Objeto com a classe de Command (comando) para armazenar a InstruÃ§Ã£o / Comando SQL
                    OleDbCommand comandoSQL = new OleDbCommand(strSQL, conectar);

                    DbDataReader lerRegistro = comandoSQL.ExecuteReader();

                    //Metodo Read(): Informe um "boolean" "True" (exite Registro) e "False" (NÃ£o Existe Registro),
                    //Possibilita Ler o Proximo Registro de uma Tabela (Enquanto for True, Se existir registro)

                    if (lerRegistro.Read())
                    {
                        MessageBox.Show("Contato (nome) jÃ¡ Existe!!!", "Sistema Informa");
                    }
                    else
                    {
                        //Montando a variavel tipo String "strSQL" de InserÃ§Ã£o dos Dados (concatenando) com os objetos do FormulÃ¡rio 
                        strSQL = "INSERT INTO fornecedores (empresa,contato,ramo,telefone,email,site,cnpj) " +
                                  "VALUES (" +
                                  "'" + txt_empresa.Text + "'," +
                                  "'" + txt_contato.Text + "'," +
                                  "'" + txt_ramo.Text + "'," +
                                  "'" + txt_telefone.Text + "'," +
                                  "'" + txt_email.Text + "'," +
                                  "'" + txt_site.Text + "'," +
                                  "'" + txt_cnpj.Text + "')";

                        //Observe que as MaskTextBox ao utilizar o caracer "." ele inverte para "," (vice-versa)

                        //Criar o Objeto com a classe de Command (comando) para armazenar a InstruÃ§Ã£o / Comando SQL
                        //comandoSQL jÃ¡ Ã© uma Classe OleDbCommand, portanto sÃ³ precisamos Instacia-lÃ¡
                        comandoSQL = new OleDbCommand(strSQL, conectar);

                        //Executando 
                        comandoSQL.ExecuteNonQuery();
                        MessageBox.Show("Registro Inserido com Sucesso...", "Sistema Informa");

                        //Limpar Objetos do FormulÃ¡rio
                        LimparDados();

                    }

                    //Fechar ConexÃ£o
                    conectar.Close();

                }
                else
                {
                    MessageBox.Show("Preencher pelo menos o campo NOME!!!");
                }

                //Votar Cursor para o Objeto de Formulario
                txt_empresa.Focus();


            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            //---Termino Inserir
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
             string strCon = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\joão\TI\bd_22_banco.3.mdb";

            try
            {
                OleDbConnection conectar = new OleDbConnection(strCon);
                //Abrir Objeto de ConexÃ£o com Banco de Dados criada acima;
                conectar.Open();

                if (DialogResult.Yes == MessageBox.Show("Confirma AlteraÃ§Ã£o do Registro?", "Sistema Informa",
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                                 MessageBoxDefaultButton.Button2))
                {

                    //Mondando a String de Consulta (concatenando) com os objetos do FormulÃ¡rio 
                    string strSQL = "UPDATE fornecedores SET " +
                                    "empresa = '" + txt_empresa.Text + "', " +
                                    "contato = '" + txt_contato.Text + "', " +
                                    "ramo = '" + txt_ramo.Text + "', " +
                                    "telefone = '" + txt_telefone.Text + "', " +
                                    "email = '" + txt_email.Text + "', " +
                                    "site = '" + txt_site.Text + "', " +
                                    "CNPJ = '" + txt_cnpj.Text + "' " +
                                    "WHERE (ID = " + txt_id.Text + ")";



                    //comandoSQL jÃ¡ Ã© uma Classe OleDbCommand, portanto sÃ³ precisamos Instacia-lÃ¡
                    OleDbCommand comandoSQL = new OleDbCommand(strSQL, conectar);

                    //Executando sem resposta
                    comandoSQL.ExecuteNonQuery();
                    MessageBox.Show("Registro Alterado com Sucesso...", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show("OperaÃ§Ã£o Cancelada!!!", "Sistema Informa");
                }
                MessageBox.Show("Limpar FormulÃ¡rio (Dados)...", "Sistema Informa");

                //Limpar Objetos do FormulÃ¡rio
                LimparDados();

                //Fechar ConexÃ£o
                conectar.Close();

                //Desbalitar BotÃµes Delete e Alterar / Hablitar BotÃ£o Inserir
                btn_excluir.Enabled = false;
                btn_alterar.Enabled = false;
                btn_inserir.Enabled = true;

                //Habilitar e Votar Cursor para o Objeto de Formulario
                txt_id.Enabled = true;
                txt_id.Focus();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erros ou Falhas");
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            string strCon = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\joão\TI\bd_22_banco.3.mdb";
            try
            {

                //Declarar objetos de Classe de ConexÃ£o
                OleDbConnection conectar = new OleDbConnection(strCon);
                //Abrir Objeto de ConexÃ£o com Banco de Dados criada acima;
                conectar.Open();
                //ConsistÃªncia para ExclusÃ£o 
                if (DialogResult.Yes == MessageBox.Show("Excluir Registro?", "Sistema Informa",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button2))
                {
                    //Mondando a String de Consulta (concatenando) com os objetos do FormulÃ¡rio 
                    string strSQL = "DELETE FROM fornecedores WHERE(ID = " +txt_id.Text + ")";

                    //comandoSQL jÃ¡ Ã© uma Classe OleDbCommand, portanto sÃ³ precisamos Instacia-lÃ¡
                    OleDbCommand comandoSQL = new OleDbCommand(strSQL, conectar);

                    //Executando sem resposta
                    comandoSQL.ExecuteNonQuery();
                    MessageBox.Show("Registro ExcluÃ­do com Sucesso...", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show("OperaÃ§Ã£o Cancelada!!!", "Sistema Informa");
                }

                MessageBox.Show("Limpar FormulÃ¡rio (Dados)...", "Sistema Informa");

                //Limpar Objetos do FormulÃ¡rio
                LimparDados();

                //Fechar ConexÃ£o
                conectar.Close();

                //Desbalitar BotÃµes Delete e Alterar / Hablitar BotÃ£o Inserir
                btn_excluir.Enabled = false;
                btn_alterar.Enabled = false;
                btn_inserir.Enabled = true;

                //Habilitar e Votar Cursor para o Objeto de Formulario
                txt_id.Enabled = true;
                txt_id.Focus();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erros ou Falhas");
            }
        }
    }
}

