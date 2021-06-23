using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddTask : Form
    {
        public static double commads;
        public static double taks;

        MainWindow form;

        public AddTask()
        {
            InitializeComponent();
        }

        public AddTask(MainWindow form)
        {
            InitializeComponent();
            this.form = form;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "Целое число";
            dataGridView1.Rows[0].Cells[1].Value = "Выч/Упр";
            dataGridView1.Rows[0].Cells[2].Value = "кэш / не кэш";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.queue.Clear();
            for (var i = 1; i < dataGridView1.Rows.Count-1; i++)
            {
                var term = new Job();
                term.Numb = i;
               //int int1;
                if(!int.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString(),out var int1))
                {
                    int1 = 1;
                }
                taks += int1;
                term.TimeDo = int1;
                term.Type = (dataGridView1.Rows[i].Cells[1].Value == null ||
                             dataGridView1.Rows[i].Cells[1].Value.ToString().ToLower() != "упр")
                    ? OperationType.ComputingProcess
                    : OperationType.ControlOperation;

                term.Cash = (dataGridView1.Rows[i].Cells[2].Value != null &&
                             dataGridView1.Rows[i].Cells[2].Value.ToString().ToLower() == "кэш")
                    ? MemoryType.Cache
                    : MemoryType.NotCache;
                form.queue.Enqueue(term);
            }

            commads = dataGridView1.Rows.Count - 1;

            this.Close();
        }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
