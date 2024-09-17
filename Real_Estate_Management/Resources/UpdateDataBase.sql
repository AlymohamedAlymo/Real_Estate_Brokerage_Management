USE [realestatebrokermanagement]
GO

/****** Object:  Table [dbo].[tbOwner]    Script Date: 9/15/2024 8:57:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbOwner](
	[PlanGuid] [uniqueidentifier] NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Statues] [varchar](10) NULL,
	[Number] [int] NULL,
	[Name] [varchar](255) NULL,
	[IDNumber] [varchar](255) NULL,
	[Mobile] [varchar](255) NULL,
	[MobileAdd] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[VatNumber] [varchar](255) NULL,
	[PublicNumber] [varchar](255) NULL,
	[AgentName] [varchar](255) NULL,
	[AgentID] [varchar](255) NULL,
	[AgentMobile] [varchar](255) NULL,
	[AgenteMail] [varchar](255) NULL,
	[AgentVatNumber] [varchar](255) NULL,
	[AgencyNumber] [varchar](255) NULL,
	[AgentPublicNumber] [varchar](255) NULL,
	[OfficeName] [varchar](255) NULL,
	[OfficeCR] [varchar](255) NULL,
	[OfficePhone] [varchar](255) NULL,
	[OfficeeMail] [varchar](255) NULL,
	[OfficeVatNumber] [varchar](255) NULL,
	[OfficePublicNumber] [varchar](255) NULL,
	[Note] [varchar](255) NULL,
	[LastAction] [varchar](255) NULL,
 CONSTRAINT [PK_tbOwner] PRIMARY KEY CLUSTERED 
(
	[PlanGuid] ASC,
	[Guid] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[tbLawyer]    Script Date: 9/11/2024 6:26:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbLawyer](
	[PlanGuid] [uniqueidentifier] NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Statues] [varchar](10) NULL,
	[Number] [int] NULL,
	[Name] [varchar](25) NULL,
	[IDNumber] [varchar](50) NULL,
	[Mobile] [varchar](25) NULL,
	[Email] [varchar](25) NULL,
	[VatNumber] [varchar](50) NULL,
	[PublicNumber] [varchar](25) NULL,
	[Note] [varchar](255) NULL,
	[LastAction] [varchar](200) NULL,
 CONSTRAINT [PK_tbLawyer] PRIMARY KEY CLUSTERED 
(
	[PlanGuid] ASC,
	[Guid] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

UPDATE [realestatebrokermanagement].[dbo].[tbLawyer] set 
[PlanGuid] = (SELECT TOP(1) [guid] FROM realestatebrokermanagement.[dbo].[tbPlanInfo]),
[Statues] = '‰‘ÿ'
GO

/****** Object:  Table [dbo].[tbPlans]    Script Date: 9/11/2024 6:28:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbPlans](
	[OwnerGuid] [uniqueidentifier] NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](25) NULL,
	[Name] [varchar](25) NULL,
	[City] [varchar](25) NULL,
	[Location] [varchar](200) NULL,
	[Note] [varchar](255) NULL,
	[LastAction] [varchar](200) NULL,
 CONSTRAINT [PK_tbPlans] PRIMARY KEY CLUSTERED 
(
	[OwnerGuid] ASC,
	[Guid] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

DELETE FROM realestatebrokermanagement.[dbo].[tbPlans]
INSERT INTO realestatebrokermanagement.[dbo].[tbPlans]
SELECT ownerguid as [OwnerGuid], [guid] as [Guid], number as [Number],[name] as [Name],
		   city as [City], [location] as [Location], '' as [Note],'' as [LastAction] 
		   FROM data2024Restore.[dbo].tbPlanInfo
GO


/****** Object:  Table [dbo].[tbLog]    Script Date: 9/11/2024 6:27:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbLog](
	[PlanGuid] [uniqueidentifier] NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [int] IDENTITY(1,1) NOT NULL,
	[RegDate] [date] NULL,
	[UserName] [varchar](100) NULL,
	[ActionType] [varchar](10) NULL,
	[Action] [varchar](25) NULL,
	[Note] [varchar](255) NULL,
 CONSTRAINT [PK_tbLog] PRIMARY KEY CLUSTERED 
(
	[PlanGuid] ASC,
	[Guid] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



DELETE FROM realestatebrokermanagement.[dbo].tbAccount
INSERT INTO realestatebrokermanagement.[dbo].tbAccount
SELECT * FROM data2024Restore.[dbo].tbAccount
Go

DELETE FROM realestatebrokermanagement.[dbo].tbLog
INSERT INTO realestatebrokermanagement.[dbo].tbLog
SELECT * FROM data2024Restore.[dbo].tbLog
Go

DELETE FROM realestatebrokermanagement.[dbo].tbAgent
INSERT INTO realestatebrokermanagement.[dbo].tbAgent
SELECT  *, ' ' FROM data2024Restore.[dbo].tbAgent
Go

DELETE FROM realestatebrokermanagement.[dbo].tbAppInfo
INSERT INTO realestatebrokermanagement.[dbo].tbAppInfo
SELECT * FROM data2024Restore.[dbo].tbAppInfo
Go

DELETE FROM realestatebrokermanagement.[dbo].tbAttachment
INSERT INTO realestatebrokermanagement.[dbo].tbAttachment
SELECT * FROM data2024Restore.[dbo].tbAttachment
Go

DELETE FROM realestatebrokermanagement.[dbo].tbBanks
INSERT INTO realestatebrokermanagement.[dbo].tbBanks
SELECT * FROM data2024Restore.[dbo].tbBanks
Go

DELETE FROM realestatebrokermanagement.[dbo].tbBillBody
INSERT INTO realestatebrokermanagement.[dbo].tbBillBody
SELECT * FROM data2024Restore.[dbo].tbBillBody
Go

DELETE FROM realestatebrokermanagement.[dbo].tbBillheader
INSERT INTO realestatebrokermanagement.[dbo].tbBillheader
SELECT *, ' ' FROM data2024Restore.[dbo].tbBillheader
Go

DELETE FROM realestatebrokermanagement.[dbo].tbEditPassword
INSERT INTO realestatebrokermanagement.[dbo].tbEditPassword
SELECT * FROM data2024Restore.[dbo].tbEditPassword
Go

DELETE FROM realestatebrokermanagement.[dbo].tbEmailSettings
INSERT INTO realestatebrokermanagement.[dbo].tbEmailSettings
SELECT * FROM data2024Restore.[dbo].tbEmailSettings
Go

DELETE FROM realestatebrokermanagement.[dbo].tbPlanInfo
INSERT INTO realestatebrokermanagement.[dbo].tbPlanInfo
SELECT * FROM data2024Restore.[dbo].tbPlanInfo
Go

DELETE FROM realestatebrokermanagement.[dbo].tbLand
INSERT INTO realestatebrokermanagement.[dbo].tbLand
SELECT (SELECT TOP(1) [guid] From data2024Restore.[dbo].tbPlanInfo),  *, ' ' FROM data2024Restore.[dbo].tbLand
Go

DELETE FROM realestatebrokermanagement.[dbo].tbLandTrans
INSERT INTO realestatebrokermanagement.[dbo].tbLandTrans
SELECT * FROM data2024Restore.[dbo].tbLandTrans
Go

DELETE FROM realestatebrokermanagement.[dbo].tbPay
INSERT INTO realestatebrokermanagement.[dbo].tbPay
SELECT * FROM data2024Restore.[dbo].tbPay
Go

DELETE FROM realestatebrokermanagement.[dbo].tbPayContract
INSERT INTO realestatebrokermanagement.[dbo].tbPayContract
SELECT * FROM data2024Restore.[dbo].tbPayContract
Go

DELETE FROM realestatebrokermanagement.[dbo].tbPaymentsNotes
INSERT INTO realestatebrokermanagement.[dbo].tbPaymentsNotes
SELECT * FROM data2024Restore.[dbo].tbPaymentsNotes
Go

DELETE FROM realestatebrokermanagement.[dbo].tbPicture
INSERT INTO realestatebrokermanagement.[dbo].tbPicture
SELECT * FROM data2024Restore.[dbo].tbPicture
Go


DELETE FROM realestatebrokermanagement.[dbo].tbPriceLog
INSERT INTO realestatebrokermanagement.[dbo].tbPriceLog
SELECT * FROM data2024Restore.[dbo].tbPriceLog
Go

DELETE FROM realestatebrokermanagement.[dbo].tbReportCustomView
INSERT INTO realestatebrokermanagement.[dbo].tbReportCustomView
SELECT * FROM data2024Restore.[dbo].tbReportCustomView
Go

DELETE FROM realestatebrokermanagement.[dbo].tbSaleOrder
INSERT INTO realestatebrokermanagement.[dbo].tbSaleOrder
SELECT * FROM data2024Restore.[dbo].tbSaleOrder
Go

DELETE FROM realestatebrokermanagement.[dbo].tbSaleOrderBody
INSERT INTO realestatebrokermanagement.[dbo].tbSaleOrderBody
SELECT * FROM data2024Restore.[dbo].tbSaleOrderBody
Go

DELETE FROM realestatebrokermanagement.[dbo].tbSelectObject
INSERT INTO realestatebrokermanagement.[dbo].tbSelectObject
SELECT * FROM data2024Restore.[dbo].tbSelectObject
Go

DELETE FROM realestatebrokermanagement.[dbo].tbSMSsettings
INSERT INTO realestatebrokermanagement.[dbo].tbSMSsettings
SELECT * FROM data2024Restore.[dbo].tbSMSsettings
Go

DELETE FROM realestatebrokermanagement.[dbo].tbTaxDiscount
INSERT INTO realestatebrokermanagement.[dbo].tbTaxDiscount
SELECT * FROM data2024Restore.[dbo].tbTaxDiscount
Go

DELETE FROM realestatebrokermanagement.[dbo].tbUsers
INSERT INTO realestatebrokermanagement.[dbo].tbUsers
SELECT * FROM data2024Restore.[dbo].tbUsers
Go

DELETE FROM realestatebrokermanagement.[dbo].tbUsersPermissions
INSERT INTO realestatebrokermanagement.[dbo].tbUsersPermissions
SELECT * FROM data2024Restore.[dbo].tbUsersPermissions
Go

DELETE FROM realestatebrokermanagement.[dbo].tbVatSettings
INSERT INTO realestatebrokermanagement.[dbo].tbVatSettings
SELECT * FROM data2024Restore.[dbo].tbVatSettings
Go
