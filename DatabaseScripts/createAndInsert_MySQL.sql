drop database if exists AnReshProbation;
create database  AnReshProbation;

use AnReshProbation;
drop table if exists EmployeeSkills;
drop table if exists Users;
drop table if exists Employee;
drop table if exists Department;
drop table if exists Skills;

create table Department (
	Id int not null auto_increment primary key,
	Pid int not null,
	Name varchar(100) not null);

insert Department (Pid, Name)
values
	(0,'Администрация'),
    (0,'Отдел кадров'),
    (0,'Отдел маркетинга'),
    (0,'Отдел продаж'),
    (0,'Отдел финансов'),
    (0,'Отдел логистики'),
    (0,'Отдел ИТ'),
    (0,'Отдел закупок'),
	(1,'Сектор 1'),
	(1,'Сектор 2'),
	(2,'Сектор 1'),
	(2,'Сектор 2'),
	(3,'Сектор 1'),
	(3,'Сектор 2'),
	(4,'Сектор 1'),
	(4,'Сектор 2'),
	(5,'Сектор 1'),
	(6,'Сектор 1'),
	(7,'Сектор 1'),
	(8,'Сектор 1');

insert Department(Pid, Name)
values
	(9,'Группа 1'),
	(10,'Группа 1'),
	(11,'Группа 1'),
	(12,'Группа 1'),
	(13,'Группа 1'),
	(14,'Группа 1'),
	(15,'Группа 1'),
	(16,'Группа 1'),
	(17,'Группа 1'),
	(18,'Группа 1'),
	(19,'Группа 1'),
	(20,'Группа 1'),
	(9,'Группа 2'),
	(10,'Группа 2'),
	(11,'Группа 2'),
	(12,'Группа 2'),
	(13,'Группа 2'),
	(14,'Группа 2'),
	(15,'Группа 2'),
	(16,'Группа 2'),
	(17,'Группа 2'),
	(18,'Группа 2'),
	(19,'Группа 2'),
	(20,'Группа 2');
	
create table Employee (
	Id int not null auto_increment primary key,
	Full_name varchar(250) not null,
	Id_department int not null ,
	Salary int not null default 12392,
    foreign key (Id_department) references Department(Id) on delete cascade);
    
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Macey K. Waller',22,169970),
  ('Buckminster H. Brennan',30,81338),
  ('Caleb L. Duncan',22,85974),
  ('Xena K. Fuentes',37,124188),
  ('Kiona W. Mitchell',21,193225),
  ('Clementine C. Blake',43,58810),
  ('Quinlan Z. Benson',25,189181),
  ('Jacob L. Rios',31,176251),
  ('Darrel B. Winters',37,192787),
  ('Ishmael J. Quinn',25,165573);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Amber C. Harper',42,24981),
  ('Xenos L. Holmes',31,16649),
  ('Carly R. Daugherty',41,13097),
  ('Imogene F. Douglas',32,123013),
  ('Myles B. Mercer',37,109380),
  ('Fatima Z. Vincent',39,144482),
  ('Sade C. Morrow',30,75789),
  ('Cameron W. Romero',39,162387),
  ('Mariam C. Riddle',44,94174),
  ('Molly Q. Roy',26,50095);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Xanthus C. O''connor',26,174029),
  ('Florence Y. Atkinson',40,44530),
  ('Arsenio O. Payne',33,31008),
  ('Berk V. Sampson',42,195539),
  ('Penelope P. Hamilton',43,112727),
  ('Colin J. King',35,160209),
  ('Hilary U. Delgado',43,189395),
  ('Fatima U. Bowman',30,155210),
  ('Emery L. Crawford',37,112514),
  ('Hiroko I. Woods',42,87017);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Hop Q. Cabrera',31,91685),
  ('Colorado W. Cantrell',34,182967),
  ('Jermaine D. Morris',33,107044),
  ('Cooper D. Barr',40,168789),
  ('Danielle A. Hubbard',22,115408),
  ('Velma R. Riley',41,108148),
  ('Shelley J. Branch',40,58611),
  ('Dorothy G. Suarez',28,98523),
  ('September I. Raymond',24,120499),
  ('Colorado Z. Simon',39,111827);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Sophia G. Pacheco',24,86322),
  ('Bree U. Cooper',30,65112),
  ('Keegan M. Young',25,31198),
  ('Dawn Q. Weeks',41,42020),
  ('Bethany E. Marks',30,129792),
  ('Tobias W. Stanley',29,172389),
  ('Minerva F. Allen',23,47635),
  ('Olivia Q. Parker',44,133059),
  ('Yardley F. Wiley',35,196022),
  ('Carter W. Stanley',34,154521);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Jena K. Osborn',26,107978),
  ('Elvis W. Woods',27,29308),
  ('Calista U. Howard',27,123164),
  ('Bernard N. Freeman',28,83450),
  ('Rhoda E. Sanchez',36,87657),
  ('Robin B. Fletcher',32,190512),
  ('Brent T. Marks',29,187221),
  ('Sybil T. Marquez',34,134194),
  ('Selma W. Mcfarland',27,80137),
  ('Hadassah Q. Goff',28,93120);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Keegan S. Ratliff',30,13049),
  ('Unity E. Hampton',22,22616),
  ('Nasim O. Michael',24,175372),
  ('Lenore M. Kirk',38,69871),
  ('Christian B. Mccormick',39,89260),
  ('Jonas I. Bentley',32,127263),
  ('Lunea J. Dickson',23,72222),
  ('Garth Q. Cash',23,139529),
  ('Adara B. Lowery',33,119888),
  ('Aurelia B. Rodriguez',36,33188);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Brett D. Osborne',39,139616),
  ('Patricia E. Acevedo',30,165673),
  ('Lars O. Quinn',37,186901),
  ('Dale I. Bond',37,112001),
  ('Briar U. Decker',36,110954),
  ('Xanthus M. David',36,76521),
  ('Chase U. Clayton',44,173772),
  ('Avye S. Barnett',22,158234),
  ('Dora I. Vega',25,28656),
  ('Olympia X. Leblanc',27,195786);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Ray J. Buckley',25,100542),
  ('Barclay S. Paul',41,82444),
  ('Cullen M. Clarke',42,151543),
  ('Boris I. Mcgowan',24,179624),
  ('Tiger E. Sawyer',41,176268),
  ('Autumn B. Whitney',43,25697),
  ('Charity H. Ford',34,85389),
  ('Camden G. Aguirre',22,130334),
  ('Shaine X. Mclean',28,161766),
  ('Shellie R. Conner',22,125917);
INSERT INTO Employee (Full_name,Id_department,Salary)
VALUES
  ('Jordan C. Sykes',27,189700),
  ('Alvin N. May',26,165607),
  ('Scarlet N. Mason',36,186032),
  ('Yoshio Q. Randolph',42,43414),
  ('Camden B. Craft',41,36638),
  ('Sacha W. Hendrix',23,61563),
  ('Holmes R. Hickman',38,152273),
  ('Chelsea V. Hawkins',41,102688),
  ('Chaim H. Knox',30,58534),
  ('Orli M. Holloway',26,19311);

create table Skills (
	Id int not null auto_increment primary key,
	Skill_name varchar(250) not null unique);

INSERT Skills (Skill_name)
	VALUES
	('C#'),
	('JavaScript'),
	('SQL');

create table EmployeeSkills (
	Id_employee int not null,
	Id_skills int not null,
    foreign key (Id_employee) references Employee(Id) on delete cascade,
    foreign key (Id_skills) references Skills(Id) on delete cascade);

INSERT EmployeeSkills (Id_employee, Id_skills)
	VALUES
	(1, 1),
	(1, 2),
	(1, 3),
	(2, 2),
	(3, 2),
	(3, 1),
	(4, 2),
	(5, 1),
	(6, 1),
	(7, 2),
	(7, 3),
	(8, 1),
	(9, 2),
	(10, 3),
	(11, 2),
	(12, 3),
	(13, 2),
	(13, 3),
	(14, 1),
	(15, 2),
	(15, 3),
	(16, 1),
	(17, 1),
	(17, 3),
	(18, 3),
	(19, 1),
	(19, 2),
	(19, 3),
	(20, 2),
	(20, 1),
	(21, 1),
	(21, 2),
	(21, 3),
	(22, 2),
	(23, 2),
	(23, 1),
	(24, 2),
	(25, 1),
	(26, 1),
	(27, 2),
	(27, 3),
	(28, 1),
	(29, 2),
	(30, 3),
	(31, 2),
	(32, 3),
	(33, 2),
	(33, 3),
	(34, 1),
	(35, 2),
	(35, 3),
	(36, 1),
	(37, 1),
	(37, 3),
	(38, 3),
	(39, 1),
	(39, 2),
	(39, 3),
	(40, 2),
	(40, 1);

 create table Users (
	Id int not null auto_increment primary key,
	Login varchar(50) not null unique,
	Password varchar(50) not null);

INSERT Users (Login,Password)
	VALUES
	('login', 'password');