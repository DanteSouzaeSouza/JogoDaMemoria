using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JogoDaMemoria
{
    public partial class FrmJogoMem : Form
    {
        // Criando e instanciando uma lista de letras para que possa ser usada no tabuleiro
        // Cada elemento aparece duas vezes na listagem
        private readonly List<string> icons = new List<string>
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        //Criando elemento Random para ajudar colocação dos itens do tabuleiro
        private readonly Random random = new Random();

        // Criando variáveis para controle dos cliques
        private Label primeiraClicada;
        private Label segundaClicada;

        public FrmJogoMem()
        {
            InitializeComponent();

            AdicionaIcones();
        }


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
                    // Ajustando a cor do ícone para torná-lo invisível
                    iconLabel.ForeColor = iconLabel.BackColor;
                    // remove o ícone adicionado da lista de ícones
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            // O timer só começa a rodar depois que dois itens
            // que não combinam forem selecionados então
            // devemos ignorar novos cliques se o timer estiver rodando
            if (timerJogo.Enabled)
                return;


            // Adiciona uma variável ligada ao controle Label do form
            var clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se a label clicada está com a cor preta, ignorar o clique
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                // Checando se é a primeira clicada do jogadr
                // se for, pintar a label clicada de preto
                if (primeiraClicada == null)
                {
                    primeiraClicada = clickedLabel;
                    primeiraClicada.ForeColor = Color.Black;

                    return;
                }
            }

            // Se o jogador chegar nessa parte do código,
            // isso significa que ele e clicou no primeiro item,
            // esse deve ser o segundo item clicado,
            // então devemos pintá-lo de preto
            segundaClicada = clickedLabel;
            segundaClicada.ForeColor = Color.Black;


            // Verificando se o jogo acabou e alguém venceu
            ChecaVencedor();

            // Se o jogador chegar nessa parte do código,
            // isso significa que ele e clicou dois itens iguais,
            // manteremos eles aparecendo e resetaremos as variáveis
            // de controle para null pra que possam ser feitos novos cliques
            if (primeiraClicada.Text == segundaClicada.Text)
            {
                primeiraClicada = null;
                segundaClicada = null;
                return;
            }

            // Se o jogador chegar nessa parte do código,
            // isso significa que ele clicou dois itens diferentes
            // o timer é então iniciado e em 3/4 de um segundo
            // esconderá os ícones
            timerJogo.Start();
        }

        private void timerJogo_Tick(object sender, EventArgs e)
        {
            // Pára o timer
            timerJogo.Stop();

            // Esconde os dois ícones
            primeiraClicada.ForeColor = primeiraClicada.BackColor;
            segundaClicada.ForeColor = segundaClicada.BackColor;

            // Reseta primeiraClicada e segundaClicada 
            // pra que o programa saiba que a próxima clicada é a primeira novamente
            primeiraClicada = null;
            segundaClicada = null;
        }


        private void ChecaVencedor()
        {
            // verifica cada label no tabuleiro e checa uma a uma
            // pra ver se alguma não tem cor de fundo combinando com a cor do ícone
            foreach (Control control in tlpCards.Controls)
            {
                var iconLabel = control as Label;

                if (iconLabel != null)
                    // checa se a cor do fundo do ícone bate com a cor do próprio ícone
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
            }

            // Se o foreach não der return, não achou ícones iguais de cores diferentes
            // isso significa que o usuário venceu, mostrando a tela abaixo e fechando o form
            MessageBox.Show("Você conseguiu combinar todos os ícones!", "Parabéns!");
            Close();
        }
    }
}