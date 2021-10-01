use master;
drop database if exists AnReshProbation;
create database  AnReshProbation;

use [AnReshProbation];
drop user if exists anreshuser;
create user anreshuser without login;
GRANT BACKUP DATABASE, BACKUP LOG, CREATE DEFAULT, CREATE FUNCTION, CREATE PROCEDURE, CREATE RULE, CREATE TABLE, CREATE VIEW TO anreshuser;
drop table if exists Employee;
drop table if exists Department;
create table Department (
	Id int not null identity(1,1) primary key,
	Name char(100) not null);

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
	Full_name char(250) not null,
	Id_department int not null foreign key references Department(Id) on delete cascade,
	Salary int not null default 12392);
    
INSERT INTO [Employee] (Full_name,Id_department,Salary)
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
INSERT INTO [Employee] (Full_name,Id_department,Salary)
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
INSERT INTO [Employee] (Full_name,Id_department,Salary)
VALUES
  ('Tara Q. Burris',5,54632),
  ('Ruby Q. Mccray',4,17813),
  ('Fiona W. Britt',6,108583),
  ('Fiona L. Chase',5,28536),
  ('Bradley O. Walters',6,116245),
  ('Gemma Y. Pruitt',8,177386),
  ('Galvin N. Nicholson',6,46033),
  ('Brent O. Allison',2,160291),
  ('George T. Kaufman',6,38282),
  ('Talon K. Calhoun',2,100570);
INSERT INTO [Employee] (Full_name,Id_department,Salary)
VALUES
  ('Arden F. Palmer',3,111011),
  ('Murphy W. Campbell',3,81176),
  ('Bree S. Warner',2,165522),
  ('Kirby U. Reyes',4,77981),
  ('Slade U. Daniels',3,197863),
  ('Benjamin N. Mccormick',3,67471),
  ('Kay K. Mack',6,72349),
  ('Eliana Y. Terry',3,156534),
  ('Patrick S. York',4,117721),
  ('Thaddeus R. Farrell',6,176273),
  ('Evan O. Byrd',4,64456),
  ('Kyla N. Wong',4,84813),
  ('Hollee C. Whitehead',2,170571),
  ('Nolan B. Larson',5,135906),
  ('Solomon Q. Campos',4,81642),
  ('Leslie B. Pruitt',5,61737),
  ('Dominique H. Dotson',7,51142),
  ('Trevor D. Pennington',5,114536),
  ('Logan A. Stone',5,16226),
  ('Reese X. Figueroa',3,137792);
INSERT INTO [Employee] (Full_name,Id_department,Salary)
VALUES
  ('Germane M. Le',6,105040),
  ('Jamal W. Dillon',8,28010),
  ('Xena N. Mcfarland',1,72590),
  ('Declan F. Mitchell',5,41866),
  ('Sarah P. Reed',3,25198),
  ('Mary D. Summers',1,98386),
  ('Kenneth I. Lucas',7,29343),
  ('Noel Y. Castaneda',5,132147),
  ('Aristotle C. Alvarado',6,73101),
  ('Malcolm T. Valenzuela',6,35677),
  ('Jamalia Z. Gallagher',4,29896),
  ('Wade Q. Lowery',5,121616),
  ('Inez J. Welch',7,99865),
  ('Germaine R. Bauer',5,25640),
  ('Neil B. Terry',5,156670),
  ('Aidan P. Velazquez',2,68713),
  ('Samuel I. Moore',4,179999),
  ('Victor T. Rogers',5,186844),
  ('Dominic L. Nichols',8,50282),
  ('Christian B. Farley',4,187204);
INSERT INTO [Employee] (Full_name,Id_department,Salary)
VALUES
  ('Suki J. Hudson',7,166753),
  ('Scott T. Hull',2,178773),
  ('Lila G. Weeks',3,94700),
  ('Kylie K. Calhoun',1,172700),
  ('Kellie V. Pollard',8,100089),
  ('Chaney B. Morton',6,15207),
  ('Erica M. Brewer',7,156712),
  ('Maile L. Gray',8,52040),
  ('Matthew E. Buckner',4,57841),
  ('Neil U. Summers',7,61824);
