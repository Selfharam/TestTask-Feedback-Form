using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Test
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        public Form1(SqlConnection connect)
        {
            InitializeComponent();
            connection = connect;
            this.comboBox1.Items.AddRange(new object[] {"Техподдержка",
                        "Продажи",
                        "Другое" });
            this.comboBox1.SelectedIndex = 0;
            //this.textBox6.ReadOnly = true;
            this.textBox3.MaxLength = 15;
        }

        
        private void InsertToSql(string insertString)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = insertString;
            //this.textBox6.Text = command.CommandText;
            command.Connection = connection;

            command.Connection.Open();
            command.ExecuteNonQuery();


            command.Connection.Close();
        }

        private bool IsContact(string dataString)//проверка phone + Email
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = dataString;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                {
                    command.Connection.Close();
                    return false;
                }
            command.Connection.Close();
            return true;

            
        }

        private int amountId(string TableName)//кол-во строк в таблице + 1
        {
            int i = 1;
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM " + TableName;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) i++;
            
            command.Connection.Close();
            return i;

            
        }

        private int GetUserId(string PE)//
        {
            int id;
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT Id FROM Contacts Where " + PE;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader.GetInt32(0);
            command.Connection.Close();
            return id;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            //this.textBox6.Text = this.textBox4.Text;
            string dataString;
            string insertString;
            string mesString;
            string mesString1;

            
            String Name = this.textBox1.Text;
            String Email = this.textBox2.Text;
            String Phone = this.textBox3.Text;
            int ThemeId = this.comboBox1.SelectedIndex + 1;

            Validator validator = new Validator();

            //validator.checkEmail(Email);
           // validator.checkPhone(Phone);
            //validator.checkName(Name);
            bool filled = this.Controls.OfType<TextBox>().Any(textBox => textBox.TextLength == 0);
            

            if (!filled)
            {
                if(validator.checkEmail(Email) && validator.checkPhone(Phone))
                {
                    String Message = this.textBox4.Text;
                    String PhoneEmail = "Phone='" + Phone + "'" + " AND Email='" + Email + "'";


                    //this.textBox6.Text = amountId("Contacts").ToString();



                    //insert into dbo.Contacts(Id, ContName, Email, Phone) values (1, 'John', 'jphn@Doe', '81912204707')


                    dataString = "SELECT * FROM Contacts WHERE Phone='" + Phone + "'" + " AND Email='" + Email + "'";


                    //insertString=

                    if (IsContact(dataString))
                    {
                        //"INSERT INTO Contacts(Id, ContName, Email, Phone) VALUES (" + amountId() + ", 'Petr', 'Petr@Doe', '81455884865')";
                        insertString = "INSERT INTO Contacts(Id, ContName, Email, Phone) VALUES (" + amountId("Contacts") + ", '" + Name + "', '" + Email + "', '" + Phone + "')";
                        InsertToSql(insertString);
                        mesString = "INSERT INTO MessagesPool(Id, Id_theme, Id_contact, MessageText) VALUES (" + amountId("MessagesPool") + ", '" + ThemeId + "', '" + (amountId("Contacts") - 1) + "', '" + Message + "')";
                        InsertToSql(mesString);
                    }
                    else
                    {
                        mesString1 = "INSERT INTO MessagesPool(Id, Id_theme, Id_contact, MessageText) VALUES (" + amountId("MessagesPool") + ", '" + ThemeId + "', '" + GetUserId(PhoneEmail) + "', '" + Message + "')";
                        InsertToSql(mesString1);
                    }
                }
                else MessageBox.Show("Проверить правильность заполнения полей Телефон и Мэйл","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }    
            else MessageBox.Show("Заполните все поля", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);







            


            //select *  from Contacts where Phone='81912204707' AND Email='jphn@Doe'

           
            


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 1)
            {
                textBox3.Text = textBox3.Text + "-";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            if (textBox3.Text.Length == 5)
            {
                textBox3.Text = textBox3.Text + "-";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            if (textBox3.Text.Length == 9)
            {
                textBox3.Text = textBox3.Text + "-";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            if (textBox3.Text.Length == 12)
            {
                textBox3.Text = textBox3.Text + "-";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        
    }
}