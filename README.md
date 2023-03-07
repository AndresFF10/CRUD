CREATE DATABASE IN MYSQL:

    CREATE TABLE `dbasecrud`.`usuarios` (
      `id` INT NOT NULL AUTO_INCREMENT,
      `Name` VARCHAR(45) NOT NULL,
      `LastName` VARCHAR(45) NOT NULL,
      `Email` VARCHAR(45) NOT NULL,
      `Address` VARCHAR(45) NOT NULL,
      `Phone` INT NULL,
      `Working_Start_Date` DATE NOT NULL,
      `Picture` VARBINARY(32672) NOT NULL,
      `Rol` VARCHAR(45) NOT NULL,
      `Salary` FLOAT NOT NULL,
      `LastRevision` DATE NULL,
      `IncreasesSalary` VARCHAR(120) NULL,
      `IncreasesDates` VARCHAR(120) NULL,
      PRIMARY KEY (`id`));
  
  
  
Important: 
 1. There are conditions within the code that restrict some functionality. Check the console for information.
 2. When exporting the information, the csv file is saved in the location of the project.
