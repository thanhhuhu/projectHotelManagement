create database khachsan1
go
use  khachsan1
go

CREATE TABLE [dbo].[rooms] 
(
    [roomid]   INT           IDENTITY (1, 1) NOT NULL,
    [roomNo]   VARCHAR (250) NOT NULL,
    [roomType] VARCHAR (250) NOT NULL,
    [bed]      VARCHAR (250) NOT NULL,
    [price]    BIGINT        NOT NULL,
    [booked]   VARCHAR (50)  DEFAULT ('NO') NULL,
    PRIMARY KEY CLUSTERED ([roomid] ASC)
);


CREATE TABLE [dbo].[customer] 
(
    [cid]         INT           IDENTITY (1, 1) NOT NULL,
    [cname]       VARCHAR (250) NOT NULL,
    [mobile]      BIGINT        NOT NULL,
    [passport]    VARCHAR (250) NOT NULL,
    [gender]      VARCHAR (50)  NOT NULL,
    [idproof]     VARCHAR (250) NOT NULL,
    [checkin]     VARCHAR (250) NOT NULL,
    [checkoutday] VARCHAR (250) NOT NULL,
    [checkout]    VARCHAR (250) NULL,
    [chekout]     VARCHAR (250) DEFAULT ('NO') NULL,
    [roomid]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([cid] ASC),
    FOREIGN KEY ([roomid]) REFERENCES [dbo].[rooms] ([roomid])
);
CREATE TABLE Receipts 
(
    ReceiptID INT PRIMARY KEY IDENTITY(1,1),
    RoomID INT FOREIGN KEY REFERENCES rooms(roomid),
    CustomerName VARCHAR(250) NOT NULL,
    BookingDate DATE NOT NULL,
    ExpectedCheckOutDate DATE NOT NULL,
    ActualCheckOutDate DATE,
    RoomPrice DECIMAL(18, 2) NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    LateCheckoutPenalty DECIMAL(18, 2) NOT NULL DEFAULT 0
);