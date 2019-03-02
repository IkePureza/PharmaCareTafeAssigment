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
VALUES ("Reginald", "Greengrass", 1, 2);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Antony", "Helloworld", 1, 2);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Whatever", "Beer", 1, 2);

INSERT INTO patients (firstName, lastName, wardID, roomID)
VALUES ("Chips", "Jar", 1, 2);

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
VALUES ("Matteo", "Baldini", "matteo", "baldini", 1, 11, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Larry", "Pharmacist", "larry", "pharmacist", 2, 11, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Alex", "Helloworld", "alex", "helloworld", 3, 11, 11);

INSERT INTO staff (firstName, lastName, username, password, jobTitle, wardID, roomID)
VALUES ("Jim", "beam", "jim", "beam", 4, 11, 11);

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
VALUES (1, 1, "20 November 2018", "Take 4 times a day, 5 if particularly sad.", 0, 0);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (2, 1, "20 November 2018", "Take together with water.", 0, 0);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (3, 1, "20 November 2018", "Preferrably take before ice cream", 0, 0);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (1, 1, "20 November 2018", "Take together with water.", 0, 0);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (2, 1, "20 November 2018", "Preferrably take before ice cream", 0, 0);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (3, 1, "20/11/2018", "Preferrably take before ice cream", 1, 1);

INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor)
VALUES (3, 1, "20/11/2018", "Lorem ipsum dolor sit amet, exerci numquam eos an, solet dolore graecis eam an. 
His eu vidit diceret feugait, vel ex quando percipit, ei his assum iriure. Velit periculis moderatius cu has, 
sea alia utamur mediocritatem in, ut sed dictas nusquam repudiare. Soluta nostrum ex vix, graece urbanitas pro ex, 
his an esse fabulas. Id sit recteque tractatos scribentur, malorum adolescens est ad.
", 1, 1);

DELETE FROM prescriptions WHERE prescriptionID='14';




CREATE TABLE drugs
(
    drugID INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    name VARCHAR(100) NOT NULL,
    dangerous BOOLEAN NOT NULL
);

INSERT INTO drugs (name, dangerous)
VALUES ("Panadol", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Space cake", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Stardust", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Cannabinoid", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Opioids", false);

INSERT INTO drugs (name, dangerous)
VALUES ("Xanax", true);
VALUES ("Xanax", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Valium", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Fentanyl", true);

INSERT INTO drugs (name, dangerous)
VALUES ("Antibiotics", false);

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

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (3, "Powder", "20mg", "20 January 2018", "20 November 2018", 2);

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (4, "Liquid", "200mg", "20 January 2018", "20 November 2018", 2);

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (5, "Paste", "200mg", "20 January 2018", "20 November 2018", 2);

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (6, "Pills", "200mg", "20 January 2018", "20 November 2018", 2);

INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay)
VALUES  (7, "Aerosol", "500mg", "20 January 2018", "20 November 2018", 2);

CREATE TABLE prescriptionsToDetails
(
    prescriptionID INT(11) NOT NULL,
    detailID INT(11) NOT NULL,
    FOREIGN KEY (prescriptionID) REFERENCES prescriptions(prescriptionID),
    FOREIGN KEY (detailID) REFERENCES details(detailID)
);

INSERT INTO prescriptionsToDetails (prescriptionID, detailID)
VALUES (1, 1);

INSERT INTO prescriptionsToDetails (prescriptionID, detailID)
VALUES (2, 3);

INSERT INTO prescriptionsToDetails (prescriptionID, detailID)
VALUES (1, 2);

INSERT INTO prescriptionsToDetails (prescriptionID, detailID)
VALUES (3, 4);

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

//ADDITIONAL STUFF DO NOT COPY// or do ... whatever floats your boat

SELECT details.*
FROM prescriptionsToDetails
JOIN details ON prescriptionsToDetails.detailID = details.detailID
WHERE prescriptionID=1;

SELECT details.*, drugs.name
FROM prescriptionsToDetails
JOIN details ON prescriptionsToDetails.detailID = details.detailID
JOIN drugs ON details.drugID = drugs.drugID
WHERE prescriptionID=1;
