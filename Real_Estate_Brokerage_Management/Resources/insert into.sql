
DELETE FROM realestatebrokeragemanagement.[dbo].tbAccount
INSERT INTO realestatebrokeragemanagement.[dbo].tbAccount
SELECT * FROM [almalqa2023abha].[dbo].tbAccount

DELETE FROM realestatebrokeragemanagement.[dbo].tbLog
INSERT INTO realestatebrokeragemanagement.[dbo].tbLog
SELECT * FROM [almalqa2023abha].[dbo].tbLog

DELETE FROM realestatebrokeragemanagement.[dbo].tbAgent
INSERT INTO realestatebrokeragemanagement.[dbo].tbAgent
SELECT  *, ' ' FROM [almalqa2023abha].[dbo].tbAgent

DELETE FROM realestatebrokeragemanagement.[dbo].tbAppInfo
INSERT INTO realestatebrokeragemanagement.[dbo].tbAppInfo
SELECT * FROM [almalqa2023abha].[dbo].tbAppInfo

DELETE FROM realestatebrokeragemanagement.[dbo].tbAttachment
INSERT INTO realestatebrokeragemanagement.[dbo].tbAttachment
SELECT * FROM [almalqa2023abha].[dbo].tbAttachment

DELETE FROM realestatebrokeragemanagement.[dbo].tbBanks
INSERT INTO realestatebrokeragemanagement.[dbo].tbBanks
SELECT * FROM [almalqa2023abha].[dbo].tbBanks

DELETE FROM realestatebrokeragemanagement.[dbo].tbBillBody
INSERT INTO realestatebrokeragemanagement.[dbo].tbBillBody
SELECT * FROM [almalqa2023abha].[dbo].tbBillBody

DELETE FROM realestatebrokeragemanagement.[dbo].tbBillheader
INSERT INTO realestatebrokeragemanagement.[dbo].tbBillheader
SELECT *, ' ' FROM [almalqa2023abha].[dbo].tbBillheader

DELETE FROM realestatebrokeragemanagement.[dbo].tbEditPassword
INSERT INTO realestatebrokeragemanagement.[dbo].tbEditPassword
SELECT * FROM [almalqa2023abha].[dbo].tbEditPassword

DELETE FROM realestatebrokeragemanagement.[dbo].tbEmailSettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbEmailSettings
SELECT * FROM [almalqa2023abha].[dbo].tbEmailSettings

DELETE FROM realestatebrokeragemanagement.[dbo].tbPlanInfo
INSERT INTO realestatebrokeragemanagement.[dbo].tbPlanInfo
SELECT * FROM [almalqa2023abha].[dbo].tbPlanInfo

DELETE FROM realestatebrokeragemanagement.[dbo].tbLand
INSERT INTO realestatebrokeragemanagement.[dbo].tbLand
SELECT (SELECT TOP(1) guid From [almalqa2023abha].[dbo].tbPlanInfo),  *, ' ' FROM [almalqa2023abha].[dbo].tbLand

DELETE FROM realestatebrokeragemanagement.[dbo].tbLandTrans
INSERT INTO realestatebrokeragemanagement.[dbo].tbLandTrans
SELECT * FROM [almalqa2023abha].[dbo].tbLandTrans

DELETE FROM realestatebrokeragemanagement.[dbo].tbPay
INSERT INTO realestatebrokeragemanagement.[dbo].tbPay
SELECT * FROM [almalqa2023abha].[dbo].tbPay

DELETE FROM realestatebrokeragemanagement.[dbo].tbPayContract
INSERT INTO realestatebrokeragemanagement.[dbo].tbPayContract
SELECT * FROM [almalqa2023abha].[dbo].tbPayContract

DELETE FROM realestatebrokeragemanagement.[dbo].tbPaymentsNotes
INSERT INTO realestatebrokeragemanagement.[dbo].tbPaymentsNotes
SELECT * FROM [almalqa2023abha].[dbo].tbPaymentsNotes

DELETE FROM realestatebrokeragemanagement.[dbo].tbPicture
INSERT INTO realestatebrokeragemanagement.[dbo].tbPicture
SELECT * FROM [almalqa2023abha].[dbo].tbPicture


DELETE FROM realestatebrokeragemanagement.[dbo].tbPriceLog
INSERT INTO realestatebrokeragemanagement.[dbo].tbPriceLog
SELECT * FROM [almalqa2023abha].[dbo].tbPriceLog

DELETE FROM realestatebrokeragemanagement.[dbo].tbReportCustomView
INSERT INTO realestatebrokeragemanagement.[dbo].tbReportCustomView
SELECT * FROM [almalqa2023abha].[dbo].tbReportCustomView

DELETE FROM realestatebrokeragemanagement.[dbo].tbSaleOrder
INSERT INTO realestatebrokeragemanagement.[dbo].tbSaleOrder
SELECT * FROM [almalqa2023abha].[dbo].tbSaleOrder

DELETE FROM realestatebrokeragemanagement.[dbo].tbSaleOrderBody
INSERT INTO realestatebrokeragemanagement.[dbo].tbSaleOrderBody
SELECT * FROM [almalqa2023abha].[dbo].tbSaleOrderBody

DELETE FROM realestatebrokeragemanagement.[dbo].tbSelectObject
INSERT INTO realestatebrokeragemanagement.[dbo].tbSelectObject
SELECT * FROM [almalqa2023abha].[dbo].tbSelectObject

DELETE FROM realestatebrokeragemanagement.[dbo].tbSMSsettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbSMSsettings
SELECT * FROM [almalqa2023abha].[dbo].tbSMSsettings

DELETE FROM realestatebrokeragemanagement.[dbo].tbTaxDiscount
INSERT INTO realestatebrokeragemanagement.[dbo].tbTaxDiscount
SELECT * FROM [almalqa2023abha].[dbo].tbTaxDiscount

DELETE FROM realestatebrokeragemanagement.[dbo].tbUsers
INSERT INTO realestatebrokeragemanagement.[dbo].tbUsers
SELECT * FROM [almalqa2023abha].[dbo].tbUsers

DELETE FROM realestatebrokeragemanagement.[dbo].tbUsersPermissions
INSERT INTO realestatebrokeragemanagement.[dbo].tbUsersPermissions
SELECT * FROM [almalqa2023abha].[dbo].tbUsersPermissions

DELETE FROM realestatebrokeragemanagement.[dbo].tbVatSettings
INSERT INTO realestatebrokeragemanagement.[dbo].tbVatSettings
SELECT * FROM [almalqa2023abha].[dbo].tbVatSettings