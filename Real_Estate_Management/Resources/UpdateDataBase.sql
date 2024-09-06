
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbLawyer](
	[guid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[name] [varchar](255) NULL,
	[civilid] [varchar](255) NULL,
	[mobile] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[vatid] [varchar](255) NULL,
	[publicnumber] [varchar](255) NULL,
	[note] [varchar](255) NULL,
	[lastaction] [varchar](255) NULL
) ON [PRIMARY]
GO

GO
DELETE FROM realestatebrokermanagement.[dbo].tbAccount
INSERT INTO realestatebrokermanagement.[dbo].tbAccount
SELECT * FROM data2024Restore.[dbo].tbAccount
Go

DELETE FROM realestatebrokermanagement.[dbo].tbLog
INSERT INTO realestatebrokermanagement.[dbo].tbLog
SELECT * FROM data2024.[dbo].tbLog
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
