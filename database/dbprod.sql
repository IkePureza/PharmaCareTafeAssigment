wards:
3267
9041
5671

CREATE DATABASE pharmacare;

USE pharmacare;

CREATE TABLE patients
(
	patientID INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(30) NOT NULL,
	wardID INT(11),
	roomID INT(11)
);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("John", "Doe", 3267, 2);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Janette", "Doe", 9041, 1);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Reginald", "Greengrass", 9041, 3);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Reginald", "Greengrass", 3267, 4);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Reginald", "Greengrass", 5671, 1);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Reginald", "Greengrass", 5671, 2);

CREATE TABLE staff
(
    staffID INT(11) AUTO_INCREMENT NOT NULL PRIMARY KEY,
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30) NOT NULL,
    username VARCHAR(30) NOT NULL,
    password VARCHAR(30) NOT NULL,
    jobTitle INT(11) NOT NULL,
    wardID INT(11) NOT NULL,
    roomID INT(11) NOT NULL
);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Matteo", "Baldini", "matteo", "baldini", 1, 3267, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Henrique", "pureza", "henrique", "pureza", 2, 3267, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Alex", "Vassiliou", "alex", "vassiliou", 4, 13267, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Christovao", "Galambos", "christovao", "galambos", 5, 11, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Connor", "Aitken", "connor", "aitken", 3, 3267, 11);

CREATE TABLE prescriptions
(
    prescriptionID INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    patientID INT(11) NOT NULL,
    staffID INT(11) NOT NULL,
    prescriptionDate VARCHAR(30) NOT NULL,
    prescriptionDetails VARCHAR(8000) NOT NULL,
    status INT NOT NULL,
    indoor INT NOT NULL,
    FOREIGN KEY (patientID) REFERENCES patients(patientID),
    FOREIGN KEY (staffID) REFERENCES staff(staffID)
);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (1, 1, "20 November 2018", "Take 4 times a day, 5 if particularly sad.", 1, 0);

CREATE TABLE drugs
(
    drugID INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    name VARCHAR(100) NOT NULL,
    dangerous BOOLEAN NOT NULL
);

INSERT INTO drugs (name, dangerous)
VALUES ("Panadol", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Xanax", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Valium", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Codeine", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Adderal", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Paracetamol", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Thiamine", false);

CREATE TABLE details
(
    detailID INT(11) AUTO_INCREMENT NOT NULL PRIMARY KEY,
    drugID INT(11) NOT NULL,
    drugForm VARCHAR(100) NOT NULL,
    drugDose VARCHAR(100) NOT NULL,
    firstTime VARCHAR(100) NOT NULL,
    lastTime VARCHAR(100) NOT NULL,
    timesPerDay INT NOT NULL,
    FOREIGN KEY (drugID) REFERENCES drugs(drugID)
);

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (2, "Pills", "200mg", "20 January 2018", "20 November 2018", 2);

CREATE TABLE prescriptionsToDetails
(
    prescriptionID INT(11) NOT NULL,
    detailID INT(11) NOT NULL,
    FOREIGN KEY (prescriptionID) REFERENCES prescriptions(prescriptionID),
    FOREIGN KEY (detailID) REFERENCES details(detailID)
);

INSERT INTO prescriptionsToDetails (prescriptionID, detailID)
VALUES (1, 1);

CREATE TABLE indoorPrescription
(
    prescriptionID INT(11) NOT NULL ,
    roomNumber INT(11) NOT NULL,
    wingNumber INT(11) NOT NULL,
    floorNumber INT(11) NOT NULL,
    nursingStationID INT(11) NOT NULL,
    FOREIGN KEY (prescriptionID) REFERENCES prescriptions(prescriptionID)
);

CREATE TABLE opdPrescription
(
    prescriptionID INT(11) NOT NULL,
    toFill INT(11) NOT NULL,
    filledAndDispatched INT(11) NOT NULL,
    dateDispatched VARCHAR(30) NOT NULL,
    timeDispatched VARCHAR(30) NOT NULL,
    indoorEmergency INT(11) NOT NULL,
    FOREIGN KEY (prescriptionID) REFERENCES prescriptions(prescriptionID)
);
