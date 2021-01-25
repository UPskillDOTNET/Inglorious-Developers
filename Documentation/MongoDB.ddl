CREATE TABLE ParkingLot (
  ID          int(10) NOT NULL, 
  name        varchar(255) NOT NULL, 
  companyName varchar(255) NOT NULL, 
  address     varchar(255) NOT NULL, 
  capacity    int(10) NOT NULL, 
  PRIMARY KEY (ID));
CREATE TABLE ParkingSpot (
  ID           varchar(10) NOT NULL, 
  ParkingLotID int(10) NOT NULL, 
  PriceHour    decimal(19, 0) NOT NULL, 
  Floor        int(10), 
  PRIMARY KEY (ID));
CREATE TABLE Reservation (
  ID            varchar(10) NOT NULL, 
  ParkingSpotID varchar(10) NOT NULL, 
  StartDate     date NOT NULL, 
  Hours         int(10) NOT NULL, 
  EndDate       date NOT NULL, 
  TotalPrice    decimal(19, 0) NOT NULL, 
  PRIMARY KEY (ID));
ALTER TABLE ParkingSpot ADD CONSTRAINT FKParkingSpo163974 FOREIGN KEY (ParkingLotID) REFERENCES ParkingLot (ID);
ALTER TABLE Reservation ADD CONSTRAINT FKReservatio589727 FOREIGN KEY (ParkingSpotID) REFERENCES ParkingSpot (ID);

