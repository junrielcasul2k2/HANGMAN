using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HANGMAN111
{
    public partial class Form1 : Form
    {
        string[] secretWords = { "hangman", "programming", "computer", "science", "engineering" };
        string guessedWord;
        int attemptsLeft = 6;
        int currentWordIndex;


        Form2 obj = new Form2();

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeLetterButtons();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeGame()
        {
            currentWordIndex = new Random().Next(secretWords.Length);
            guessedWord = new string('*', secretWords[currentWordIndex].Length);

            label1.Text = guessedWord;
            label2.Text = $"Attempts Left: {attemptsLeft}";

            // Reset hangman image
            pictureBox1.Image = Properties.Resources.hangman1;
        }

        private void GuessLetter(char letter)
        {
            if (secretWords[currentWordIndex].Contains(letter))
            {
                for (int i = 0; i < secretWords[currentWordIndex].Length; i++)
                {
                    if (secretWords[currentWordIndex][i] == letter)
                    {
                        guessedWord = guessedWord.Substring(0, i) + letter + guessedWord.Substring(i + 1);
                    }
                }
                label1.Text = guessedWord;
            }
            else
            {
                attemptsLeft--;
                label2.Text = $"Attempts Left: {attemptsLeft}";

                // Update hangman image
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("hangman" + (7 - attemptsLeft));
            }

            if (secretWords[currentWordIndex] == guessedWord)
            {
                MessageBox.Show("Congratulations! You guessed the word: " + secretWords[currentWordIndex]);
                ResetGame();
            }
            else if (attemptsLeft == 0)
            {
                
                obj.Show();
                this.Hide();
                MessageBox.Show("Sorry, you ran out of attempts. The word was: " + secretWords[currentWordIndex]);
                ResetGame();
            }
        }

        private void ResetGame()
        {
            InitializeGame();
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.Enabled = true;
                }
            }
        }

        private void InitializeLetterButtons()
        {
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    char letter;
                    if (button.Text.Length == 1 && char.TryParse(button.Text.ToUpper(), out letter))
                    {
                        button.Click += button2_Click;
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            char letter = button.Text.ToLower()[0];
            button.Enabled = false;
            GuessLetter(letter);
        }
    }
}
