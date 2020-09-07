using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections;
using TestPortal;
using System.Data.SqlClient;
using System.Data;


namespace TestPortal
{
    class Student 
    {
        public List<dynamic> stuNumOptions = new List<dynamic>();
        public List<dynamic> Pass = new List<dynamic>();

        public void stuLogin()
        {
            try
            {
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";

                connect.Open();
                string query = "SELECT StudentNum,Password FROM studentDetails";
                SqlCommand cmd = new SqlCommand(query, connect);

                SqlDataReader reader = cmd.ExecuteReader();

                //Reads the database and inserts the data into the ArrayList (For ALL STUDENTS)
                while (reader.Read()) //LOOPS 3x
                {
                    stuNumOptions.Add(reader["StudentNum"].ToString());
                    Pass.Add(reader["Password"].ToString());
                }
                reader.Close();
                connect.Close();

                Clear(); //clears the screen for readability

                dynamic stuNum;
                string password;

                Write("\nLearner Login: \nEnter Student Number: \n> "); //Learner inputs student num
                stuNum = ReadLine();

                while (stuNum != stuNumOptions[0].ToString() && stuNum != stuNumOptions[1].ToString() && stuNum != stuNumOptions[2].ToString())
                {
                    Console.Beep(800, 800); //error beep sound
                    Write("\nINCORRECT Student Number! \nNOTE: use the following 13019459, 15019459 or 16019459 \n> ");
                    stuNum = ReadLine();
                }
                WriteLine("Student Number is CORRECT! :)");

                //Learner inputs Password
                Write("\nEnter Password: \n> ");
                password = ReadLine();
                while (password != Pass[0])  //Compares Lecturer password against the one in the ArrayList
                {
                    Console.Beep(800, 800); //error beep sound
                    Write("\nINCORRECT Password! \nNOTE: Password is 'Password1' \n> ");
                    password = ReadLine();
                }

                //Password is correct for any of the student num options
                if (stuNum == stuNumOptions[0].ToString() || stuNum == stuNumOptions[1].ToString() || stuNum == stuNumOptions[2].ToString())
                {
                    int stuSelection;
                    WriteLine("Password is CORRECT! :)");
                    Write("\nLearner Options: \n1. Take Test \n2. View Marks \n3. View Memorandum \n> ");
                    stuSelection = int.Parse(ReadLine());


                    //compares the students entered option with the ones in the aList above
                    while (stuSelection != 1 && stuSelection != 2 && stuSelection != 3)
                    {
                        Console.Beep(800, 800); //error beep sound
                        Write("Incorrect Option! Enter 1,2 or 3 \n> ");
                        stuSelection = int.Parse(ReadLine());
                    }

                    //Calls the constructors of the test, marks and memo 
                    if (stuSelection == 1) //Compares entry to Option 1
                    {
                        Options ops = new Options();
                        ops.takeTest(stuNum);     //Calls the Take Test method
                    }
                    else if (stuSelection == 2) //Compares entry to Option 2
                    {
                        Options marks = new Options();  //Calls the view marks methods
                        marks.viewMarks(stuNum);

                        Options ops = new Options();    //Displays options
                        ops.lecOptions();
                    }
                    else if (stuSelection == 3)    //Compares entry to Option 3
                    {
                        Options memo = new Options();
                        memo.viewMemo(stuNum);     //Calls the View Memo method

                        Options ops = new Options();    //Displays options
                        ops.lecOptions();
                    }
                } 
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error! " + exc.Message);
            }
            
        }
    }
}
