using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using static System.Console;
using AddupMarksDLL;
using System.Data.SqlClient;
using System.Threading;
using System.Data;

namespace TestPortal
{
    class TestPortal
    {
                
        static void Main(string[] args)
        {
          
            Portal();
            Lecturer lec = new Lecturer();
            Thread myThread = new Thread(new ThreadStart(lec.lecLogin));
            myThread.Start();            
        }

        static void Portal()
        {
            welcome wel = new welcome();        //Welcome message
            wel.WelcomeMessage();               //Calls the Welcome Message Constructor

            Options ops = new Options();        //Lecturer or Learner Login Option
            ops.login(); //login method

            Options lecOptions = new Options();  //options constructor has lecture options, setup test and take test inside of it
            lecOptions.lecOptions();             //lecturer options method, for setup test, take test or view student details
        }
    }
    class welcome       //Class that dispay the welcome message
    {
        public void WelcomeMessage()
        {
            WriteLine("*************************");
            WriteLine("* \t\t\t*");
            WriteLine("* Welcome to TestPortal *");
            WriteLine("* \t\t\t*");
            WriteLine("*************************");
        }
    }
    class Options : marks  //class holds test setup and take test methods
    {
        public static List<string> questions = new List<string>();    //stores the tests questions

        public static List<string> options = new List<string>();      //stores the options of the test

        public static List<string> correctAns = new List<string>();   //Stores Lecturers correct answers

        public static List<string> stu1Ans = new List<string>();   //Stores student 1s answers
        public static List<string> stu2Ans = new List<string>();   //Stores student 2s answers
        public static List<string> stu3Ans = new List<string>();   //Stores student 3s answers

        public static List<string> allAnswer1s = new List<string>();   //Stores student 3s answers
        public static List<string> allAnswer2s = new List<string>();   //Stores student 3s answers
        public static List<string> allAnswer3s = new List<string>();   //Stores student 3s answers

        public static List<dynamic> stu1marks = new List<dynamic>();   //stores student 1s marks
        public static List<dynamic> stu2marks = new List<dynamic>();   //stores student 2s marks
        public static List<dynamic> stu3marks = new List<dynamic>();   //stores student 3s marks

        public static List<string> stringOptions = new List<string>();   //stores string options such as A,B and C, for foreach loop usage

        
        //lecturer options method
        public void lecOptions()
        {
            
            //Threading


            dynamic userOption; //dynamic variable
            dynamic testSetup = 1;      //test setup is assigned the value of 1
            dynamic details = 2;        //student details is assigned the value of 2
          

                Console.Write("\nLecturer Options: \nEnter any Selection Below: 1 or 2 \n1. Test Setup \n2. View Student Details \n\n");
                Console.Write("Select: ");
                userOption = int.Parse(ReadLine());


                //tests if use selection is valid (only takes 1 or 2)
                while (userOption != testSetup && userOption != details)
                {
                    Console.Beep(800, 800); //error beep sound
                    WriteLine("INCORRECT Selection! Please enter 1 or 2 \n");
                    Console.Write("Select: ");
                    userOption = int.Parse(ReadLine());
                }

                if (userOption == testSetup)
                {
                    setupTest(userOption, testSetup, details);      //Allow the lecturer to setup a test
                }
                else if (userOption == details)
                {
                    ViewDetails view = new ViewDetails();
                    view.StuDetails();      //Views Student details name,surname,stu num and marks
                }
        }

        public void setupTest(dynamic userOption, dynamic testSetup, dynamic details)
        {

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";

                con.Open();
                //deletes the previous test before creating a NEW one
                string deleteQuery = "DELETE FROM SetupTest WHERE testNum != 0";
                SqlCommand command = new SqlCommand(deleteQuery, con);
                SqlDataReader dr = command.ExecuteReader();

                con.Close();

                //Stores the questions 1-3
                string q1, q2, q3;

                //stores the options
                string q1Options; //Q1 options
                string q2Options; //Q2 options
                string q3Options; //Q3 options

                //stores the correct answers
                string ans1, ans2, ans3;

                if (userOption == testSetup) //option1
                {
                    Clear(); //clears the screen for readability


                    Console.WriteLine("Test Setup: ");

                    WriteLine("************************");
                    Console.Write("Enter Question 1: \n> ");  //Lecturer enters Q1
                    q1 = ReadLine();

                    //string options with the list
                    stringOptions.Add("A");
                    stringOptions.Add("B");
                    stringOptions.Add("C");

                    foreach (string i in stringOptions)     //Q1 options loop x3
                    {
                        Console.Write("\nOption {0} > ", i); //increments by 1 every loop
                        q1Options = ReadLine();
                        options.Add(q1Options);
                    }

                    Console.Write("\nCorrect Answer: A,B or C \n> ");  //Lecturers Correct option for Q1
                    ans1 = ReadLine().ToUpper(); //converts to upper case

                    //Lecturer needs to input ONLY A,B OR C
                    while (ans1 != "A" && ans1 != "B" && ans1 != "C")  //user needs to enter these in order to proceed
                    {
                        Console.Write("\nInvalid Entry! Enter ONLY A,B or C \n> ");
                        ans1 = ReadLine().ToUpper(); //converts to upper case
                    }
                    correctAns.Add(ans1);


                    WriteLine("************************");
                    Console.Write("\nEnter Question 2: \n> ");     //Lecturer enters Q2
                    q2 = Console.ReadLine();

                    foreach (string i in stringOptions)     //Q1 options loop x3
                    {
                        Console.Write("\nOption {0} > ", i); //increments by 1 every loop
                        q2Options = ReadLine();
                        options.Add(q2Options);
                    }
                    Console.Write("\nCorrect Answer: A,B or C \n> ");  //Lecturers Correct option for Q2
                    ans2 = ReadLine().ToUpper(); //converts to upper case

                    //Lecturer needs to input ONLY A,B OR C
                    while (ans2 != "A" && ans2 != "B" && ans2 != "C")  //user needs to enter these in order to proceed
                    {
                        Console.Write("\nInvalid Entry! enter ONLY A,B or C \n> ");
                        ans2 = Console.ReadLine().ToUpper(); //converts to upper case
                    }
                    correctAns.Add(ans2);

                    WriteLine("************************");
                    Console.Write("\nEnter Question 3: \n> ");     //Lecturer enters Q2
                    q3 = Console.ReadLine();


                    foreach (string i in stringOptions)     //Q1 options loop x3
                    {
                        Console.Write("\nOption {0} > ", i); //increments by 1 every loop
                        q3Options = ReadLine();
                        options.Add(q3Options);
                    }
                    Console.Write("\nCorrect Answer: A,B or C \n> ");  //Lecturers Correct option for Q1
                    ans3 = ReadLine().ToUpper(); //converts to upper case

                    //Lecturer needs to input ONLY A,B OR C
                    while (ans3 != "A" && ans3 != "B" && ans3 != "C")  //user needs to enter these in order to proceed
                    {
                        Console.Write("\nInvalid Entry! enter ONLY A,B or C \n> ");
                        ans3 = ReadLine().ToUpper(); //converts to upper case
                    }
                    correctAns.Add(ans3);

                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
                    connection.Open();

                    //INSERTS to DATABASE!!! for all Qs
                    string query = "INSERT INTO SetupTest" + $"(Question1,option1Q1,option2Q1,option3Q1,correctAnswerQ1,"
                                                        + "Question2,option1Q2,option2Q2,option3Q2,correctAnswerQ2,"
                                                        + "Question3,option1Q3,option2Q3,option3Q3,correctAnswerQ3) VALUES"
                    + $"( '{q1}', ' {options[0].ToString()}',' {options[1].ToString()}','{options[2].ToString()} ', '{correctAns[0].ToString()}','"
                    + $"{q2}', ' {options[3].ToString()}',' {options[4].ToString()}','{options[5].ToString()} ', '{correctAns[1].ToString()}','"
                    + $"{q3}', ' {options[6].ToString()}',' {options[7].ToString()}','{options[8].ToString()} ', '{correctAns[2].ToString()}' )";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    connection.Close();

                    WriteLine();
                    WriteLine("***********************");
                    WriteLine("*                     *");
                    WriteLine("* Test Setup Complete *");
                    WriteLine("*                     *");
                    WriteLine("***********************");


                    string yes = "Y";   //holds the value of yes
                    string no = "N";    //holds the value of no
                    string response;

                    Write("\nStudent Login: Enter Y/N \n> ");
                    Write("NOTE: 'N' exits the program \n> ");
                    response = ReadLine();

                    while (response.ToUpper() != no && response.ToUpper() != yes)
                    {
                        Write("Incorrect Input! Enter Y or N...ONLY \n> ");
                        response = ReadLine().ToUpper();
                    }

                    if (response.ToUpper() == no)
                    {
                        //Sentinal value, ends the program
                        Environment.Exit(0);
                    }
                    else if (response.ToUpper() == yes)
                    {
                        Student LogIn = new Student();  //Calls student login method
                        
                        LogIn.stuLogin();
                    }
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error..." + exc.Message);
            }

        
        }
        public void takeTest(dynamic stuNum)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
            connection.Open();

            //Reads from DATABASE!!! for all Qs
            string query = "SELECT Question1,option1Q1,option2Q1,option3Q1,"
                                + "Question2,option1Q2,option2Q2,option3Q2,"
                                + "Question3,option1Q3,option2Q3,option3Q3 FROM SetupTest";


            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            //Reads the test from the Database (Qs and options)
            while (reader.Read()) //LOOPS 3x
            {
                questions.Add(reader["Question1"].ToString());
                questions.Add(reader["Question2"].ToString());
                questions.Add(reader["Question3"].ToString());

                options.Add(reader["option1Q1"].ToString());
                options.Add(reader["option2Q1"].ToString());
                options.Add(reader["option3Q1"].ToString());

                options.Add(reader["option1Q2"].ToString());
                options.Add(reader["option2Q2"].ToString());
                options.Add(reader["option3Q2"].ToString());

                options.Add(reader["option1Q3"].ToString());
                options.Add(reader["option2Q3"].ToString());
                options.Add(reader["option3Q3"].ToString());

            }
            connection.Close();

            //student takes the test in this method
            string answer1, answer2, answer3;
            
            Write("\nTEST: enter A,B or C...ONLY \n");

            if (stuNum == "13019459")   //compares stu1s login details with input from the user
            {

                connection.Open();

                //deletes the previous student answers before creating a NEW ones
                string deleteQuery = "DELETE FROM TakeTest WHERE ansNum != 0";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                SqlDataReader dr = command.ExecuteReader();

                connection.Close();

                //Q1
                Write("\nQuestion 1 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[0], options[0], options[1], options[2]);    //displays the questions
                answer1 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer1 != "A" && answer1 != "B" && answer1 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer1 = ReadLine().ToUpper(); //converts answers to upper case
                }
                stu1Ans.Add(answer1);               //Stores learner 1s Q1 answer in the List

                //Q2
                Write("\nQuestion 2 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[1], options[3], options[4], options[5]);    //displays the questions
                answer2 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer2 != "A" && answer2 != "B" && answer2 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer2 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu1Ans.Add(answer2);                 //Stores learner 1s Q2 answer in the List

                //Q3
                Write("\nQuestion 3 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[2], options[6], options[7], options[8]);    //displays the questions
                answer3 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer3 != "A" && answer3 != "B" && answer3 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer3 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu1Ans.Add(answer3);                 //Stores learner 1s Q3 answer in the List


                connection.Open();
                //INSERTS to DATABASE!!! for student1
                string q1Query = "INSERT INTO TakeTest" + $"(stu1Answer1,stu1Answer2,stu1Answer3) VALUES"
                                                        + $"( '{stu1Ans[0].ToString()}', "
                                                        + $" '{stu1Ans[1].ToString()}',"
                                                        + $" '{stu1Ans[2].ToString()}' )";    //stores student 1s answers



                SqlCommand c1 = new SqlCommand(q1Query, connection);
                SqlDataReader dr1 = c1.ExecuteReader();

                connection.Close();

            }
            else if(stuNum == "15019459")   //compares stu2s login details with input from the user
            {
                connection.Open();

                //deletes the previous student answers before creating a NEW ones
                string deleteQuery = "DELETE FROM TakeTest WHERE ansNum != 0";
                SqlCommand command2 = new SqlCommand(deleteQuery, connection);
                SqlDataReader dr2 = command2.ExecuteReader();

                connection.Close();

                //Q1
                Write("\nQuestion 1 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[0], options[0], options[1], options[2]);    //displays the questions
                answer1 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer1 != "A" && answer1 != "B" && answer1 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer1 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu2Ans.Add(answer1);                 //Stores learner 2s Q1 answer in the List

                //Q2
                Write("\nQuestion 2 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[1], options[3], options[4], options[5]);    //displays the questions
                answer2 = ReadLine().ToUpper(); //conver ts answers to upper case

                while (answer2 != "A" && answer2 != "B" && answer2 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer2 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu2Ans.Add(answer2);                 //Stores learner 2s Q2 answer in the List

                //Q3
                Write("\nQuestion 3 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[2], options[6], options[7], options[8]);    //displays the questions
                answer3 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer3 != "A" && answer3 != "B" && answer3 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! Enter ONLY A,B or C \n> ");
                    answer3 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu2Ans.Add(answer3);                 //Stores learner 2s Q3 answer in the List


                connection.Open();
                //INSERTS to DATABASE!!! for student2
                string q2Query = "INSERT INTO TakeTest " + $"(stu2Answer1,stu2Answer2,stu2Answer3) VALUES"
                                                        + $"( '{stu2Ans[0].ToString()}', "
                                                        + $" '{stu2Ans[1].ToString()}',"
                                                        + $" '{stu2Ans[2].ToString()}' )";    //stores student 2s answers    //stores student 3s answers



                SqlCommand c2 = new SqlCommand(q2Query, connection);
                SqlDataReader dReader2 = c2.ExecuteReader();
                connection.Close();

            }
            else if (stuNum == "16019459")  //compares stu3s login details with input from the user
            {
                connection.Open();

                //deletes the previous student answers before creating a NEW ones
                string deleteQuery = "DELETE FROM TakeTest WHERE ansNum != 0";
                SqlCommand command3 = new SqlCommand(deleteQuery, connection);
                SqlDataReader dr3 = command3.ExecuteReader();

                connection.Close();
                //Q1
                Write("\nQuestion 1 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[0], options[0], options[1], options[2]);    //displays the questions
                answer1 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer1 != "A" && answer1 != "B" && answer1 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer1 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                //stu3Ans.Add(answer1);                 //Stores learner 3s Q1 answer in the List

                //Q2
                Write("\nQuestion 2 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[1], options[3], options[4], options[5]);    //displays the questions
                answer2 = ReadLine().ToUpper(); //conver ts answers to upper case

                while (answer2 != "A" && answer2 != "B" && answer2 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer2 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu3Ans.Add(answer2);                 //Stores learner 3s Q2 answer in the List

                //Q3
                Write("\nQuestion 3 : {0} \nA. {1} \nB. {2} \nC. {3} \n> ", questions[2], options[6], options[7], options[8]);    //displays the questions
                answer3 = ReadLine().ToUpper(); //converts answers to upper case

                while (answer3 != "A" && answer3 != "B" && answer3 != "C")  //user needs to enter these in order to proceed
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.WriteLine("Invalid Entry! enter ONLY A,B or C \n> ");
                    answer3 = ReadLine().ToUpper(); //conver ts answers to upper case
                }
                stu3Ans.Add(answer3);                 //Stores learner 3s Q3 answer in the List

                connection.Open();
                //INSERTS to DATABASE!!! for student3
                string q3Query = "INSERT INTO TakeTest" + $"(stu3Answer1,stu3Answer2,stu3Answer3) VALUES"
                                                        + $"( '{stu3Ans[0].ToString()}', "
                                                        + $" '{stu3Ans[1].ToString()}',"
                                                        + $" '{stu3Ans[2].ToString()}' )";    //stores student 3s answers   //stores student 3s answers

                SqlCommand c3 = new SqlCommand(q3Query, connection);
                SqlDataReader dRead3 = c3.ExecuteReader();
                connection.Close();
            }


            ////Reads the ANSWERS from database and inserts the data into the ArrayList (For ALL STUDENTS)
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";

            //string ans = "SELECT stu1Answer1,stu1Answer2,stu1Answer3,stu2Answer1,stu2Answer2,stu2Answer3,stu3Answer1,stu3Answer2,stu3Answer3 FROM TakeTest";
            //SqlCommand com = new SqlCommand(ans, con);
            //SqlDataReader r = com.ExecuteReader();
            //while (r.Read()) //LOOPS 3x
            //{
            //    allAnswer1s.Add(r["stu1Answer1"].ToString());
            //    allAnswer2s.Add(r["stu1Answer2"].ToString());
            //    allAnswer3s.Add(r["stu1Answer3"].ToString());
            //}
            //r.Close();


            //test complete message
            WriteLine();
            WriteLine("*****************");
            WriteLine("*               *");
            WriteLine("* Test Complete *");
            WriteLine("*               *");
            WriteLine("*****************");
           
            
            login();    //login method (Lecturer or Learner)
        }
        public void viewMarks(dynamic stuNum)
        {
            
            int mark1 = 0;
            int mark2 = 0;
            int mark3 = 0;
            
                if (stuNum == "13019459")    //student 1s marks if user logged in
                {
                    //Marking for Q1
                    if (stu1Ans[0] == correctAns[0]) //compares the students answers with the lecturers correct answer
                    {
                        mark1 += 1;
                    }
                    else
                    {
                        mark1 += 0;
                    }

                    //Marking for Q2
                    if (stu1Ans[1] == correctAns[1]) //compares the students answers with the lecturers correct answer
                    {
                        mark2 += 1;
                    }
                    else
                    {
                        mark2 += 0;
                    }


                    //Marking for Q3
                    if (stu1Ans[2] == correctAns[2]) //compares the students answers with the lecturers correct answer
                    {
                        mark3 += 1;
                    }
                    else
                    {
                        mark3 += 0;
                    }

                    stu1marks.Add((marks.addupMarks(mark1, mark2, mark3)));  //adds the students total marks from the DLL to the list

                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
                    connection.Open();

                    //Updates Marks to DATABASE!!! for the logged in student
                    string query = "UPDATE studentDetails set Marks = '" + stu1marks[0].ToString() + "' WHERE StudentNum = '" + 13019459 + "' ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    connection.Close();

            }
                else if (stuNum == "15019459")  //student 1s marks if user logged in
                {
                    //Marking for Q1
                    if (stu2Ans[0] == correctAns[0]) //compares the students answers with the lecturers correct answer
                    {
                        mark1 += 1;
                    }
                    else
                    {
                        mark1 += 0;
                    }

                    //Marking for Q2
                    if (stu2Ans[1] == correctAns[1]) //compares the students answers with the lecturers correct answer
                    {
                        mark2 += 1;
                    }
                    else
                    {
                        mark2 += 0;
                    }


                    //Marking for Q3
                    if (stu2Ans[2] == correctAns[2]) //compares the students answers with the lecturers correct answer
                    {
                        mark3 += 1;
                    }
                    else
                    {
                        mark3 += 0;
                    }

                stu2marks.Add((marks.addupMarks(mark1,mark2,mark3))); //adds the students total marks from the DLL to the list

                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
                connection.Open();

                //Updates Marks to DATABASE!!! for the logged in student
                string query = "UPDATE studentDetails set Marks = '" + stu2marks[0].ToString() + "' WHERE StudentNum = 15019459 ";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                connection.Close();
            }
                else if (stuNum == "16019459")  //student 1s marks if user logged in
                {
                    //Marking for Q1
                    if (stu3Ans[0] == correctAns[0]) //compares the students answers with the lecturers correct answer
                    {
                        mark1 += 1;
                    }
                    else
                    {
                        mark1 += 0;
                    }

                    //Marking for Q2
                    if (stu3Ans[1] == correctAns[1]) //compares the students answers with the lecturers correct answer
                    {
                        mark2 += 1;
                    }
                    else
                    {
                        mark2 += 0;
                    }


                    //Marking for Q3
                    if (stu3Ans[2] == correctAns[2]) //compares the students answers with the lecturers correct answer
                    {
                        mark3 += 1;
                    }
                    else
                    {
                        mark3 += 0;
                    }
                stu3marks.Add((marks.addupMarks(mark1, mark2, mark3))); //adds the students total marks from the DLL to the list

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
                connection.Open();

                //Updates Marks to DATABASE!!! for the logged in student
                string query = "UPDATE studentDetails set Marks = '" + stu3marks[0].ToString() + "' WHERE StudentNum = 16019459";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                connection.Close();
            }
            //Logged in Student views their Marks
            try
            {
                SqlConnection connect = new SqlConnection();
                SqlCommand cm = new SqlCommand("SELECT Marks FROM StudentDetails", connect);

                SqlDataReader dataR = cm.ExecuteReader();

                while (dataR.Read())
                {
                    stu1marks.Add(dataR["Marks"].ToString()); //loops 3x ([0]stu1Marks, [1]stu2Marks, [2]stu3Marks
                    stu2marks.Add(dataR["Marks"].ToString());
                    stu3marks.Add(dataR["Marks"].ToString());
                }
                dataR.Close();
                connect.Close();

                if (stuNum == "13019459")
                {
                    //Displays the logged in students marks
                    WriteLine("\n***********************");
                    Console.WriteLine("Learner Marks: {0} ", stuNum);
                    Console.WriteLine("Marks: {0}/3 ", stu1marks[0]);
                    WriteLine("***********************");

                }
                else if (stuNum == "15019459")
                {
                    //Displays the logged in students marks
                    WriteLine("\n***********************");
                    Console.WriteLine("Learner Marks: {0} ", stuNum);
                    Console.WriteLine("Marks: {0}/3 ", stu2marks[0]);
                    WriteLine("***********************");
                }
                else if (stuNum == "16019459")
                {
                    //Displays the logged in students marks
                    WriteLine("\n***********************");
                    Console.WriteLine("Learner Marks: {0} ", stuNum);
                    Console.WriteLine("Marks: {0}/3 ", stu3marks[0]);
                    WriteLine("***********************");
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error " + exc.Message);
            }
            finally
            {
                login(); //login method (Lecturer or Learner)
            }
            
        }
        public void viewMemo(dynamic stuNum)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = HP-I3\\SQLEXPRESS; Database = TestPortal; integrated security = SSPI";
            connection.Open();

            //Reads Memo from DATABASE (Question, student answer and correct answer)
            string query1 = "SELECT Question1,Question2,Question3"  //add correct answer from TakeTest table
                            + "correctAnsQ1,correctAnsQ2,correctAnsQ3"
                            + "stu1Answer1,stu1Answer2,stu1Answer3"
                            + "stu2Answer1,stu2Answer2,stu2Answer3"
                            + "stu3Answer1,stu3Answer2,stu3Answer3 FROM SetupTest";

            SqlCommand cmd1 = new SqlCommand(query1, connection); 
            SqlDataReader reader = cmd1.ExecuteReader();    //Reads test Qs and correct ans
            

            //Reads the database and inserts the data into the ArrayList (For ALL STUDENTS)
            while (reader.Read()) //LOOPS 3x
            {
                questions.Add(reader["Question1"].ToString());
                questions.Add(reader["Question2"].ToString());
                questions.Add(reader["Question3"].ToString());

                correctAns.Add(reader["correctAnsQ1"].ToString());
                correctAns.Add(reader["correctAnsQ2"].ToString());
                correctAns.Add(reader["correctAnsQ3"].ToString());

                stu1Ans.Add(reader["stu1Answer1"].ToString());
                stu1Ans.Add(reader["stu1Answer2"].ToString());
                stu1Ans.Add(reader["stu1Answer3"].ToString());

                stu2Ans.Add(reader["stu2Answer1"].ToString());
                stu2Ans.Add(reader["stu2Answer2"].ToString());
                stu2Ans.Add(reader["stu2Answer3"].ToString());

                stu3Ans.Add(reader["stu3Answer1"].ToString());
                stu3Ans.Add(reader["stu3Answer2"].ToString());
                stu3Ans.Add(reader["stu3Answer3"].ToString());
            }

            
            //reader2.Close();
            try
            {
                if (stuNum == "13019459")    //displays memo for the logged in students marks (13019459)
                {
                    WriteLine("\n********************************");
                    Console.WriteLine("Memorandum: ");
                    Console.WriteLine("\nQuestion 1: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[0], stu1Ans[0], correctAns[0]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 2: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[1],stu2Ans[0], correctAns[0]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 3: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[2], stu3Ans[0], correctAns[0]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");


                }
                else if (stuNum == "15019459")   //displays memo for the logged in students marks (15019459)
                {
                    WriteLine("\n********************************");
                    Console.WriteLine("Memorandum: ");
                    Console.WriteLine("\nQuestion 1: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[0], stu2Ans[0], correctAns[0]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 2: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[1], stu2Ans[0], correctAns[1]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 3: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[2], stu2Ans[0], correctAns[2]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");
                }
                else if (stuNum == "16019459")  //displays memo for the logged in students marks (16019459)
                {
                    WriteLine("\n********************************");
                    Console.WriteLine("Memorandum: ");
                    Console.WriteLine("\nQuestion 1: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[0], stu3Ans[0], correctAns[0]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 2: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[1], stu3Ans[0], correctAns[1]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");

                    Console.WriteLine("\nQuestion 3: {0} \nStudent Answer: {1} \nCorrect Answer: {2}", questions[2], stu3Ans[0], correctAns[2]); //displays the test question, user answer and correct answer
                    WriteLine("********************************");
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error!!! " + exc.Message);
            }
            
            login(); //login method

        }
        public void login()
        {
            //This method offers login sel ections for both lecturer and learner
            WriteLine();
            Console.Write("Enter Login Selection:  1 or 2...\n(1) Lecturer \n(2) Learner  \n> ");
            int entry = int.Parse(ReadLine());

            try
            {
                while (entry != 1 && entry != 2)
                {
                    Console.Beep(800, 800); //error beep sound
                    Console.Write("\nInvalid entry! Enter 1 or 2 ONLY. \n> ");
                    entry = int.Parse(ReadLine());
                }

                if (entry == 1)
                {
                    Lecturer log = new Lecturer();  //Lecturer logins in again to check marks or setup another test.
                    log.lecLogin(); //calls method from Lecturer Class

                    Options Opts = new Options();
                    Opts.lecOptions();
                }
                else if (entry == 2)
                {
                    Student login = new Student(); //student logins in again to check marks and memo


                    login.stuLogin();   //Calls method from Student Class
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error!!!... " + exc.Message);
            }
            finally
            {
                login();
            }
            
        }
    }
}



