using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;





namespace Implementacao_Csharp_XML
{
    public partial class FormPrincipal : Form
    {
        List<Componente> listaComponentes = new List<Componente>();

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDialog();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            try
            {
                string fileName = fileDialog.FileName;
                //string filename = "D:\Projects\Implementacao_Csharp_XML\teste.xml";
                // Create an XML reader for this file.
                using (XmlReader reader = XmlReader.Create("perls.xml"))
                {
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "componente":

                                    break;
                            }
                        }
                    }
                }//using (XmlReader reader = XmlReader.Create(fileName))
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }

        }

        //gerar novo visualizador de equipamento
        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlEquip.Controls.Clear();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Imagem do equipamento";
            fileDialog.ShowDialog();
            Componente componente = new Componente();
            componente.picBoxComponente.ImageLocation = fileDialog.FileName;
            componente.picBoxComponente.Location = new Point((pnlEquip.Size.Width / 2) - componente.picBoxComponente.Size.Width, (pnlEquip.Size.Height / 2) - componente.picBoxComponente.Size.Height);
            pnlEquip.Controls.Add(componente.picBoxComponente);
        }//private void novoToolStripMenuItem_Click(object sender, EventArgs e)

        //SALVA o arquivo
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                using (XmlWriter writer = XmlWriter.Create(saveFileDialog.FileName))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Equipamento");
                    //armazena as informações de cada Componente presente na coleção de controles do panel no xml
                    foreach (SizeMoveablePicBox componente in pnlEquip.Controls)
                    {
                        //inicio da inserção de componente
                        writer.WriteStartElement("Componente");
                        writer.WriteElementString("PosiçãoX", componente.Location.X.ToString());
                        writer.WriteElementString("PosiçãoY", componente.Location.Y.ToString());
                        writer.WriteElementString("Largura", componente.Size.Width.ToString());
                        writer.WriteElementString("Altura", componente.Size.Height.ToString());
                        //writer.WriteElementString("Arquivo", componente.);

                        //caso não haja imagem associada ao controle
                        if (componente.ImageLocation.ToString() == null)
                        {
                            writer.WriteElementString("Imagem", "");
                        }
                        else {
                            writer.WriteElementString("Imagem", componente.ImageLocation.ToString());
                        }



                        //writer.WriteElementString("Arquivo", componente.caminhoArquivo;
                        //writer.WriteElementString("Planilha", componente.caminhoPlanilha;
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }

        public void cbxNovoComponente_Click(object sender, EventArgs e)
        {
            cbxLinha.Checked = false;
        }


        //status se o botão de inserção de novo componente foi acionado ou não
        public void cbxNovoComponente_CheckedChanged(object sender, EventArgs e)
        {
            //ao selecionar esse botão outras funcionalidades devem ser  desselecionadas
            cbxLinha.Checked = false;
        }

        private void cbxLinha_Click(object sender, EventArgs e)
        {

        }

        ToolTip tTcbxNewComp = new ToolTip();

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //status se o botão de inserção de linha foi acionado ou não
        private void cbxLinha_CheckedChanged(object sender, EventArgs e)
        {
            //ao selecionar esse botão outras funcionalidades devem ser  desselecionadas
            cbxNovoComponente.Checked = false;
        }

        private void pnlEquip_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && cbxNovoComponente.Checked == true)
            {
                Componente componente = new Componente();
                componente.picBoxComponente.Location = e.Location;

                //adição do controle ao painel de componentes
                pnlEquip.Controls.Add(componente.picBoxComponente);


            }
        }
    }//public partial class FormPrincipal : Form


}//namespace Implementacao_Csharp_XML
