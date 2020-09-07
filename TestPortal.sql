CREATE TABLE studentDetails(
StudentNum char(8) NOT NULL,
Name varchar(50) NOT NULL,
Surname varchar(50) NOT NULL,
[Password] varchar(15) NOT NULL,
Marks int NOT NULL,

CONSTRAINT PK_studentDetails PRIMARY KEY (StudentNum)
);

CREATE TABLE Lecturer(
[Password] varchar(15) NOT NULL
);

CREATE TABLE SetupTest(
testNum int identity(1,1) NOT NULL,
Question1 varchar(100) NOT NULL,
option1Q1 varchar(50) NOT NULL,
option2Q1 varchar(50) NOT NULL,
option3Q1 varchar(50) NOT NULL,
correctAnswerQ1 char(1) NOT NULL,

Question2 varchar(100) NOT NULL,
option1Q2 varchar(50) NOT NULL,
option2Q2 varchar(50) NOT NULL,
option3Q2 varchar(50) NOT NULL,
correctAnswerQ2 char(1) NOT NULL,

Question3 varchar(100) NOT NULL,
option1Q3 varchar(50) NOT NULL,
option2Q3 varchar(50) NOT NULL,
option3Q3 varchar(50) NOT NULL,
correctAnswerQ3 char(1) NOT NULL,

CONSTRAINT PK_SetupTest PRIMARY KEY (testNum)
);

CREATE TABLE TakeTest(
ansNum int identity(1,1) NOT NULL,
stu1Answer1 char(1) ,
stu1Answer2 char(1) ,
stu1Answer3 char(1) ,

stu2Answer1 char(1) ,
stu2Answer2 char(1) ,
stu2Answer3 char(1) ,

stu3Answer1 char(1) ,
stu3Answer2 char(1) ,
stu3Answer3 char(1) 
);


INSERT INTO studentDetails VALUES
('13019459','Keo','Nthite','Password1',0),
('15019459','Tony','Stark','Password1',0),
('16019459','Bruce','Wayne','Password1',0);

--update studentDetails
--set Marks = 0
--where studentNum = '13019459';

--update studentDetails
--set  Marks = 0
--where studentNum = '15019459';

--update studentDetails
--set Marks = 0
--where studentNum = '16019459';

INSERT INTO Lecturer VALUES
('Password1');

INSERT INTO SetupTest VALUES
('Q1 whats my name?','keo','kev','king','A','Q2 fav color','red','blue','pink','B','Q3 fav team?','man utd','man city','chelsea','C');

INSERT INTO TakeTest VALUES
('a','b','c','d','e','g','t','y','q');

SELECT * FROM studentDetails;
SELECT * FROM Lecturer;
SELECT * FROM SetupTest;
SELECT * FROM TakeTest;

--DELETE FROM SetupTest WHERE testNum = 1

DROP TABLE studentDetails;
DROP TABLE Lecturer;
DROP TABLE SetupTest;
DROP TABLE TakeTest;



