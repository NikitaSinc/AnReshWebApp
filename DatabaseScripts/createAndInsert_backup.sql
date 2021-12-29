use master;
drop database if exists AnReshProbation;
create database  AnReshProbation;

use AnReshProbation
drop table if exists EmployeeSkills;
drop table if exists Users;
drop table if exists Employee;
drop table if exists Department;
drop table if exists Skills;

create table Department (
	Id int not null identity(1,1) primary key,
	Name varchar(100) not null);

insert Department(Name)
values
	('Администрация'),
    ('Отдел кадров'),
    ('Отдел маркетинга'),
    ('Отдел продаж'),
    ('Отдел финансов'),
    ('Отдел логистики'),
    ('Отдел ИТ'),
    ('Отдел закупок');
	
create table Employee (
	Id int not null identity(1,1) primary key,
	Full_name varchar(250) not null,
	Id_department int not null foreign key references Department(Id) on delete cascade,
	Salary int not null default 12392);
    
INSERT Employee(Full_name,Id_department,Salary)
VALUES
  ('Graham S. Baldwin',6,176030),
  ('Shelby N. Walsh',8,174982),
  ('Dalton I. Owen',4,25768),
  ('Nash S. Walls',6,127024),
  ('Dawn U. Mercer',4,31874),
  ('Alice Y. Langley',2,93255),
  ('Lisandra N. Ramos',3,150931),
  ('Sybil T. Callahan',4,176298),
  ('Ariel V. Rosales',2,186759),
  ('Yeo U. Thompson',5,101748),
  ('Melodie G. Walton',3,109411),
  ('Nathaniel Y. Ball',2,158062),
  ('Jaden R. Collins',5,139689),
  ('Oren M. Silva',4,102409),
  ('Allistair B. Cooper',5,105186),
  ('Blaze D. Webb',5,53344),
  ('Lana V. Stafford',3,178001),
  ('Ainsley Y. Fuentes',6,168466),
  ('Hashim G. Lester',5,178244),
  ('Mollie M. Mack',1,12900);
INSERT Employee (Full_name,Id_department,Salary)
VALUES
  ('Alisa S. Lee',5,182891),
  ('Eagan O. Miles',8,158931),
  ('Hilary O. Holden',3,54243),
  ('Georgia T. Leon',6,130319),
  ('Hamish J. Luna',6,148752),
  ('Coby D. Banks',2,158482),
  ('Isaiah G. Mullen',4,77220),
  ('Cameran O. Burke',4,148419),
  ('Jacqueline U. Hayes',1,107068),
  ('Demetria N. Hull',2,117529),
  ('Adara V. Johnston',8,77698),
  ('Boris C. Hull',6,67050),
  ('Brenna Y. Anderson',5,50954),
  ('Maggie X. Haley',3,61862),
  ('Palmer Y. Villarreal',5,148794),
  ('Uma F. Good',7,176140),
  ('Cody G. Hutchinson',4,50927),
  ('Edward X. Hopper',2,105871),
  ('Nadine F. Andrews',5,107209),
  ('Benjamin R. Bush',7,120210);

create table Skills (
	Id int not null identity(1,1) primary key,
	Skill_name varchar(250) not null unique);

INSERT Skills (Skill_name)
	VALUES
	('C#'),
	('JavaScript'),
	('SQL');

create table EmployeeSkills (
	Id_employee int not null foreign key references Employee(Id) on delete cascade,
	Id_skills int not null foreign key references Skills(Id) on delete cascade);

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
	Id int not null identity(1,1) primary key,
	Login varchar(50) not null unique,
	Password varchar(50) not null);

INSERT Users (Login,Password)
	VALUES
	('login', 'password');