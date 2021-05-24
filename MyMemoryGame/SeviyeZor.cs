﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace MyMemoryGame
{
    public partial class SeviyeZor : Form
    {
        public SeviyeZor()
        {
            InitializeComponent();
        }
        byte islem = 0;
        PictureBox oncekiResim;
        byte kalan = 10;
        byte gosterhak = 3;
        byte time = 90;
        void resimSifirla()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Properties.Resources.block;
                }
            }
        }
        void tagSifirla()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Tag = "block";
                }
            }
        }
        void tagDagit()
        {
            int[] sayilar = new int[20];
            Random rnd = new Random();
            byte i = 0;
            while (i < 20)
            {
                int rastgele = rnd.Next(1, 21);
                if (Array.IndexOf(sayilar, rastgele) == -1)
                {
                    sayilar[i] = rastgele;
                    i++;
                }
            }
            for (byte j = 0; j < 20; j++)
            {
                if (sayilar[j] > 10) sayilar[j] -= 10;
            }

            byte b = 0;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Tag = sayilar[b].ToString();
                    b++;
                }
            }
        }
        void showImage(PictureBox box)
        {
            switch (Convert.ToInt32(box.Tag))
            {
                case 1:
                    box.Image = Properties.Resources.z1;
                    break;
                case 2:
                    box.Image = Properties.Resources.z2;
                    break;
                case 3:
                    box.Image = Properties.Resources.z3;
                    break;
                case 4:
                    box.Image = Properties.Resources.z4;
                    break;
                case 5:
                    box.Image = Properties.Resources.z5;
                    break;
                case 6:
                    box.Image = Properties.Resources.z6;
                    break;
                case 7:
                    box.Image = Properties.Resources.z7;
                    break;
                case 8:
                    box.Image = Properties.Resources.z8;
                    break;
                case 9:
                    box.Image = Properties.Resources.z9;
                    break;
                case 10:
                    box.Image = Properties.Resources.z10;
                    break;
                default:
                    box.Image = Properties.Resources.block;
                    break;
            }

        }
        void karsilastirma(PictureBox onceki, PictureBox sonraki)
        {
            SoundPlayer sesDogru = new SoundPlayer();
            string dizin = Application.StartupPath + "\\Voice\\Dogru.wav";
            sesDogru.SoundLocation = dizin;

            SoundPlayer sesYanlis = new SoundPlayer();
            string dizin1 = Application.StartupPath + "\\Voice\\Yanlis.wav";
            sesYanlis.SoundLocation = dizin1;

            SoundPlayer sesAlkis = new SoundPlayer();
            string dizin2 = Application.StartupPath + "\\Voice\\Alkis.wav";
            sesAlkis.SoundLocation = dizin2;


            if (onceki.Tag.ToString() == sonraki.Tag.ToString())
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);

                onceki.Visible = false;
                sonraki.Visible = false;
                kalan--;
                lbl_puan.Text = Convert.ToString(Convert.ToInt32(lbl_puan.Text) + 10);
                lbl_canli.Text = "Tebrikler! Doğru Cevap +10 Puan:)";
                sesDogru.Play();
                if (kalan == 0)
                {
                    sesAlkis.Play();
                    lbl_canli.Text = "Tebrikler." + lbl_gamer.Text + " Güzel Oyun!  Toplam Puan:" + lbl_puan.Text + " Süre " + lbl_time.Text;
                    lbl_bilgi.Text = "Tebrikler";
                    timer1.Enabled = false;
                }
                else
                    lbl_bilgi.Text = ":" + kalan;
            }
            else
            {
                sesYanlis.Play();
                lbl_puan.Text = Convert.ToString(Convert.ToInt32(lbl_puan.Text) - 5);
                lbl_canli.Text = "Yanlış Cevap -5 Puan Daha Dikkatli Bakmalısın!";

                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                onceki.Image = Image.FromFile("block.png");
                sonraki.Image = Image.FromFile("block.png");

            }
        }

        private void SeviyeZor_Load(object sender, EventArgs e)
        {
            lbl_puan.Text = "0";
            lbl_gamer.Text = Giris.sendData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox simdikiResim = (sender as PictureBox);
            showImage((sender as PictureBox));

            if (islem == 0)
            {
                oncekiResim = simdikiResim;
                islem++;
            }
            else
            {
                if (oncekiResim == simdikiResim)
                {
                    MessageBox.Show("Aynı Resim");
                    islem = 0;
                    oncekiResim.Image = Image.FromFile("block.png");
                }

                else
                {
                    karsilastirma(oncekiResim, simdikiResim);
                    islem = 0;
                }

            }
        }
        void goster()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) showImage((x as PictureBox));
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            resimSifirla();
            if (--gosterhak == 0) btn_goster.Enabled = false;
            btn_goster.Text = "Goster (" + gosterhak + ")";
        }
        void gizle()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile("block.png");
                }
            }
        }

        private void btn_goster_Click(object sender, EventArgs e)
        {
            goster();
            lbl_canli.Text = "Gösterme Yapıldı...";
            islem = 0;
        }
        void VisibleAc()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Visible = true;

                }
            }
        }
        void NewGame()
        {
            resimSifirla();
            tagSifirla();
            tagDagit();
            btn_goster.Visible = true;
            btn_yenioyun.Visible = true;
            btn_basla.Visible = false;
            kalan = 10;
            islem = 0;
            time = 90;
            timer1.Enabled = true;
        }

        private void btn_yenioyun_Click(object sender, EventArgs e)
        {
            NewGame();
            VisibleAc();
            gosterhak = 3;
            lbl_bilgi.Text = ":" + kalan;
            lbl_puan.Text = "0";
            lbl_canli.Text = "MyMemoryGame Seviye:Zor";
            btn_goster.Text = "Goster(3)";
        }

        private void btn_cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void dur()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Enabled = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SoundPlayer sesTime = new SoundPlayer();
            string dizin3 = Application.StartupPath + "\\Voice\\Surebitti.wav";
            sesTime.SoundLocation = dizin3;

            SoundPlayer sesTimeOn = new SoundPlayer();
            string dizin4 = Application.StartupPath + "\\Voice\\SonOn.wav";
            sesTimeOn.SoundLocation = dizin4;

            time -= 1;
            lbl_time.Text = ":" + time;

            if (time <= 14 && time >= 1)
            {
                sesTimeOn.Play();
            }
            else if (time == 0)
            {
                sesTime.Play();
                lbl_canli.Text = "Maalesef Süre Doldu..";
                lbl_bilgi.Text = "Süre Doldu";
                lbl_time.Text = "0";
                dur();
                timer1.Enabled = false;
            }
        }

        private void btn_anamenu_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void btn_basla_Click(object sender, EventArgs e)
        {
            NewGame();
            VisibleAc();
        }
    }
}
