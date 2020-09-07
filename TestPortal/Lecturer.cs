using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections;
using System.Data.SqlClient;
using System.Threading;
using System.Data;

namespace TestPortal
{
    class Lecturer
    {
        public List<string> pass = new List<string>();
        
        public void lecLogin()
        {
            Thread.Sleep(500); //delays the program by half a second

            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";

            connect.Open();
            string query = "SELECT * FROM Lecturer";    //Query for getting Password from Database
            SqlCommand cmd = new SqlCommand(query, connect);
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())    //Reads the Password
            {
                pass.Add(reader["Password"].ToString());    //Adds the Password to the ArrayList
            }
            reader.Close();
            connect.Close();

            string userEntry;
            

            Write("\nLecturer Login: \nEnter your Password below: \n> ");
            userEntry = ReadLine();

            //Compares the users password and the password in the ArrayList
            while (userEntry != pass[0])
            {
                Console.Beep(800, 800); //error beep sound
                Write("\nPassword is INCORRECT! \nPassword is 'Password1' \n> ");
                userEntry = ReadLine();
            }
            WriteLine("Password is CORRECT! :)");
            WriteLine();        //Blank Line
        }
    }
}
