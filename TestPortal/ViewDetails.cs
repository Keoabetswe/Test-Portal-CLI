using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace TestPortal
{
    class ViewDetails : Options
    {
        public static List<object> studentInfo = new List<object>();
        
        public void StuDetails()
        {
            WriteLine();    //blank line
            try
            {
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";

                connect.Open();
                SqlCommand cmd = new SqlCommand("SELECT StudentNum,Name,Surname,Marks FROM studentDetails", connect);
                SqlDataReader reader = cmd.ExecuteReader();


                //Reads the database and inserts the data into the ArrayList (For ALL STUDENTS)
                while (reader.Read()) //LOOPS 3x
                {
                    studentInfo.Add(reader["StudentNum"].ToString());
                    studentInfo.Add(reader["Name"].ToString());
                    studentInfo.Add(reader["Surname"].ToString());
                    studentInfo.Add(reader["Marks"].ToString());
                }
                reader.Close();
                connect.Close();


                int lectSelection;
                Console.Write("Student Details: Enter option 1,2 or 3 below: \n1. 13019459 \n2. 15019459 \n3. 16019459 \n");
                Console.Write("Option: ");
                lectSelection = int.Parse(Console.ReadLine());

                while (lectSelection != 1 && lectSelection != 2 && lectSelection != 3) //13019459 details
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.Write("\nInvalid Option! Select 1, 2 or 3 \n>");  //Invalid selection from user
                    lectSelection = int.Parse(Console.ReadLine());
                }

                if (lectSelection == 1) //Lecturer selects student 13019459's details
                {
                    Console.WriteLine("\n************************");
                    Console.WriteLine("Student Details");
                    Console.Write("\nStudent Num:" + studentInfo[0] + "\nName:" + studentInfo[1] + "\nSurname:" + studentInfo[2] + "\nMarks:  {0}/3 \n", studentInfo[3]);
                    Console.WriteLine("************************");
                }
                else if (lectSelection == 2) //Lecturer selects student 15019459's details
                {
                    Console.WriteLine("\n************************");
                    Console.WriteLine("Student Details");
                    Console.Write("\nStudent Num:" + studentInfo[4] + "\nName:" + studentInfo[5] + "\nSurname:" + studentInfo[6] + "\nMarks: {0}/3 \n", studentInfo[7]);
                    Console.WriteLine("************************");
                }
                else if (lectSelection == 3) //Lecturer selects student 16019459's details
                {
                    Console.WriteLine("\n************************");
                    Console.WriteLine("Student Details");
                    Console.Write("\nStudent Num:" + studentInfo[8] + "\nName:" + studentInfo[9] + "\nSurname:" + studentInfo[10] + "\nMarks: {0}/3 \n", studentInfo[11]);
                    Console.WriteLine("************************");
                }
                lecOptions();   //displays the lecturer options, setup test or view student details
            }
            catch(Exception exc)
            {
                Console.WriteLine("Incorrect! Enter 1,2 or 3...ONLY " + exc.Message);
            }
        }
    }
}
