
DELETE FROM realestatebrokeragemanagement.[dbo].tbAccount
INSERT INTO realestatebrokeragemanagement.[dbo].tbAccount
SELECT * FROM [almalqa2023abha].[dbo].tbAccount
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbLog
INSERT INTO realestatebrokeragemanagement.[dbo].tbLog
SELECT * FROM [almalqa2023abha].[dbo].tbLog
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbAgent
INSERT INTO realestatebrokeragemanagement.[dbo].tbAgent
SELECT  *, ' ' FROM [almalqa2023abha].[dbo].tbAgent
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbAppInfo
INSERT INTO realestatebrokeragemanagement.[dbo].tbAppInfo
SELECT * FROM [almalqa2023abha].[dbo].tbAppInfo
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbAttachment
INSERT INTO realestatebrokeragemanagement.[dbo].tbAttachment
SELECT * FROM [almalqa2023abha].[dbo].tbAttachment
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbBanks
INSERT INTO realestatebrokeragemanagement.[dbo].tbBanks
SELECT * FROM [almalqa2023abha].[dbo].tbBanks
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbBillBody
INSERT INTO realestatebrokeragemanagement.[dbo].tbBillBody
SELECT * FROM [almalqa2023abha].[dbo].tbBillBody
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbBillheader
INSERT INTO realestatebrokeragemanagement.[dbo].tbBillheader
SELECT *, ' ' FROM [almalqa2023abha].[dbo].tbBillheader
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbEditPassword
INSERT INTO realestatebrokeragemanagement.[dbo].tbEditPassword
SELECT * FROM [almalqa2023abha].[dbo].tbEditPassword
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbEmailSettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbEmailSettings
SELECT * FROM [almalqa2023abha].[dbo].tbEmailSettings
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbPlanInfo
INSERT INTO realestatebrokeragemanagement.[dbo].tbPlanInfo
SELECT * FROM [almalqa2023abha].[dbo].tbPlanInfo
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbLand
INSERT INTO realestatebrokeragemanagement.[dbo].tbLand
SELECT (SELECT TOP(1) guid From [almalqa2023abha].[dbo].tbPlanInfo),  *, ' ' FROM [almalqa2023abha].[dbo].tbLand
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbLandTrans
INSERT INTO realestatebrokeragemanagement.[dbo].tbLandTrans
SELECT * FROM [almalqa2023abha].[dbo].tbLandTrans
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbPay
INSERT INTO realestatebrokeragemanagement.[dbo].tbPay
SELECT * FROM [almalqa2023abha].[dbo].tbPay
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbPayContract
INSERT INTO realestatebrokeragemanagement.[dbo].tbPayContract
SELECT * FROM [almalqa2023abha].[dbo].tbPayContract
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbPaymentsNotes
INSERT INTO realestatebrokeragemanagement.[dbo].tbPaymentsNotes
SELECT * FROM [almalqa2023abha].[dbo].tbPaymentsNotes
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbPicture
INSERT INTO realestatebrokeragemanagement.[dbo].tbPicture
SELECT * FROM [almalqa2023abha].[dbo].tbPicture
Go


DELETE FROM realestatebrokeragemanagement.[dbo].tbPriceLog
INSERT INTO realestatebrokeragemanagement.[dbo].tbPriceLog
SELECT * FROM [almalqa2023abha].[dbo].tbPriceLog
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbReportCustomView
INSERT INTO realestatebrokeragemanagement.[dbo].tbReportCustomView
SELECT * FROM [almalqa2023abha].[dbo].tbReportCustomView
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbSaleOrder
INSERT INTO realestatebrokeragemanagement.[dbo].tbSaleOrder
SELECT * FROM [almalqa2023abha].[dbo].tbSaleOrder
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbSaleOrderBody
INSERT INTO realestatebrokeragemanagement.[dbo].tbSaleOrderBody
SELECT * FROM [almalqa2023abha].[dbo].tbSaleOrderBody
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbSelectObject
INSERT INTO realestatebrokeragemanagement.[dbo].tbSelectObject
SELECT * FROM [almalqa2023abha].[dbo].tbSelectObject
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbSMSsettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbSMSsettings
SELECT * FROM [almalqa2023abha].[dbo].tbSMSsettings
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbTaxDiscount
INSERT INTO realestatebrokeragemanagement.[dbo].tbTaxDiscount
SELECT * FROM [almalqa2023abha].[dbo].tbTaxDiscount
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbUsers
INSERT INTO realestatebrokeragemanagement.[dbo].tbUsers
SELECT * FROM [almalqa2023abha].[dbo].tbUsers
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbUsersPermissions
INSERT INTO realestatebrokeragemanagement.[dbo].tbUsersPermissions
SELECT * FROM [almalqa2023abha].[dbo].tbUsersPermissions
Go

DELETE FROM realestatebrokeragemanagement.[dbo].tbVatSettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbVatSettings
SELECT * FROM [almalqa2023abha].[dbo].tbVatSettings
Go
