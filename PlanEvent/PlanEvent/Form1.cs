using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanEvent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var priorutetEvent = new[] { "низькій", "середній", "високій" };
            comboBox1.Items.AddRange(priorutetEvent);
            // вибір елемнта в comboBox по замовчуванню
            comboBox1.SelectedIndex = 1;
            //comboBox1.SelectedItem = "середній";
            //comboBox1.SelectedIndex = comboBox1.Items.Count - 1; // якщо вибране значення не є конкретним
            
            textBox1.Validated += TextBox1_Validated; //обробляємо помилку для пустого поля в textBox
            errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);

            //comboBox1.Validated += ComboBox1_Validated; //обробляємо помилку для пустого поля в comboBox1
            //errorProvider1.SetIconAlignment(comboBox1, ErrorIconAlignment.MiddleLeft);
        }

        //private void ComboBox1_Validated(object sender, EventArgs e)
        //{
        //    //якщо пусте поле в comboBox1, то помилка
        //    if ((sender as ComboBox).SelectedIndex == -1)
        //        errorProvider1.SetError(comboBox1, "Выберите значение из списка");
        //    else
        //        errorProvider1.SetError(comboBox1, string.Empty);
        //}

        private void TextBox1_Validated(object sender, EventArgs e)
        {
            //якщо пусте поле в textBox1, то помилка
            if (string.IsNullOrEmpty((sender as TextBox).Text))
            {
                errorProvider1.SetError(textBox1, "Потрібно заповнити поле!");
            }
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            listBox1.Items.Add(textBox2.Text);
            listBox1.Items.Add(comboBox1.SelectedItem);
            listBox1.Items.Add(monthCalendar1.SelectionRange.Start);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Серіалізація json
            EventPlan eventPlan = new EventPlan();
            eventPlan.NameEvent = textBox1.Text;
            eventPlan.PlaceEvent = textBox2.Text;
            eventPlan.PriorutetEvent = (string)comboBox1.SelectedItem;
            if (dateEnter)
            {
                eventPlan.DateEvent = monthCalendar1.SelectionRange.Start;

            }

            var json = JsonConvert.SerializeObject(eventPlan, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("eventPlan.json", json);

        }
        static bool dateEnter = true;

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (DateTime.Now < monthCalendar1.SelectionRange.Start)
            {
                dateEnter = true;
            }
            else
            {
                MessageBox.Show("Ви ввели не вірну дату! Ваша дата менше поточної дати!");
            }
        }
    }
}
