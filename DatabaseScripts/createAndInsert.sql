create database if not exists AnReshProbation;

use AnReshProbation;
CREATE USER if not exists 'anreshuser'@'%' IDENTIFIED BY 'anreshuser';
GRANT ALL privileges ON anreshprobation.* TO 'anreshuser'@'%';

create table if not exists Department (
	Id int(3) not null auto_increment,
	Name char(100) not null,
	primary key (id));

insert into 
	Department ( name)
values
	('Администрация'),
    ('Отдел кадров'),
    ('Отдел маркетинга'),
    ('Отдел продаж'),
    ('Отдел финансов'),
    ('Отдел логистики'),
    ('Отдел ИТ'),
    ('Отдел закупок');
	

create table if not exists Employee (
	Id int(8) not null auto_increment,
	Full_name char(250) not null,
	Id_department int(3),
	Salary int(9) default 12392,
	primary key (id), foreign key (id_department) references department (id) on delete set null);
    
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Quintessa P. Chan",2,19906),("Brittany U. Swanson",7,50592),("Sawyer D. Luna",6,115416),("Steel T. Orr",5,75343),("Beatrice X. Madden",4,18659),("Sophia G. Watson",1,83834),("Hillary F. Potter",1,41568),("Dante W. Lynch",5,62143),("Magee X. Rowland",1,96808),("Keith B. Mann",1,102332);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Erin I. Gallagher",7,35216),("Glenna W. Mitchell",1,106502),("Brianna H. Frye",7,22916),("Maxwell I. Small",3,99455),("Devin L. Castaneda",4,65010),("Raphael Q. Mathis",3,145532),("Wynne I. Henry",4,60220),("Kieran Y. Holland",1,30971),("Alexander F. Franklin",5,17896),("Carson A. Osborne",2,43488);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Urielle H. Stewart",2,45129),("Haviva J. Mercado",6,62266),("Macey R. Sears",2,141548),("Olivia N. Graves",1,110600),("Upton B. Watkins",7,125670),("Hermione V. Bartlett",5,97676),("Eve F. Shepherd",2,100550),("Zelenia X. Delaney",6,127239),("Brooke Z. Stephens",7,109115),("Daquan U. Watts",4,50135);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Deacon I. Stewart",1,38671),("Brady R. Pierce",1,29134),("Alden P. Carlson",5,96757),("Basil I. Kline",7,117748),("Ethan R. Malone",2,129441),("Daquan K. Sheppard",5,18344),("Hilary S. Dodson",3,105240),("Merrill O. Carver",4,72059),("Nola N. Mccray",8,37720),("Holly Y. Donovan",1,89461);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Brady M. Lawson",5,75304),("Marcia L. Gibbs",8,47557),("Tobias Y. Kramer",1,28552),("Lana M. Murray",7,96164),("Reese K. Hurley",5,89040),("Shellie Y. Mcneil",3,67119),("Asher I. Torres",2,34132),("Hector Y. Daugherty",3,76809),("Mollie T. Woodward",2,38429),("Harlan H. Dickerson",8,124694);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Echo P. Robinson",6,14979),("Marshall W. Mckay",3,116497),("Tate A. Rodgers",3,15612),("Felix Y. Hart",2,143458),("Brynne I. Kennedy",7,133490),("Galena N. Blanchard",6,35729),("Galvin X. Merrill",8,122038),("Rama S. Phillips",8,98487),("Oliver I. Knight",4,81340),("Eliana T. Dillon",3,85430);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Bruce P. Lyons",8,50003),("Mason F. Hernandez",7,116873),("Steven H. Mcdowell",1,68362),("Risa D. Ingram",7,12674),("Stuart U. Barlow",8,100998),("Haley V. Christensen",4,14863),("Benjamin G. Frazier",4,78457),("Camden L. Gentry",8,84867),("Ora K. Soto",5,28423),("Charles L. Dejesus",1,134207);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Barbara K. Peters",5,81714),("Michelle B. Eaton",7,60455),("Myra P. Barton",8,71188),("Callie A. Hickman",3,18275),("Stephanie X. Everett",3,66030),("Tallulah L. Gutierrez",8,123692),("Preston A. Glenn",3,50228),("Trevor V. Britt",8,84175),("Jenna Y. Cooper",5,63082),("Elizabeth W. Summers",3,95094);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Elizabeth T. Nichols",1,26704),("Kaye L. Mcbride",3,64915),("Brock O. Mosley",8,72545),("Uriah E. Butler",1,146017),("Ezra Q. Pope",1,29769),("Nicholas S. Perkins",4,16867),("Kennedy F. Boyer",8,100241),("Zeus U. Mcintyre",6,57313),("Roanna W. Soto",6,60315),("Cody N. Sherman",1,78980);
INSERT INTO `employee` (`full_name`,`id_department`,`salary`) VALUES ("Buckminster O. Weaver",2,140027),("Victoria N. Mcclure",4,26065),("Vance O. Knight",2,97994),("Meghan D. Mcfadden",7,92260),("Medge R. Cleveland",1,104927),("Callie J. Walton",2,128049),("Liberty C. Smith",3,56496),("Allen A. Knapp",6,137452),("Savannah C. Byrd",4,132430),("Arthur W. Tillman",2,24158);
