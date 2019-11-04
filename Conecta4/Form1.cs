using ePOSOne.btnProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conecta4
{
    public partial class Form1 : Form
    {
        bool turno;
        int countTurnos;

        Button_WOC[,] fichas = new Button_WOC[6, 7];

        public Form1()
        {
            InitializeComponent();

            int x = 600 / 8;
            int y = 489 / 7;

            for (int i = 0, count = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++, count++)
                {
                    fichas[i, j] = new Button_WOC();

                    fichas[i, j].FlatStyle = FlatStyle.Flat;
                    fichas[i, j].FlatAppearance.BorderSize = 0;
                    fichas[i, j].Text = j.ToString();

                    fichas[i, j].ButtonColor = Color.White;
                    fichas[i, j].BorderColor = Color.Blue;
                    fichas[i, j].TextColor = Color.Transparent;
                    fichas[i, j].OnHoverButtonColor = Color.White;
                    fichas[i, j].OnHoverBorderColor = Color.Blue;
                    fichas[i, j].OnHoverTextColor = Color.Transparent;

                    fichas[i, j].Location = new Point(x * j + x / 4, y * i + y / 4);
                    fichas[i, j].Height = fichas[i, j].Width = y / 4 * 3;

                    fichas[i, j].Click += new EventHandler(Ficha_Click);

                    Controls.Add(fichas[i, j]);
                }
            }
        }

        private void Ficha_Click(object sender, EventArgs e)
        {
            int j = Int32.Parse((sender as Button_WOC).Text);

            int i = 5;
            while (i >= 0)
            {
                if (fichas[i, j].ButtonColor == Color.White)
                {
                    fichas[i, j].ButtonColor = (turno) ? Color.Red : Color.Yellow;
                    fichas[i, j].OnHoverButtonColor = (turno) ? Color.Red : Color.Yellow;

                    countTurnos++;

                    if (comprueba(i, j, fichas[i, j].ButtonColor))
                    {
                        string win = "Color ";
                        win += (fichas[i, j].ButtonColor == Color.Red) ? "RED" : "YELLOW";
                        win += " is the winer!!!";
                        MessageBox.Show(win);

                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    else if (countTurnos == 42)
                    {
                        MessageBox.Show("Draw Game");

                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }

                    turno = !turno;

                    break;
                }
                i--;
            }
        }

        private bool comprueba(int x, int y, Color color)
        {
            for (int i = x, count = 0; i < 6; i++)
            {
                if (fichas[i, y].ButtonColor == color)
                {
                    count++;
                    if (count == 4) return true;
                }
                else break;
            }
            for (int y1 = y, y2 = y, aux = y * 2, count = 1; true; aux = y1 + y2)
            {
                while (y1 < 6)
                    if (fichas[x, y1 + 1].ButtonColor == color)
                    {
                        count++;
                        y1++;
                    }
                    else break;
                while (y2 > 0)
                    if (fichas[x, y2 - 1].ButtonColor == color)
                    {
                        count++;
                        y2--;
                    }
                    else break;
                if (count >= 4)
                    return true;
                if (aux == y1 + y2)
                    break;
            }
            for (int x1 = x, y1 = y, x2 = x, y2 = y, aux = x * 2 + y * 2, count = 1; true; aux = x1 + y1 + x2 + y2)
            {
                while (x1 < 5 && y1 < 6)
                    if (fichas[x1 + 1, y1 + 1].ButtonColor == color)
                    {
                        count++;
                        x1++;
                        y1++;
                    }
                    else break;
                while (x2 > 0 && y2 > 0)
                    if (fichas[x2 - 1, y2 - 1].ButtonColor == color)
                    {
                        count++;
                        x2--;
                        y2--;
                    }
                    else break;
                if (count >= 4)
                    return true;
                if (aux == x1 + y1 + x2 + y2)
                    break;
            }
            for (int x1 = x, y1 = y, x2 = x, y2 = y, aux = x * 2 + y * 2, count = 1; true; aux = x1 + y1 + x2 + y2)
            {
                while (x1 < 5 && y1 > 0)
                    if (fichas[x1 + 1, y1 - 1].ButtonColor == color)
                    {
                        count++;
                        x1++;
                        y1--;
                    }
                    else break;
                while (x2 > 0 && y2 < 6)
                    if (fichas[x2 - 1, y2 + 1].ButtonColor == color)
                    {
                        count++;
                        x2--;
                        y2++;
                    }
                    else break;
                if (count >= 4)
                    return true;
                if (aux == x1 + y1 + x2 + y2)
                    break;
            }
            return false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
