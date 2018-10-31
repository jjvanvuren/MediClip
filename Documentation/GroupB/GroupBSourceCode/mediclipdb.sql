--This file is used to create the MediClip SQL database and fill it with data to be used for testing.

--Create the Nurse Table
CREATE TABLE Nurse (
	NurseID						INT				PRIMARY KEY CHECK (NurseID > 0 AND NurseID <= 9999),
	UserName					VARCHAR(30)		NOT NULL,
	FirstName					VARCHAR(15),
	LastName					VARCHAR(15),
	Password					VARCHAR(30)
)

--Create the Ward table
CREATE TABLE Ward (
	WardID						INT				PRIMARY KEY CHECK (WardID > 0 AND WardID <= 9999),
	Name						VARCHAR(15)		NOT NULL,
	Description					VARCHAR(150)	NOT NULL
)

--Create the AssignedWard table
CREATE TABLE AssignedWard (
	NurseID						INT				NOT NULL CHECK (NurseID > 0 AND NurseID <= 9999),
	WardID						INT				NOT NULL CHECK (WardID > 0 AND WardID <= 9999),
	AssignDateFrom				DATE			NOT NULL,
	AssignDateTo				DATE			NOT NULL,
	PRIMARY KEY (NurseID, WardID),
	FOREIGN KEY(NurseID)		REFERENCES Nurse (NurseID)	ON UPDATE CASCADE ON DELETE NO ACTION,
	FOREIGN KEY(WardID)			REFERENCES Ward (WardID)	ON UPDATE CASCADE ON DELETE NO ACTION,
)

--Create the Patient table
CREATE TABLE Patient (
	PatientID					INT				PRIMARY KEY CHECK (PatientID > 0 AND PatientID <= 9999),
	WardID						INT				NOT NULL,
	AssignDateFrom				VARCHAR(150)	NOT NULL,
	AssignDateTo				VARCHAR(150)	NOT NULL,
	FirstName					VARCHAR(15)		NOT NULL,
	LastName					VARCHAR(15)		NOT NULL,
	Dob							VARCHAR(150)	NOT NULL,
	Sex							VARCHAR(7)		NOT NULL,
	Dosage						VARCHAR(150),
	Picture						VARCHAR(100),
	FOREIGN KEY(WardID)		REFERENCES	Ward (WardID) ON UPDATE CASCADE ON DELETE CASCADE
)

--Create the Notes Table
CREATE TABLE Note(
	NoteID						INT 			IDENTITY(1,1) CHECK (NoteID > 0 AND NoteID <= 9999),
	PatientID					INT				NOT NULL,
	Title						VARCHAR(100)	NOT NULL,
	Text						VARCHAR(500)	NOT NULL,
	Picture						VARCHAR(1000),
	PRIMARY KEY (NoteID, PatientID),
	FOREIGN KEY(PatientID)		REFERENCES Patient (PatientID)	ON UPDATE CASCADE ON DELETE CASCADE,
)

SELECT * FROM Patient
SELECT * FROM Ward
SELECT * FROM AssignedWard
SELECT * FROM Nurse
SELECT * FROM Note

DROP TABLE Note;
DROP TABLE Patient;
DROP TABLE Ward;
DROP TABLE AssignedWard;
DROP TABLE Nurse;

--Insert data into Ward
INSERT INTO Ward (WardID, Name, Description)
VALUES
(1, 'Pediatrics', 'Pediatric stuff'),
(2, 'Maternity', 'Maternity stuff'),
(3, 'Geriatrics', 'Geriatric stuff'),
(4, 'Psychiatrics', 'Psychiatric stuff');

--Insert data into Nurse
INSERT INTO Nurse (NurseID, UserName, FirstName, LastName, Password)
VALUES
(1, 'joyrainbow','Joy', 'Rainbow', '1234'),
(2, 'sallysummers','Sally', 'Summers', '1234'),
(3, 'josephmoseph','Joseph', 'Moseph', '1234');

--Insert data into Patient
INSERT INTO Patient (PatientID, WardID, AssignDateFrom, AssignDateTo, FirstName, LastName, Dob, Sex, Dosage, Picture)
VALUES
(1, 4, '2018-10-21', '2018-12-21', 'Bob', 'Ross', '1974-10-21', 'Male', 'Mesalazine 4mg per day' + CHAR(13) + CHAR(10) + 'ibuprofen 200mg twice a day', 'bobross.jpeg'),
(2, 3, '2018-09-21', '2018-11-1', 'Joe', 'Grow', '1990-08-04', 'Male', 'N/A', ''),
(3, 2, '2018-01-11', '2018-08-11', 'Molly', 'Polly', '1980-06-04', 'Female', 'N/A', ''),
(4, 1, '2018-03-12', '2018-05-12', 'Jane', 'Gain', '1993-07-20', 'Female', 'N/A', ''),
(5, 4, '2018-03-12', '2018-05-12', 'Jenny', 'Craig', '1993-07-20', 'Female', 'N/A', ''),
(6, 3, '2018-03-12', '2018-05-12', 'Billy', 'Jean', '1993-07-20', 'Male', 'N/A', ''),
(7, 2, '2018-03-12', '2018-05-12', 'Amanda', 'Sander', '1993-07-20', 'Female', 'N/A', ''),
(8, 1, '2018-03-12', '2018-05-12', 'Joel', 'Bowl', '1993-07-20', 'Male', 'N/A', '');

--Insert data into Note
INSERT INTO Note (PatientID, Title, Text)
VALUES
(1, 'Painkillers', 'Patient requests more painkillers. Has alot of pain in his leg.'),
(1, 'Leg Pain', 'Patient says his leg feels much better since he got some painkillers.');

