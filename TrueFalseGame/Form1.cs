using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueFalseGame
{
    public partial class Form1 : Form
    {
        private TrueFalse database;
        private TrueFalse databaseOld;
        bool bIsSavedOnce = false;
        bool bQuestionSaved = false;
        public Form1()
        {
            InitializeComponent();
            bIsSavedOnce = false;
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (bIsSavedOnce) Close();
            else
            {
                if (MessageBox.Show("Would you like to save changes?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No) Close();
                else if (database != null)
                {
                    database.Save();
                    bIsSavedOnce = true;
                }

                else
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        database = new TrueFalse(saveFile.FileName);

                        //Добавляем в файл вместо текста по умолчанию текст с экрана
                        database.Add(txtQuestion.Text, chkBxTrue.Checked);
                        database.Save();
                        numUpDown.Maximum = 1;
                        numUpDown.Minimum = 1;
                        numUpDown.Value = 1;

                    }
                }
            }
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(saveFile.FileName);
                database.Add("Is the Earth round?", true);
                database.Save();
                numUpDown.Maximum = 1;
                numUpDown.Minimum = 1;
                numUpDown.Value = 1;
            }
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(openFile.FileName);
                database.Load();
                numUpDown.Maximum = database.Count;
                numUpDown.Minimum = 1;
                numUpDown.Value = 1;
            }
        }

        private void miSaveFile_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                database[(int)numUpDown.Value - 1].Text = txtQuestion.Text;
                database[(int)numUpDown.Value - 1].TrueFalse = chkBxTrue.Checked;
                database.Save();
                bQuestionSaved = true;
            }

            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    database = new TrueFalse(saveFile.FileName);

                    //Добавляем в файл вместо текста по умолчанию текст с экрана
                    database.Add(txtQuestion.Text, chkBxTrue.Checked);
                    database.Save();
                    numUpDown.Maximum = 1;
                    numUpDown.Minimum = 1;
                    numUpDown.Value = 1;

                }
            }
        }


        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            txtQuestion.Text = database[(int)numUpDown.Value - 1].Text;
            chkBxTrue.Checked = database[(int)numUpDown.Value - 1].TrueFalse;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            // при добавлении нового вопроса, если предыдущий не был сохранен высплывает напоминание
            //если вопрос уже сохранен - не всплывает
            if (!bQuestionSaved)
            {
                if (MessageBox.Show("Would you like to save changes?", "Caution",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    database.Add($"#{database.Count + 1}", true);
                    numUpDown.Maximum = database.Count;
             //тут допилил так, чтобы он показывал нам при добавлении вопроса следующий вопрос, иначе надо вручную выставлять нумератор
             //это не удобно
                    numUpDown.Value = database.Count;
                    bQuestionSaved = false;
                }

                else
                {
                    database[(int)numUpDown.Value - 1].Text = txtQuestion.Text;
                    database[(int)numUpDown.Value - 1].TrueFalse = chkBxTrue.Checked;
                    database.Save();
                    MessageBox.Show("Question saved", "Progress saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    database.Add($"#{database.Count + 1}", true);
                    numUpDown.Maximum = database.Count;
                    numUpDown.Value = database.Count;
                    bQuestionSaved = false;
                }
            }

            else
            {
                database.Add($"#{database.Count + 1}", true);
                numUpDown.Maximum = database.Count;
                numUpDown.Value = database.Count;
                bQuestionSaved = false;
            }

        }

//Сделал предупреждение, о последнем вопросе, чтобы не вылетала ошибка, когда выходим за границы допустимых значений
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (numUpDown.Value == 1)
            {
                MessageBox.Show("There are no questions to delete", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                database.Remove((int)numUpDown.Value - 1);
                numUpDown.Maximum--;
                numUpDown.Value--;
            }

        }

        private void miAboutTheProgramm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Joe Haldon\n" + "Version 1.1\n"
                             + "Copy right: Creative common\n" + "Release year: 2022\n" + "Latest update: 24.02.2022",
                               "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                databaseOld = database;
                database = new TrueFalse(saveFile.FileName);
                for (int i =0; i < databaseOld.Count; i++)
                {
                    database.Add(databaseOld[i].Text, databaseOld[i].TrueFalse);
                    database.Save();
                }
                database[(int)numUpDown.Value - 1].Text = txtQuestion.Text;
                database[(int)numUpDown.Value - 1].TrueFalse = chkBxTrue.Checked;
                database.Save();
            }
        }
    }
}
