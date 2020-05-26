using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaMemoria
{
    public partial class FrmJogoMem : Form
    {

        //Criando elemento Random para ajudar na criação dos itens do tabuleiro
        Random random = new Random();

        // Criando e instanciando uma lista de letras para que possa ser usada no tabuleiro
        // Cada elemento aparece duas vezes na listagem
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };


        private void AdicionaIcones()
        {
            // O TableLayoutPanel tem 16 labels,
            // e a lista de ícones tem 16 elementos.
            // essa função puxa os ícones da lista
            // e os adiciona aos elementos um a um
            foreach (Control control in tlpCards.Controls)
            {
                // criando um objeto Label e associando-o a um controle de formulário
                var iconLabel = control as Label;
                if (iconLabel != null)
                {
                    // Gerando numero aleatório
                    var randomNumber = random.Next(icons.Count);
                    // colocando o ícone referente ao número aleatório na label
                    iconLabel.Text = icons[randomNumber];
                    // Ajustando a cor do ícone
                    iconLabel.ForeColor = iconLabel.BackColor;
                    // remove o ícone adicionado da lista de ícones
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public FrmJogoMem()
        {
            InitializeComponent();

            AdicionaIcones();
        }
    }
}
