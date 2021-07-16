using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Implementacao_Csharp_XML
{
    class Componente
    {
        //instancia de picture box customizada para movimentação e regulagem de tamanho em tempo de execução
        public SizeMoveablePicBox picBoxComponente = new SizeMoveablePicBox();        

        public int index;

        //menu de contexto do componente
        public ContextMenu componentContxtMenu = new ContextMenu();

        //definição de items do menu de contexto
        MenuItem mnuItemSetpath = new MenuItem();
        MenuItem mnuItemSetImgpath = new MenuItem();
        MenuItem mnuItemOpen = new MenuItem();
        MenuItem mnuItemDelete = new MenuItem();
        MenuItem mnuItemSetXlsheet = new MenuItem();
        MenuItem mnuItemOpenXlsheet = new MenuItem();

        //string de caminho do arquivo associado ao componente
        public string caminhoArquivo;
        public string caminhoPlanilha;


        //dialogo de abertura e definição do caminho do arquivo e imagem
        OpenFileDialog fileDialog = new OpenFileDialog();

        public Componente()
        {
            //definições da pictureBox
            //picBoxComponente.Size = new System.Drawing.Size(100, 50);
            picBoxComponente.BorderStyle = BorderStyle.FixedSingle;
            picBoxComponente.SizeMode = PictureBoxSizeMode.StretchImage;
            //picBoxComponente.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom| AnchorStyles.Left | AnchorStyles.Right);

            //adição de event handlers para movimentação da picBox com o botão do meio do mouse
            picBoxComponente.MouseDown += new MouseEventHandler(picBoxComponente_MouseDown);
            picBoxComponente.MouseMove += new MouseEventHandler(picBoxComponente_MouseMove);

            //associando o menu de contexto à pictureBox
            picBoxComponente.ContextMenu = componentContxtMenu;

            mnuItemOpen.Text = "Abrir";
            mnuItemOpenXlsheet.Text = "Abrir planilha";
            mnuItemSetpath.Text = "Definir arquivo";
            mnuItemSetImgpath.Text = "Definir imagem";
            mnuItemSetXlsheet.Text = "Associar planilha";
            mnuItemDelete.Text = "Deletar";

            //adição dos items no menu de contexto
            componentContxtMenu.MenuItems.Add(mnuItemOpen);
            componentContxtMenu.MenuItems.Add(mnuItemOpenXlsheet);
            componentContxtMenu.MenuItems.Add(mnuItemSetImgpath);
            componentContxtMenu.MenuItems.Add(mnuItemSetXlsheet);
            componentContxtMenu.MenuItems.Add(mnuItemSetpath);
            componentContxtMenu.MenuItems.Add(mnuItemDelete);

            //adição de handlers para o evento de click dos items
            mnuItemOpen.Click += new EventHandler(mnuItemOpen_Click);            
            mnuItemOpenXlsheet.Click += new EventHandler(mnuItemOpenXlsheet_Click);
            mnuItemSetImgpath.Click += new EventHandler(mnuItemSetImgpath_Click);
            mnuItemSetXlsheet.Click += new EventHandler(mnuItemSetXlsheet_Click);
            mnuItemSetpath.Click += new EventHandler(mnuItemSetPath_Click);
            mnuItemDelete.Click += new EventHandler(mnuItemDelete_Click);

        }

        //ponto auxiliar de referencia para reposicionamento da picbox
        Point PicBoxMouseDownLocation;

        //identificação do clique e arraste do mouse na pictureBox
        public void picBoxComponente_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) PicBoxMouseDownLocation = e.Location;
        }
        //movimentação da pictureBox
        public void picBoxComponente_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //alteração das coordenadas da pictureBox baseado no movimento do mouse, quando clicando
                picBoxComponente.Left += e.X - PicBoxMouseDownLocation.X;
                picBoxComponente.Top += e.Y - PicBoxMouseDownLocation.Y;
            }
        }

        //função de clique no item de definição do arquivo referente ao componente
        private void mnuItemSetPath_Click(object sender, EventArgs e)
        {
            fileDialog.Title = "Definir arquivo do componente";
            fileDialog.ShowDialog();
            caminhoArquivo = fileDialog.FileName;
        }

        //função de clique no item de definição da imagem referente ao componente
        private void mnuItemSetImgpath_Click(object sender, EventArgs e)
        {
            fileDialog.Title = "Definir arquivo de imagem do equipamento";
            fileDialog.ShowDialog();
            picBoxComponente.ImageLocation = fileDialog.FileName;
        }

        //associar planilha
        private void mnuItemSetXlsheet_Click(object sender, EventArgs e)
        {
            fileDialog.Title = "Definir planilha referente ao equipamento";
            fileDialog.ShowDialog();
            caminhoPlanilha = fileDialog.FileName;

        }


        //abertura de arquivo
        private void mnuItemOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(caminhoArquivo);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Arquivo não encontrado ou não definido.");
            }
            catch
            {

            }

        }


        //abertura de planilha referente ao componente

        private void mnuItemOpenXlsheet_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(caminhoPlanilha);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Arquivo não encontrado ou não definido.");
            }
            catch
            {

            }

        }

        private void mnuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.picBoxComponente.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
