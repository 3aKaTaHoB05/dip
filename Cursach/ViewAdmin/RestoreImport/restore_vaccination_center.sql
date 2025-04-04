CREATE DATABASE IF NOT EXISTS `vaccination_centerr`
  /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */
  /*!80016 DEFAULT ENCRYPTION='N' */;
USE `vaccination_centerr`;

-- Структура таблицы `Gender`
DROP TABLE IF EXISTS `Gender`;
CREATE TABLE `Gender` (
  `GenderID` INT NOT NULL AUTO_INCREMENT,
  `GenderName` VARCHAR(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`GenderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `MethodOfVaccineAdministration`
DROP TABLE IF EXISTS `MethodOfVaccineAdministration`;
CREATE TABLE `MethodOfVaccineAdministration` (
  `MethodOfVaccineAdministrationID` INT NOT NULL AUTO_INCREMENT,
  `MethodOfVaccineAdministrationName` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`MethodOfVaccineAdministrationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Packaging`
DROP TABLE IF EXISTS `Packaging`;
CREATE TABLE `Packaging` (
  `PackagingID` INT NOT NULL AUTO_INCREMENT,
  `PackagingName` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`PackagingID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `SocialStatus`
DROP TABLE IF EXISTS `SocialStatus`;
CREATE TABLE `SocialStatus` (
  `SocialStatusID` INT NOT NULL AUTO_INCREMENT,
  `SocialStatusName` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`SocialStatusID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Patient`
DROP TABLE IF EXISTS `Patient`;
CREATE TABLE `Patient` (
  `PatientID` INT NOT NULL AUTO_INCREMENT,
  `FIO` VARCHAR(100) NOT NULL,
  `Gender` INT NOT NULL,
  `PhoneNumber` VARCHAR(19) NOT NULL,
  `Age` INT NOT NULL,
  `SocialStatus` INT NOT NULL,
  PRIMARY KEY (`PatientID`),
  CONSTRAINT `fk_Gender` FOREIGN KEY (`Gender`) REFERENCES `Gender` (`GenderID`),
  CONSTRAINT `fk_SocialStatus` FOREIGN KEY (`SocialStatus`) REFERENCES `SocialStatus` (`SocialStatusID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Role`
DROP TABLE IF EXISTS `Role`;
CREATE TABLE `Role` (
  `RoleID` INT NOT NULL AUTO_INCREMENT,
  `RoleName` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Unit`
DROP TABLE IF EXISTS `Unit`;
CREATE TABLE `Unit` (
  `UnitID` INT NOT NULL AUTO_INCREMENT,
  `UnitName` VARCHAR(17) NOT NULL,
  PRIMARY KEY (`UnitID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `User`
DROP TABLE IF EXISTS `User`;
CREATE TABLE `User` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `FIO` VARCHAR(150) NOT NULL,
  `PhoneNumber` VARCHAR(19) NOT NULL,
  `Age` INT NOT NULL,
  `Role` INT NOT NULL,
  `Login` VARCHAR(25) NOT NULL,
  `Password` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`UserID`),
  CONSTRAINT `fk_Role` FOREIGN KEY (`Role`) REFERENCES `Role` (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Vaccine`
DROP TABLE IF EXISTS `Vaccine`;
CREATE TABLE `Vaccine` (
  `VaccineSeries` INT NOT NULL AUTO_INCREMENT,
  `VaccineName` VARCHAR(100) NOT NULL,
  `Volume` DOUBLE(10,3) NOT NULL,
  `Unit` INT NOT NULL,
  `Packaging` INT NOT NULL,
  `Image` MEDIUMBLOB,
  PRIMARY KEY (`VaccineSeries`),
  CONSTRAINT `fk_Unit` FOREIGN KEY (`Unit`) REFERENCES `Unit` (`UnitID`),
  CONSTRAINT `fk_Packaging` FOREIGN KEY (`Packaging`) REFERENCES `Packaging` (`PackagingID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Структура таблицы `Vaccination`
DROP TABLE IF EXISTS `Vaccination`;
CREATE TABLE `Vaccination` (
  `VaccinationSeries` INT NOT NULL AUTO_INCREMENT,
  `VaccineName` INT NOT NULL,
  `Patient` INT NOT NULL,
  `Executor` INT NOT NULL,
  `DateOfExecution` DATE NOT NULL,
  `MethodOfVaccineAdministration` INT NOT NULL,
  `Status` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`VaccinationSeries`),
  CONSTRAINT `fk_VaccineName` FOREIGN KEY (`VaccineName`) REFERENCES `Vaccine` (`VaccineSeries`),
  CONSTRAINT `fk_Patient` FOREIGN KEY (`Patient`) REFERENCES `Patient` (`PatientID`),
  CONSTRAINT `fk_Executor` FOREIGN KEY (`Executor`) REFERENCES `User` (`UserID`),
  CONSTRAINT `fk_MethodOfVaccineAdministration` FOREIGN KEY (`MethodOfVaccineAdministration`) REFERENCES `MethodOfVaccineAdministration` (`MethodOfVaccineAdministrationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;